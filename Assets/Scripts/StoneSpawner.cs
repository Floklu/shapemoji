using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * Class, which manages the avaiable stones on the playground
 */
public class StoneSpawner : MonoBehaviour
{
    public int MaxStones;

    public StoneFactory factory;
    public List<GameObject> spawnZones;

    private List<GameObject> _stones;
    private System.Random _randomizer;
    private List<GameObject> _spawnPlaces;

    // Start is called before the first frame update
    void Start()
    {
        _stones = new List<GameObject>();
        _randomizer = new System.Random();
        _spawnPlaces = new List<GameObject>();

        foreach (GameObject zone in spawnZones)
        {
            foreach (Transform child in zone.transform)
            {
                _spawnPlaces.Add(child.gameObject);
            }
        }
        
        InvokeRepeating(nameof(CreateRandomStone), 1f, 1f);
        for (int i = 0; i < MaxStones; i++)
        {
            CreateRandomStone();
        }
    }

    /*
     * creates a stone at a random location
     */
    void CreateRandomStone()
    {
        if (_stones.Count < MaxStones)
        {
            List<GameObject> places = _spawnPlaces.Where(plc => !containsStone(plc)).ToList();
            if (places.Count > 0)
            {
                float x, y;
                GameObject place = places[_randomizer.Next(places.Count)];
                SpawnPlace spawn = place.GetComponent<SpawnPlace>();

                Vector3 spawnPosition = place.transform.position;
                x = spawnPosition.x;
                y = spawnPosition.y;
            
                GameObject stone = factory.CreateStone(x, y);
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
    private bool containsStone(GameObject place)
    {
        SpawnPlace spawn = place.GetComponent<SpawnPlace>();
        return spawn != null && spawn.stone != null;
    }
}
