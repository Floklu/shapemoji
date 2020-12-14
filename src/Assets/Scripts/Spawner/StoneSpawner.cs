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
    public class StoneSpawner : MonoBehaviour
    {
        [FormerlySerializedAs("MaxStones")] public int maxStones;
        public int maxItems;

        public HookableGameObjectFactory factory;
        public List<GameObject> spawnZones;

        private readonly List<SpawnPlace> _spawnPlaces;

        /**
         * constructor of class StoneSpawner
         */
        public StoneSpawner()
        {
            _spawnPlaces = new List<SpawnPlace>();
        }

        private void Start()
        {
            StartGeneration();
            InvokeRepeating(nameof(CreateRandomStone), 1f, 1f);
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
            return places == maxStones;
        }


        /**
         * creates a stone at a random location
         */
        public void CreateRandomStone()
        {
            if (ContainsMaxAmountStones()) return;

            var places = _spawnPlaces.Where(plc => !ContainsStone(plc)).ToList();

            CreateHookableObject(places, new Func<float, float, GameObject>(factory.CreateStone));
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