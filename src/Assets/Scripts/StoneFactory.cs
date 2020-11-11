using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

/**
 * Factory Class, which spawns Stones onto the playground
 */
public class StoneFactory: MonoBehaviour
{

    public List<GameObject> preFabs;

    private readonly Random _randomizer = new Random();


    /**
     * creates random stone from selection of prefabs and instantiates stone on the playground
     * 
     * @param x X-Position on the playground
     * @param y Y-Position on the playground
     * @returns GameObject of a Stone
     */
    public GameObject CreateStone(float x, float y)
    {
        var newStone = preFabs[_randomizer.Next(preFabs.Count)];
        var spawnPosition = new Vector3(x, y, 0);

        Instantiate(newStone, spawnPosition, Quaternion.identity);
        
        return newStone;
    }
}
