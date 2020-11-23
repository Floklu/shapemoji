using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
    /**
     * Factory Class, which spawns Stones onto the playground
     */
    public class HookableGameObjectFactory : MonoBehaviour
    {
        public List<GameObject> preFabsStones;
        public List<GameObject> preFabsItems;
        
        /**
         * creates random stone from selection of prefabs and instantiates stone on the playground
         * 
         * @param x X-Position on the playground
         * @param y Y-Position on the playground
         * @returns GameObject of a Stone
         */
        public GameObject CreateStone(float x, float y)
        {
            var random = Random.Range(0, preFabsStones.Count);
            var preFabStone = preFabsStones[random];
            var spawnPosition = new Vector3(x, y, 0);
            var stone = Instantiate(preFabStone, spawnPosition, Quaternion.identity);
            
            stone.AddComponent(typeof(Stone));
            return stone; 
        }

        public GameObject CreateItem(float x, float y)
        {
            throw new NotImplementedException();
        }
    }
}