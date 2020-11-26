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
            var preFabsStonesRange = preFabsStones.Count;
            //if no prefabs are available
            if (preFabsStonesRange < 1)
            {
                throw new IndexOutOfRangeException();
            }

            //choose prefab
            var random = Random.Range(0, preFabsStonesRange);
            var preFab = preFabsStones[random];
            //create stone
            var stone = CreateHookableGameObject(preFab, x, y);
            stone.AddComponent(typeof(Stone));
            return stone;
        }

        /**
         * creates random item from selection of prefabs and instantiates item on the playground
         * 
         * @param x X-Position on the playground
         * @param y Y-Position on the playground
         * @returns GameObject of an Item
         */
        public GameObject CreateItem(float x, float y)
        {
            var preFabItemsRange = preFabsItems.Count;
            //if no prefabs are available 
            if (preFabItemsRange < 1)
            {
                throw new IndexOutOfRangeException();
            }

            //choose preFab
            var random = Random.Range(0, preFabItemsRange);
            var preFab = preFabsItems[random];

            //create item
            var item = CreateHookableGameObject(preFab, x, y);
            item.AddComponent(typeof(Item));

            return item;
        }

        /**
         * creates HookableGameObject with given PreFab at given position at the playground
         *
         * @param preFab PreFab used for HookableGameObject returned
         * @param x X-Position on the playground
         * @param y Y-Position on the playground
         * @returns GameObject of a Stone
         */
        private GameObject CreateHookableGameObject(GameObject preFab, float x, float y)
        {
            var spawnPosition = new Vector3(x, y, 0);
            return Instantiate(preFab, spawnPosition, Quaternion.identity);
        }
    }
}