using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Harpoon;
using Lean.Touch;
using NUnit.Framework;
using Spawner;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Assert = NUnit.Framework.Assert;

namespace Tests.PlayMode
{
    /**
     * Test class for workshop integration
     * @author Andrei Dziubenka
     * @date 2020.12.01
     */
    public class WorkshopTest
    {
        private int _currentPlayer;

        private Stone _currentStone;

        /*
         * _harpoon, _projectile, _inventory, _field are GameObjects selected by class method LoadPlayer
         */
        private GameObject _harpoon,
            _projectile,
            _inventory,
            _field,
            _field2,
            _spawner,
            _wheel,
            _workshopSlot,
            _otherSlot;

        /**
         * Setup test environment
         */
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("Scenes/Scene_Playground_2vs2");
        }

        /**
         * Test for Player 1
         */
        [UnityTest]
        public IEnumerator WorkshopTestPlayer1()
        {
            _currentPlayer = 1;
            yield return TestPlayer();
        }

        /**
         * Test for Player 2
         */
        [UnityTest]
        public IEnumerator WorkshopTestPlayer2()
        {
            _currentPlayer = 2;
            yield return TestPlayer();
        }

        /**
         * Test for Player 3
         */
        [UnityTest]
        public IEnumerator WorkshopTestPlayer3()
        {
            _currentPlayer = 3;
            yield return TestPlayer();
        }

        /**
         * Test for Player 4
         */
        [UnityTest]
        public IEnumerator WorkshopTestPlayer4()
        {
            _currentPlayer = 4;
            yield return TestPlayer();
        }

        /**
         * Run test for player
         * @param player Player number
         */
        private IEnumerator TestPlayer()
        {
            LoadPlayer();
            yield return GetStone();
            InitTest();
            yield return MoveToOtherSlot();
            yield return MoveToSlot();
            yield return MoveBackToInventory();
            yield return GetStone();
            yield return MoveToFullSlot();
        }

        /**
         * Test, if initial conditions are correct
         */
        private void InitTest()
        {
            var leanSelectable = _currentStone.gameObject.GetComponent<LeanSelectable>();
            Assert.IsNotNull(leanSelectable, $"Player {_currentPlayer}: Stone is not selectable in the inventory");
            //Assert.AreEqual(_workshopSlot.GetComponent<Workshop>().GetInventory(), _inventory, $"Player {_currentPlayer}: Inventory not assigned to workshop");
            Assert.IsTrue(_workshopSlot.GetComponent<Workshop>().IsEmpty(), $"Player {_currentPlayer}: workshop is already full");
        }

        /**
         * Move current stone to a slot, that doesn't belong to current player and test, if stone is moved back into inventory
         */
        private IEnumerator MoveToOtherSlot()
        {
            yield return AssertMovement(_otherSlot.transform.position, _currentStone.transform.position,
                $"Player {_currentPlayer}: Stone is not in the Inventory");
            // ReSharper disable once Unity.InefficientPropertyAccess
            AssertVectors(_inventory.GetComponent<Inventory>().GetPositionOfStoneChild(_currentStone),
                _currentStone.transform.position, $"Player {_currentPlayer}: Stone is not in the Inventory");
        }

        /**
         * Move current stone to current workshop slot and test, if stone is in workshop
         */
        private IEnumerator MoveToSlot()
        {
            // ReSharper disable once Unity.InefficientPropertyAccess
            yield return AssertMovement(_workshopSlot.transform.position + Vector3.up, _workshopSlot.transform.position,
                $"Player {_currentPlayer}: Stone is not in center of workshop");
            Assert.IsFalse(_workshopSlot.GetComponent<Workshop>().IsEmpty(),$"Player {_currentPlayer}: Workshop slot should be full");
        }

        /**
         * Move current stone from workshop to inventory and test, if stone is moved back to workshop slot
         */
        private IEnumerator MoveBackToInventory()
        {
            // ReSharper disable once Unity.InefficientPropertyAccess
            yield return AssertMovement(_inventory.transform.position, _workshopSlot.transform.position,
                $"Player {_currentPlayer}: Stone is not in center of workshop");
            Assert.AreEqual(_inventory.GetComponent<Inventory>().GetPositionOfStoneChild(_currentStone), Vector3.zero,
                $"Player {_currentPlayer}: Stone was not removed from inventory");
        }

        /**
         * Move new stone to current slot, which is already full, and test, if stone is moved back to inventory
         */
        private IEnumerator MoveToFullSlot()
        {
            // ReSharper disable twice Unity.InefficientPropertyAccess
            yield return AssertMovement(_workshopSlot.transform.position, _currentStone.transform.position,
                $"Player {_currentPlayer}: Stone is not in the Inventory, despite being moved to full inventory");
        }

        /**
         * Move stone and assert position
         * @param movePosition Position, to move current stone to
         * @param expectedPosition expected Position, after current stone was dropped
         * @param message error message
         */
        private IEnumerator AssertMovement(Vector3 movePosition, Vector3 expectedPosition, String message)
        {
            yield return MoveStone(_currentStone, movePosition);
            AssertVectors(expectedPosition, _currentStone.transform.position, message);
        }

        /**
         * Check, if 2 Vectors are equal, with delta=0.1
         * @param a first vector
         * @param b second vector
         * @param message error message, if vectors are not equal
         */
        private void AssertVectors(Vector3 a, Vector3 b, String message)
        {
            Assert.AreEqual(a.x, b.x, 0.1, message);
            Assert.AreEqual(a.y, b.y, 0.1, message);
        }

        /**
         * Get new stone into Inventory
         */
        private IEnumerator GetStone()
        {
            GameObject stoneCollided = null;

            _projectile.GetComponent<ProjectileCollision>().CollisionEvent +=
                delegate(object sender, Collider2D collider2D)
                {
                    //if it is a stone
                    if (collider2D.gameObject.CompareTag("Stone"))
                    {
                        //if no stone collision happened in the past
                        // ReSharper disable once AccessToModifiedClosure
                        if (stoneCollided == null)
                        {
                            stoneCollided = collider2D.gameObject;
                        }
                    }
                };

            GameObject stoneToAim = FindStone();
            Assert.IsNotNull(stoneToAim, $"Player {_currentPlayer}: not enough stones on the playing field");
            // stoneCollided can be changed by Collision Event Handler
            // stoneCollided is used to determine which stone was catched by harpoon
            stoneCollided = null;
            Vector3 stonePos = stoneToAim.transform.position;
            yield return AimAtPoint(stonePos.x, stonePos.y);
            WindIn();
            yield return new WaitForSeconds(0.1f);
            Assert.IsNotNull(stoneCollided, $"Player {_currentPlayer}: could not find collided stone");

            _currentStone = (Stone) stoneCollided.GetComponent<HookableObject>();

            Assert.IsNull(_currentStone.gameObject.GetComponent<LeanSelectable>(),
                $"Player {_currentPlayer}: Stone is selectable on the playing field");

            yield return new WaitForSeconds(5.0f);


            var position = stoneCollided.transform.position;
            AssertVectors(position, _inventory.GetComponent<Inventory>().GetPositionOfStoneChild(_currentStone),
                $"Player {_currentPlayer}: Stone wasn't placed into inventory");
        }

        /**
         * Moves stone to a position and simulates Deselect Event
         * @param stone Stone to move
         * 
         */
        private IEnumerator MoveStone(Stone stone, Vector3 position)
        {
            stone.transform.position = position;

            yield return new WaitForSeconds(0.2f);
            stone.OnDeselectOnUp();
            yield return new WaitForSeconds(0.2f);
        }

        /**
         * Wind in harpoon method
         */
        private void WindIn()
        {
            for (var i = 0; i < 400; i++)
            {
                _wheel.GetComponent<CrankController>().RotateCrank(90);
            }
        }

        /**
         * Find stone for the current player to aim at
         * @return Stone from a spawn place or null, if no stones found
         */
        private GameObject FindStone()
        {
            List<GameObject> spawnZones = new List<GameObject>();
            List<GameObject> spawnPlaces = new List<GameObject>();

            spawnZones.Add(_field);
            spawnZones.Add(_field2);

            foreach (var child in spawnZones.SelectMany(zone => zone.transform.Cast<Transform>()))
            {
                spawnPlaces.Add(child.gameObject);
            }

            var stones = spawnPlaces.Where(ContainsStone).ToList();

            if (stones.Any())
            {
                var stoneToAim = stones.First().GetComponent<SpawnPlace>().stone.gameObject;
                return stoneToAim;
            }

            // return null, if no stones are found
            return null;
        }

        /**
         * Rotates Harpoon to aim at point
         * @param harpoon Harpoon Object to aim
         * @param pointX x parameter of point to aim in Screen Coordinates
         * @param pointY y parameter of point to aim in Screen Coordinates
         */
        private IEnumerator AimAtPoint(float pointX, float pointY)
        {
            Vector3 pointWorldCoord = new Vector3(pointX, pointY, 0);
            Vector3 harpoonPosition = _harpoon.transform.position;
            Vector3 path = pointWorldCoord - harpoonPosition;
            float angle = Vector3.SignedAngle(Vector3.up, path, Vector3.forward);
            _harpoon.GetComponent<HarpoonController>().RotateHarpoon(angle);
            _harpoon.GetComponent<HarpoonController>().ShootProjectile();
            yield return new WaitForSeconds(10.0f);
        }

        /**
         * Source: StoneSpawner.cs
         * Author: Robert Meier
         * determines, if the chosen GameObject already contains a stone
         *
         * @param place chosen spawn place for a stone
         * @returns true, if the gameObject contains a SpawnPlace and it contains a stone
         */
        private bool ContainsStone(GameObject place)
        {
            var spawn = place.GetComponent<SpawnPlace>();
            return spawn != null && spawn.stone != null;
        }

        /**
         * Method to load player Game Objects
         * @param player Player number
         */
        private void LoadPlayer()
        {
            var team = (_currentPlayer + 1) / 2;
            _harpoon = GameObject.Find("Team_" + team + "/Player_" + _currentPlayer + "/Base/HarpoonBase/Harpoon");
            _projectile =
                GameObject.Find("Team_" + team + "/Player_" + _currentPlayer +
                                "/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");
            _inventory =
                GameObject.Find("Team_" + team + "/Player_" + _currentPlayer +
                                "/Base/Inventory");

            _wheel =
                GameObject.Find("Team_" + team + "/Player_" + _currentPlayer +
                                "/Base/Wheel");


            int slotNumber = 1;

            switch (_currentPlayer)
            {
                case 1:
                    _field = GameObject.Find("SpawnArea_SouthWest");
                    _field2 = GameObject.Find("SpawnArea_SouthEast");
                    slotNumber = 1;
                    break;
                case 2:
                    _field = GameObject.Find("SpawnArea_NorthWest");
                    _field2 = GameObject.Find("SpawnArea_NorthEast");
                    slotNumber = 2;
                    break;
                case 3:
                    _field = GameObject.Find("SpawnArea_SouthEast");
                    _field2 = GameObject.Find("SpawnArea_SouthWest");
                    slotNumber = 1;
                    break;
                case 4:
                    _field = GameObject.Find("SpawnArea_NorthEast");
                    _field2 = GameObject.Find("SpawnArea_NorthWest");
                    slotNumber = 2;
                    break;
            }

            _workshopSlot = GameObject.Find("Team_" + team + "/Workshop/Slot (" + slotNumber +
                                            ")");

            var otherSlot = slotNumber == 1 ? 2 : 1;

            _otherSlot = GameObject.Find("Team_" + team + "/Workshop/Slot (" + otherSlot +
                                         ")");
        }
    }
}