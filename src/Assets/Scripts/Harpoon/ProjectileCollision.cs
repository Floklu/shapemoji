using System;
using UnityEngine;

namespace Harpoon
{
    /**
     * Handles the collision of the projectile and communicates collisions over custom Event
     */
    public class ProjectileCollision : MonoBehaviour
    {
       
        /**
        * OnTriggerEnter2D is called when the collider enters a trigger
        *
        * Gets called on collision of Projectile with GameObj other. Stops projectile and 
        * hooks GameObj if stone or item.
        *
        * @param other Other Collider that got hit.
        */
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnCollisionEvent();
        }
        
        #region Events

        /**
         * invokes CollisionEvent, if the gameObjects collides with other Object
         */
        public event EventHandler CollisionEvent;
        
        /**
         * raises Event CollisionEvent
         */
        protected virtual void OnCollisionEvent()
        {
            CollisionEvent?.Invoke(this, EventArgs.Empty);
        }
        
        #endregion

        
        
    }
}