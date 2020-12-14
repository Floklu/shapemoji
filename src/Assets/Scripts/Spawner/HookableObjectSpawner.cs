using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Spawner
{
    /**
    * Class, which manages the available stones on the playground
    */
    public class HookableObjectSpawner : MonoBehaviour
    {
        [Tooltip("Defines the maximum number of stones on the playing field")][FormerlySerializedAs("MaxStones")] public int maxStones;
        [Tooltip("Defines the maximum number of Items on the playing field")] public int maxItems;
        [Tooltip("Determines the probability in percent to spawn an item")] public float percentageItemSpawn;

        public HookableGameObjectFactory factory;
        public List<GameObject> spawnZones;

        private readonly List<SpawnPlace> _spawnPlaces;

        /**
         * constructor of class StoneSpawner
         */
        public HookableObjectSpawner()
        {
            _spawnPlaces = new List<SpawnPlace>();
        }

        private void Start()
        {
            StartGeneration();
            InvokeRepeating(nameof(CreateRandomStone), 1f, 1f);
            InvokeRepeating(nameof(CreateRandomItem), 1f, 1f);
        }

        /**
         * Generates predefined number of random stones and places them onto the playground
         */
        public void StartGeneration()
        {
            foreach (var child in spawnZones.SelectMany(zone => zone.transform.Cast<Transform>()))
                _spawnPlaces.Add(child.gameObject.GetComponent<SpawnPlace>());

            for (var i = 0; i < maxStones; i++) CreateRandomStone();
        }

        /**
         * * deletes a HookableObject at given position
         * *
         * * @param hookableObject delete reference of given hookableObject to free position
         */
        public void DeleteHookableObject(HookableObject hookableObject)
        {
            var places = _spawnPlaces
                .Where(ContainsHookableObject)
                .Select(x => x.GetComponent<SpawnPlace>())
                .Where(x => x.hookableObject.Equals(hookableObject)).ToList();
            places.ForEach(x => x.hookableObject = null);
        }

        /**
         * checks, if Spawner is at maximum capacity for stones
         * 
         * @returns true, if spawner cannot spawn any new stones
         */
        public bool ContainsMaxAmountStones()
        {
            var places = _spawnPlaces.Where(ContainsStone).Select(x => 1).Sum();
            return places >= maxStones;
        }

        /**
         * checks, if Spawner is at maximum capacity for items
         *
         * @returns true, if spawner cannot spawn any new items
         */
        public bool ContainsMaxAmountItems()
        {
            var places = _spawnPlaces.Where(ContainsItem).Select(x=>1).Sum();
            return places >= maxItems;
        }


        /**
         * creates a stone at a random location
         */
        public void CreateRandomStone()
        {
            if (ContainsMaxAmountStones()) return;

            var places = _spawnPlaces.Where(plc => !plc.isItemContainer && !ContainsStone(plc)).ToList();

            CreateHookableObject(places, new Func<float, float, GameObject>(factory.CreateStone));
        }

        public void CreateRandomItem()
        {
            var randomSpawn = Random.Range(0, 100);
            if (randomSpawn < percentageItemSpawn)
            {
                if (ContainsMaxAmountItems()) return;
                var places = _spawnPlaces.Where(plc => plc.isItemContainer && !ContainsItem(plc)).ToList();
                CreateHookableObject(places, new Func<float, float, GameObject>(factory.CreateItem));    
            }
            
        }

        /**
         * creates a hookable Object on a random place of a list of unoccupied spawnPlaces
         *
         * @param spawnPlaces list of spawnPlaces
         */
        private void CreateHookableObject(List<SpawnPlace> spawnPlaces, Delegate spawnHookableObject)
        {
            if (spawnPlaces.Count < 1) return;

            var random = Random.Range(0, spawnPlaces.Count);
            var place = spawnPlaces[random];
            var spawn = place.GetComponent<SpawnPlace>();

            var spawnPosition = place.transform.position;
            var x = spawnPosition.x;
            var y = spawnPosition.y;
            //var stone = factory.CreateStone(x, y);
            var hookableObject = (GameObject)spawnHookableObject.DynamicInvoke(x, y);
            spawn.hookableObject = hookableObject.GetComponent<HookableObject>();
        }

        /**
         * determines, if the chosen GameObject already contains a stone
         * 
         * @param place chosen spawn place for a stone
         * @returns true, if the gameObject contains a SpawnPlace and it contains a stone
         */
        private static bool ContainsStone(SpawnPlace place)
        {
            //var spawn = place.GetComponent<SpawnPlace>();
            return place != null && place.ContainsStone();
        }
        
        /**
         * determines, if the chosen GameObject already contains an item
         * 
         * @param place chosen spawn place for a item
         * @returns true, if the gameObject contains a SpawnPlace and it contains a item
         */
        private static bool ContainsItem(SpawnPlace place)
        {
            //var spawn = place.GetComponent<SpawnPlace>();
            return place != null && place.ContainsItem();
        }
        
        /**
         * determines, if the chosen GameObject already contains a HookableObject
         * 
         * @param place chosen spawn place for a HookableObject
         * @returns true, if the gameObject contains a SpawnPlace and it contains a HookableObject
         */
        private static bool ContainsHookableObject(SpawnPlace place)
        {
            //var spawn = place.GetComponent<SpawnPlace>();
            return place != null && (place.ContainsStone() || place.ContainsItem()) ;
        }
    }
}