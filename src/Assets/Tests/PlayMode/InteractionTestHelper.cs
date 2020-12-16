using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Harpoon;
using Lean.Touch;
using NUnit.Framework;
using Spawner;
using UnityEngine;
using UnityEngine.UI;

namespace Tests.PlayMode
{
    /**
     * Provides Utilities and Interfaces to help test the interaction with the game
     */
    public class InteractionTestHelper
    {
        private readonly int _currentPlayer;

        /**
         * Initializes InteractionTestHelper with given Player
         */
        public InteractionTestHelper(int currentPlayer)
        {
            _currentPlayer = currentPlayer;
            LoadPlayer(currentPlayer);
        }

        #region Properties

        public Stone CurrentStone { get; private set; }
        public GameObject Harpoon { get; private set; }
        public GameObject Projectile { get; private set; }
        public GameObject Inventory { get; private set; }
        public GameObject Field { get; private set; }
        public GameObject Field2 { get; private set; }
        public GameObject Wheel { get; private set; }
        public GameObject WorkshopSlot { get; private set; }
        public GameObject WorkshopSlotTeamMate { get; private set; }
        public GameObject Emoji { get; private set; }
        public GameObject ButtonEmoji_1 { get; private set; }
        public GameObject ButtonEmoji_2 { get; private set; }
        public Camera MainCamera { get; private set; }

        #endregion


        #region Stone

        /**
         * Test, if wound in Stone is draggable and if the workshop slot is empty
         */
        public void TestConditionsBeforeToWorkshop()
        {
            var leanSelectable = CurrentStone.gameObject.GetComponent<LeanSelectable>();
            Assert.IsNotNull(leanSelectable, $"Player {_currentPlayer}: Stone is not selectable in the inventory");
            Assert.IsTrue(WorkshopSlot.GetComponent<Workshop>().IsEmpty(), $"Player {_currentPlayer}: workshop is already full");
        }

        /**
         * Move current stone to the slot of the team mate and test, if stone is moved back into inventory
         */
        public IEnumerator MoveToTeamMateWorkshopSlot()
        {
            yield return AssertMovement(WorkshopSlotTeamMate.transform.position, CurrentStone.transform.position,
                $"Player {_currentPlayer}: Stone is not in the Inventory");
            // ReSharper disable once Unity.InefficientPropertyAccess
            AssertVectors(Inventory.GetComponent<Inventory>().GetPositionOfStoneChild(CurrentStone),
                CurrentStone.transform.position, $"Player {_currentPlayer}: Stone is not in the Inventory");
        }

        /**
         * Move current stone to current workshop slot and test, if stone is in workshop
         */
        public IEnumerator MoveToWorkshopSlot()
        {
            // ReSharper disable once Unity.InefficientPropertyAccess
            yield return AssertMovement(WorkshopSlot.transform.position + Vector3.up, WorkshopSlot.transform.position,
                $"Player {_currentPlayer}: Stone is not in center of workshop");
            Assert.IsFalse(WorkshopSlot.GetComponent<Workshop>().IsEmpty(),$"Player {_currentPlayer}: Workshop slot should be full");
        }

        /**
         * Move current stone from workshop to inventory and test, if stone is moved back to workshop slot
         */
        public IEnumerator MoveBackToInventory()
        {
            // ReSharper disable once Unity.InefficientPropertyAccess
            yield return AssertMovement(Inventory.transform.position, WorkshopSlot.transform.position,
                $"Player {_currentPlayer}: Stone is not in center of workshop");
            Assert.AreEqual(Inventory.GetComponent<Inventory>().GetPositionOfStoneChild(CurrentStone), Vector3.zero,
                $"Player {_currentPlayer}: Stone was not removed from inventory");
        }

        /**
         * Move new stone to current slot, which is already full, and test, if stone is moved back to inventory
         */
        public IEnumerator MoveToFullSlot()
        {
            // ReSharper disable twice Unity.InefficientPropertyAccess
            yield return AssertMovement(WorkshopSlot.transform.position, CurrentStone.transform.position,
                $"Player {_currentPlayer}: Stone is not in the Inventory, despite being moved to full inventory");
        }

        /**
         * Move new stone to emoji and test if stone remains at same position
         */
        public IEnumerator MoveToEmoji()
        {
            yield return AssertMovement(Emoji.transform.position, Emoji.transform.position,
                $"Player {_currentPlayer}: Stone is not on the Emoji, despite being moved to Emoji");
        }

        /**
         * Move stone and assert position
         * @param movePosition Position, to move current stone to
         * @param expectedPosition expected Position, after current stone was dropped
         * @param message error message
         */
        public IEnumerator AssertMovement(Vector3 movePosition, Vector3 expectedPosition, string message)
        {
            yield return MoveStone(CurrentStone, movePosition);
            AssertVectors(expectedPosition, CurrentStone.transform.position, message);
        }

        /**
         * Check, if 2 Vectors are equal, with delta=0.1
         * @param a first vector
         * @param b second vector
         * @param message error message, if vectors are not equal
         */
        public void AssertVectors(Vector3 a, Vector3 b, string message)
        {
            Assert.AreEqual(a.x, b.x, 0.1, message);
            Assert.AreEqual(a.y, b.y, 0.1, message);
        }

        /**
         * Get new stone into Inventory
         */
        public IEnumerator GetStone()
        {
            GameObject stoneCollided = null;

            Projectile.GetComponent<ProjectileCollision>().CollisionEvent +=
                delegate(object sender, Collider2D collider2D)
                {
                    //if it is a stone
                    if (collider2D.gameObject.CompareTag("Stone"))
                    //if no stone collision happened in the past
                    // ReSharper disable once AccessToModifiedClosure
                        if (stoneCollided == null)
                            stoneCollided = collider2D.gameObject;
                };

            var stoneToAim = FindStone();
            Assert.IsNotNull(stoneToAim, $"Player {_currentPlayer}: not enough stones on the playing field");
            // stoneCollided can be changed by Collision Event Handler
            // stoneCollided is used to determine which stone was catched by harpoon
            stoneCollided = null;
            var stonePos = stoneToAim.transform.position;
            yield return AimAtPoint(stonePos.x, stonePos.y);
            WindIn();
            yield return new WaitForSeconds(1f);
            Assert.IsNotNull(stoneCollided, $"Player {_currentPlayer}: could not find collided stone");

            CurrentStone = (Stone) stoneCollided.GetComponent<HookableObject>();

            //Assert.IsNull(CurrentStone.gameObject.GetComponent<LeanSelectable>(),
            //    $"Player {_currentPlayer}: Stone is selectable on the playing field");

            yield return new WaitForSeconds(1f);


            var position = stoneCollided.transform.position;
            AssertVectors(position, Inventory.GetComponent<Inventory>().GetPositionOfStoneChild(CurrentStone),
                $"Player {_currentPlayer}: Stone wasn't placed into inventory");
        }

        /**
         * Moves stone to a position and simulates Deselect Event
         * @param stone Stone to move
         * 
         */
        public IEnumerator MoveStone(Stone stone, Vector3 position)
        {
            stone.transform.position = position;

            yield return new WaitForSeconds(0.2f);
            stone.OnDeselectOnUp();
            yield return new WaitForSeconds(0.2f);
        }

        /**
         * Find stone for the current player to aim at
         * @return Stone from a spawn place or null, if no stones found
         */
        public GameObject FindStone()
        {
            var spawnZones = new List<GameObject>();
            var spawnPlaces = new List<GameObject>();

            spawnZones.Add(Field);
            spawnZones.Add(Field2);

            foreach (var child in spawnZones.SelectMany(zone => zone.transform.Cast<Transform>())) spawnPlaces.Add(child.gameObject);

            var stones = spawnPlaces.Where(ContainsStone).ToList();

            if (stones.Any())
            {
                var stoneToAim = stones.First().GetComponent<SpawnPlace>().hookableObject.gameObject;
                return stoneToAim;
            }

            // return null, if no stones are found
            return null;
        }


        /**
         * determines, if the chosen GameObject already contains a stone
         *
         * @param place chosen spawn place for a stone
         * @returns true, if the gameObject contains a SpawnPlace and it contains a stone
         */
        public bool ContainsStone(GameObject place)
        {
            var spawn = place.GetComponent<SpawnPlace>();
            return spawn != null && spawn.hookableObject != null;
        }

        #endregion

        #region Harpoon

        /**
         * Rotates Harpoon to aim at point
         * @param harpoon Harpoon Object to aim
         * @param pointX x parameter of point to aim in Screen Coordinates
         * @param pointY y parameter of point to aim in Screen Coordinates
         */
        private IEnumerator AimAtPoint(float pointX, float pointY)
        {
            var collision = false;
            Projectile.GetComponent<ProjectileCollision>().CollisionEvent += (sender, collider2D) => collision = true;
            var pointWorldCoord = new Vector3(pointX, pointY, 0);
            var harpoonPosition = Harpoon.transform.position;
            var path = pointWorldCoord - harpoonPosition;
            var angle = Vector3.SignedAngle(Vector3.up, path, Vector3.forward);
            Harpoon.GetComponent<HarpoonController>().RotateHarpoon(angle);
            Harpoon.GetComponent<HarpoonController>().ShootProjectile();
            yield return new WaitUntil(() => collision);
        }

        /**
         * Wind in harpoon method
         */
        private void WindIn()
        {
            for (var i = 0; i < 400; i++) Wheel.GetComponent<CrankController>().RotateCrank(90);
        }

        #endregion

        #region Emoji

        /**
         * Turns stones on emoji in for evaluation and checks, if stones are cleared from playing field and if the sprite of the team changes
         *
         * @returns true if number of stones before and after turn in differs and when sprite of emoji changes after turn in
         */
        public IEnumerator TurnInEmoji()
        {
            // before turn in
            var sprite = Emoji.GetComponent<SpriteRenderer>().sprite;
            var countStones = GameObject.FindGameObjectsWithTag("Stone").Count();
            ButtonEmoji_1.GetComponent<Toggle>().isOn = true;
            ButtonEmoji_2.GetComponent<Toggle>().isOn = true;
            
            yield return new WaitForSeconds(0.5f); // wait for turn in to be processed
            
            // after turn in
            var spriteNew = Emoji.GetComponent<SpriteRenderer>().sprite;
            var countStonesNew = GameObject.FindGameObjectsWithTag("Stone").Count();
            Assert.AreNotEqual(spriteNew, sprite, $"Player {_currentPlayer}: Emoji sprite didn't change after turn in");
            Assert.AreNotEqual(countStonesNew, countStones, $"Player {_currentPlayer}: expected stones to be cleared from Emoji after turn in");
        }

        #endregion

        /**
         * initializes GameObjects related for a player
         * 
         * @param player Player number
         */
        public void LoadPlayer(int player)
        {
            var team = (player + 1) / 2;
            Harpoon = GameObject.Find($"Team_{team}/Player_{player}/Base/HarpoonBase/Harpoon");
            Projectile = GameObject.Find($"Team_{team}/Player_{player}/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");
            Inventory = GameObject.Find($"Team_{team}/Player_{player}/Base/Inventory");
            Wheel = GameObject.Find($"Team_{team}/Player_{player}/Base/Wheel");
            Emoji = GameObject.Find($"Team_{team}/ScoreArea/Emoji");
            MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

            var slotNumber = 1;

            switch (player)
            {
                case 1:
                    Field = GameObject.Find("SpawnArea_SouthWest");
                    Field2 = GameObject.Find("SpawnArea_SouthEast");
                    slotNumber = 1;
                    break;
                case 2:
                    Field = GameObject.Find("SpawnArea_NorthWest");
                    Field2 = GameObject.Find("SpawnArea_NorthEast");
                    slotNumber = 2;
                    break;
                case 3:
                    Field = GameObject.Find("SpawnArea_SouthEast");
                    Field2 = GameObject.Find("SpawnArea_SouthWest");
                    slotNumber = 1;
                    break;
                case 4:
                    Field = GameObject.Find("SpawnArea_NorthEast");
                    Field2 = GameObject.Find("SpawnArea_NorthWest");
                    slotNumber = 2;
                    break;
            }

            switch (team)
            {
                case 1:
                    ButtonEmoji_1 = GameObject.Find("UIElements/Canvas/Button_TurnIn_Player_1");
                    ButtonEmoji_2 = GameObject.Find("UIElements/Canvas/Button_TurnIn_Player_2");
                    break;
                case 2:
                    ButtonEmoji_1 = GameObject.Find("UIElements/Canvas/Button_TurnIn_Player_3");
                    ButtonEmoji_2 = GameObject.Find("UIElements/Canvas/Button_TurnIn_Player_4");
                    break;
            }
            
            WorkshopSlot = GameObject.Find($"Team_{team}/Workshop/Slot ({slotNumber})");
            var otherSlot = slotNumber == 1 ? 2 : 1;
            WorkshopSlotTeamMate = GameObject.Find($"Team_{team}/Workshop/Slot ({otherSlot})");

            
        }
    }
}