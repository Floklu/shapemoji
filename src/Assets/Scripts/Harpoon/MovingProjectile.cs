using UnityEngine;

namespace Harpoon
{
    /**
     * Handles the movement of the projectile
     */
    public class MovingProjectile : MonoBehaviour
    {
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
                vector = gameObject.transform.right * velocity;
            else
                vector = Vector3.zero;

            rigidBody2d.velocity = vector;
        }
    }
}