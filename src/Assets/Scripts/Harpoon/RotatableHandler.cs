using System;
using System.Collections.Generic;
using System.Linq;
using Lean.Touch;
using UnityEngine;

namespace Harpoon
{
    /**
     * Handles the interaction with rotatable gameObjects
     */
    public class RotatableHandler : MonoBehaviour
    {
        // Determines, whether RotationEvent passes differences in degrees since last event (true) or the total degree (false) 
        [SerializeField] private bool newRotationBehaviour;

        private LeanFinger _finger;
        private Vector3 _initialPosition;
        private Camera _mainCamera;
        private bool _onDrag;

        private LeanFingerFilter Use = new LeanFingerFilter(true);

        protected virtual void Awake()
        {
            Use.UpdateRequiredSelectable(gameObject);
        }

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            Use.UpdateRequiredSelectable(gameObject);
        }
#endif

        /**
        * Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        */
        private void Start()
        {
            _mainCamera = Camera.main;
        }

        /**
         * processes the touch movements and
         */
        private void Update()
        {
            var fingers = Use.GetFingers();
            if (fingers.Count > 0)
            {
                var finger = fingers[0];
                if (!newRotationBehaviour)
                {
                    var position = finger.GetWorldPosition(0, _mainCamera);
                    var direction = transform.position - position;

                    direction.Normalize();

                    var rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                    OnRotationEvent(rotationZ);
                }
                else
                {
                    OnRotationEvent(finger.GetDeltaDegrees(_mainCamera.WorldToScreenPoint(transform.position)));
                }
            }
        }

        /**
         * invokes Event, if rotation happens
         */
        public event EventHandler<float> RotationEvent;

        /**
         * raises RotationEvent
         */
        private void OnRotationEvent(float e)
        {
            RotationEvent?.Invoke(this, e);
        }
    }
}