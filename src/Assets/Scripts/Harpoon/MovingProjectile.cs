using UnityEngine;

namespace Harpoon
{
    /**
     * Handles the movement of the projectile
     */
    public class MovingProjectile : MonoBehaviour
    {
        private GameObject _gameObjectHooked;

        /**
         * sets the velocity of the projectile
         *
         * @param velocity value of the velocity of the flying projectile
         */
        public void SetVelocity(float velocity)
        {
            Vector3 vector;
            var rigidBody2d = gameObject.GetComponent<Rigidbody2D>();

            if (velocity != 0)
            {
                vector = gameObject.transform.right * velocity;
            }
            else
            {
                vector = Vector3.zero;
            }

            rigidBody2d.velocity = vector;
        }

        /**
        *  sets parent to be classes gameObject
        *
        * @param child object set to be child of gameObject 
        */
        public void AttachObject(GameObject child)
        {
            _gameObjectHooked = child;
            _gameObjectHooked.transform.parent = gameObject.transform;
        }

        /**
        * unattaches hooked Object from projectile 
        */
        public void UnattachObject()
        {
            _gameObjectHooked.transform.parent = null;
        }
        
    }
}