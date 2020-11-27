﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Harpoon;
using NUnit.Framework;
using Spawner;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    
    /**
     * Test class for wind in and inventory
     * @author Andrei Dziubenka
     * @date 2020.11.26
     */
    public class InventoryTest
    {
        
        /*
         * _harpoon, _projectile, _inventory, _field are GameObjects selected by class method LoadPlayer
         */
        private GameObject _harpoon, _projectile, _inventory, _field,_field2, _spawner;
        
        /**
         * Setup test environment
         */
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("Scenes/Scene_Playground_2vs2");
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator InventoryTestWithEnumeratorPasses()
        {
            yield return new WaitForSeconds(5.0f);
            
            List<GameObject> stones = new List<GameObject>();
            
            List<GameObject> spawnZones = new List<GameObject>();
            List<GameObject> spawnPlaces = new List<GameObject>();
            
            spawnZones.Add(_field);
            spawnZones.Add(_field2);

            foreach (var child in spawnZones.SelectMany(zone => zone.transform.Cast<Transform>()))
            {
                spawnPlaces.Add(child.gameObject);
            }
/*            
            stones = spawnPlaces.Where(plc => !ContainsStone(plc)).ToList();

            var stoneToAim = stones.First();
            yield return AimAtObject(stoneToAim);
*/
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
            
            
        }
        
        /**
         * Aims at Object and Shoots Harpoon#
         * @param gameObject Object to aim
         */
        private IEnumerator AimAtObject(GameObject gameObject)
        {
            Quaternion q = Quaternion.Euler(_harpoon.transform.eulerAngles);
            Vector3 harpoonRotation = q * Vector3.up;
            Vector3 pointWorldCoord = gameObject.transform.position;

            Vector3 harpoonPosition = _harpoon.transform.position;
            Vector3 path = pointWorldCoord - harpoonPosition;
            float angle = Vector3.SignedAngle(harpoonRotation, path, Vector3.forward);
            _harpoon.transform.Rotate(0, 0, angle);
            _harpoon.GetComponent<HarpoonController>().ShootProjectile();
            yield return new WaitForSeconds(5.0f);
            Assert.Zero(_projectile.GetComponent<Rigidbody2D>().velocity.magnitude);
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

        private void LoadPlayer(int player)
        {
            var team = (player + 1) / 2;
            _harpoon = GameObject.Find("Team_" + team + "/Player_" + player + "/Base/HarpoonBase/Harpoon");
            _projectile =
                GameObject.Find("Team_" + team + "/Player_" + player +
                                "/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");
            _inventory =
                GameObject.Find("Team_" + team + "/Player_" + player +
                                "/Base/Inventory");

            switch (player)
            {
                case 1:
                    _field = GameObject.Find("SpawnArea_SouthWest");
                    _field2 = GameObject.Find("SpawnArea_SouthEast");
                    break;
                case 2:
                    _field = GameObject.Find("SpawnArea_NorthWest");
                    _field2 = GameObject.Find("SpawnArea_SouthWest");
                    break;
                case 3:
                    _field = GameObject.Find("SpawnArea_SouthEast");
                    break;
                case 4:
                    _field = GameObject.Find("SpawnArea_SouthWest");
                    _field2 = GameObject.Find("SpawnArea_NorthWest");
                    break;
            }


        }
        
    }
}
