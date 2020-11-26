using System.Collections;
using System.Linq;
using NUnit.Framework;
using Spawner;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditMode
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
            var stoneFactory = GameObject.Find("StoneSpawner")?.GetComponent<HookableGameObjectFactory>();
            Assert.NotNull(stoneFactory);

            var x = Random.Range(0,100);
            var y = Random.Range(0,100);
            var stone = stoneFactory.CreateStone(x, y);
            
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

        /**
         * Tests, if stones can be properly deleted from the spawner
         *
         * @returns true, if spawner is full at first, but after deletion of a stone isn't full anymore.
         */
        [UnityTest]
        public IEnumerator SpawnerTestDeleteStone()
        {
            var spawnPlaces = Object.FindObjectsOfType<GameObject>().Where(x => x.name.Equals("SpawnPlace"));
            var spawner = GameObject.Find("StoneSpawner")?.GetComponent<StoneSpawner>();
            Assert.NotNull(spawner);
            Assert.IsFalse(spawner.IsFull());
            spawner.StartGeneration();
            Assert.IsTrue(spawner.IsFull());

            //get first stone
            var stone = spawnPlaces
                .Select(place => place.GetComponent<SpawnPlace>())
                .First(x => x.stone != null).stone;

            spawner.DeleteHookableObject(stone.GetComponent<HookableObject>());
            Assert.IsFalse(spawner.IsFull());
            
            spawner.CreateRandomStone();
            Assert.IsTrue(spawner.IsFull());
            
            yield return null;
        }
    }
}
