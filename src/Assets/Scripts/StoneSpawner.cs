using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

/**
 * Class, which manages the avaiable stones on the playground
 */
public class StoneSpawner : MonoBehaviour
{
    [FormerlySerializedAs("MaxStones")] public int maxStones;

    public StoneFactory factory;
    public List<GameObject> spawnZones;

    private List<GameObject> _stones;
    private Random _randomizer;
    private List<GameObject> _spawnPlaces;

    private void Start()
    {
        _stones = new List<GameObject>();
        _randomizer = new Random();
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

    /*
     * creates a stone at a random location
     */
    private void CreateRandomStone()
    {
        if (_stones.Count < maxStones)
        {
            var places = _spawnPlaces.Where(plc => !ContainsStone(plc)).ToList();
            if (places.Count > 0)
            {
                var place = places[_randomizer.Next(places.Count)];
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
