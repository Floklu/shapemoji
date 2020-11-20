using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    /**
     * Factory Class, which spawns Stones onto the playground
     */
    public class StoneFactory : MonoBehaviour
    {
        public List<GameObject> preFabs;

        /**
         * creates random stone from selection of prefabs and instantiates stone on the playground
         * 
         * @param x X-Position on the playground
         * @param y Y-Position on the playground
         * @returns GameObject of a Stone
         */
        public GameObject CreateStone(float x, float y)
        {
            var random = Random.Range(0, preFabs.Count);
            var preFabStone = preFabs[random];
            var spawnPosition = new Vector3(x, y, 0);

            var stone = Instantiate(preFabStone, spawnPosition, Quaternion.identity);
            //TODO: stone.addComponent(StoneObject)
            return stone;
        }
    }
}