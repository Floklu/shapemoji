using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class, which manages the avaiable stones on the playground
 */
public class StoneSpawner : MonoBehaviour
{

    private int MAX_STONES = 20;
    
    public StoneFactory factory;
    public GameObject spawnArea;

    private List<GameObject> _stones;
    private System.Random _randomizer;

    // Start is called before the first frame update
    void Start()
    {
        _stones = new List<GameObject>();
        _randomizer = new System.Random();
        InvokeRepeating(nameof(CreateRandomStone), 1f, 1f);
        for (int i = 0; i < 10; i++)
        {
            CreateRandomStone();
        }
    }

    /*
     * creates a stone at a random location
     */
    void CreateRandomStone()
    {
        if (_stones.Count < MAX_STONES)
        {
            Vector3 spawnPosition = spawnArea.transform.position;
            Vector3 spawnBounds = spawnArea.transform.localScale;
            
            float x = spawnPosition.x - (spawnBounds.x / 2);
            float y = spawnPosition.y - (spawnBounds.y / 2);
            
            x = x + (float)(_randomizer.NextDouble()) * spawnBounds.x;
            y = y + (float)(_randomizer.NextDouble()) * spawnBounds.y;
            
            GameObject stone = factory.CreateStone(x, y);
            _stones.Add(stone);
        }
    }
}
