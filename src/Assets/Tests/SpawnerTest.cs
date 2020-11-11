using System.Collections;
using System.Linq;
using NUnit.Framework;
using Spawner;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class SpawnerTest
    {
        [SetUp]
        public void SetUp()
        {
            EditorSceneManager.OpenScene($"Assets/Scenes/Scene_Playground_2vs2.unity");
        }

        /**
         * Tests, if the stoneFactory creates a stone at the target position
         *
         * @returns true, if stone is created and the position matches target position
         * @returns AssertionException, if stoneFactory couldn't be found or if stone is null or if position doesn't match target location
         */
        [UnityTest]
        public IEnumerator SpawnerTestStoneFactoryCreateStone()
        {
            var stoneFactory = GameObject.Find("StoneSpawner")?.GetComponent<StoneFactory>();
            Assert.NotNull(stoneFactory);

            const int x = 100;
            const int y = 10;
            var stone = stoneFactory.CreateStone(x, y);
            // skip frames to ensure stone is moved correctly
            // TODO: add trigger?
            for(int i=0; i<3; i++){yield return null;}

            Assert.NotNull(stone);
            var location = stone.transform.position;
            Assert.AreEqual(x, location.x);
            Assert.AreEqual(y, location.y);
            
            yield return null;
        }
        
        /**
         * Tests, whether the max number if stones as configured in StoneSpawner have been created
         *
         * @returns true, if the number of spawnPlaces which contains stone equals maxStone of spawner
         * @returns AssertionException, if spawner couldn't be found or if number of spawned stones differ from maxStones of spawner
         */
        [UnityTest]
        public IEnumerator SpawnerTestIfAllStonesSpawned()
        {
            var spawnPlaces = Object.FindObjectsOfType<GameObject>().Where(x => x.name.Equals("SpawnPlace"));
            var spawner = GameObject.Find("StoneSpawner")?.GetComponent<StoneSpawner>();
            Assert.NotNull(spawner);
            spawner.StartGeneration();
            
            //count spawned stones
            var stonesSpawned = spawnPlaces.Select(place => place.GetComponent<SpawnPlace>()).Select(placeScript => placeScript.stone != null ? 1 : 0).Sum();

            Assert.AreEqual(spawner.maxStones, stonesSpawned );
            
            yield return null;
        }
    }
}
