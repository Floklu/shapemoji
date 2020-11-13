using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spawner
{
    /**
    * Class, which manages the avaiable stones on the playground
    */
    public class StoneSpawner : MonoBehaviour
    {
        [FormerlySerializedAs("MaxStones")] public int maxStones;

        public StoneFactory factory;
        public List<GameObject> spawnZones;

        private List<GameObject> _stones;
        private List<GameObject> _spawnPlaces;

        private void Start()
        {
            StartGeneration();
        }

        /**
         * Generates predefined number of random stones and places them onto the playground 
         */
        public void StartGeneration()
        {
            Random.InitState((int) System.DateTime.Now.Ticks); //TODO should be moved to a different class
            _stones = new List<GameObject>();
            _spawnPlaces = new List<GameObject>();

            foreach (var child in spawnZones.SelectMany(zone => zone.transform.Cast<Transform>()))
            {
                _spawnPlaces.Add(child.gameObject);
            }

            InvokeRepeating(nameof(CreateRandomStone), 1f, 1f);
            for (var i = 0; i < maxStones; i++)
            {
                CreateRandomStone();
            }
        }
        
        //TODO: DELETE STONE
        /**
        * PLACEHOLDER FUNCTION: deletes a stone at given position
         *
         * @param stone delete reference of given stone to free position
        */
        public void DeleteStone(GameObject stone)
        {
            
        }
        
        
        /**
         * creates a stone at a random location
         */
        private void CreateRandomStone()
        {
            if (_stones.Count < maxStones)
            {
                var places = _spawnPlaces.Where(plc => !ContainsStone(plc)).ToList();
                if (places.Count > 0)
                {
                    var random = Random.Range(0, places.Count);
                    var place = places[random];
                    var spawn = place.GetComponent<SpawnPlace>();

                    var spawnPosition = place.transform.position;
                    var x = spawnPosition.x;
                    var y = spawnPosition.y;
            
                    var stone = factory.CreateStone(x, y);
                    _stones.Add(stone);
                    spawn.stone = stone;    
                }
            
            }
        }
    
        /**
         * determines, if the chosen GameObject already contains a stone
         *
         * @param place chosen spawn place for a stone
         * @returns true, if the gameObject contains a SpawnPlace and it contains a stone
         */
        private static bool ContainsStone(GameObject place)
        {
            var spawn = place.GetComponent<SpawnPlace>();
            return spawn != null && spawn.stone != null;
        }
    }
}