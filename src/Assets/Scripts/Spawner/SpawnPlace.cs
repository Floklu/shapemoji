using UnityEngine;
using UnityEngine.Serialization;

namespace Spawner
{
    /**
 * Class, which describes a place, where a random stone can be spawned
 */
    public class SpawnPlace : MonoBehaviour
    {
        //keeps track of a stone
        public HookableObject hookableObject;

        /**
         * checks, if spawn place contains Stone
         *
         * @return true, if place contains Object and it is a stone
         */
        public bool ContainsStone()
        {
            return hookableObject != null && hookableObject.IsStone();
        }

        /**
         * checks, if spawn place contains item
         *
         * @return true, if place contains an object and it is an item
         */
        public bool ContainsItem()
        {
            return hookableObject != null && !hookableObject.IsStone();
        }
    }
}