using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harpoon
{
    public class HarpoonWoundInHandler : MonoBehaviour
    {
        private Collider2D _collider;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.transform.parent.gameObject == this.gameObject)
            {
                OnWoundInEvent();
                Debug.Log("WoundIn Trigger");
            }
            
        }
        
        #region Events

        /**
         * invokes CollisionEvent, if harpoon collides with cannon collider
         */
        public event EventHandler CollisionEvent;
        
        /**
         * raises Event CollisionEvent
         */
        protected virtual void OnWoundInEvent()
        {
            CollisionEvent?.Invoke(this, EventArgs.Empty);
        }
        
        #endregion
        
    }
}

