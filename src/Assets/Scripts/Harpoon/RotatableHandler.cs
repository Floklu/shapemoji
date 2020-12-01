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
        private bool _onDrag;
        private Vector3 _initialPosition;
        private Camera _mainCamera;
        private Collider2D _collider;

        private LeanFinger _finger;

        // Determines, whether RotationEvent passes differences in degrees since last event (true) or the total degree (false) 
        [SerializeField] private bool newRotationBehaviour = false;

        /**
        * Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        */
        private void Start()
        {
            _collider = GetComponent<Collider2D>();
            _mainCamera = Camera.main;
        }

        #region LeanTouchEvents

        /**
         * Handle LeanTouch OnFingerUp Event
         *
         * @param finger selected touch point
         */
        private void OnFingerUp(LeanFinger finger)
        {
            if (_finger != null && _finger.Equals(finger)) _finger = null;
        }

        /**
         * Processes LeanTouch Finger Gestures
         *
         * @param lstFinger List of currently active LeanFinger Objects
         */
        private void OnGesture(List<LeanFinger> lstFinger)
        {
            var fingers = lstFinger.Where(leanFinger => leanFinger.Equals(_finger));
            foreach (var leanFinger in fingers)
            {
                if (!leanFinger.ScreenDelta.normalized.Equals(Vector2.zero))
                {
                    ProcessTouchMovement(leanFinger);
                }
            }
        }

        /**
         *  Handle LeanTouch OnFingerDown Event
         *
         *  @param finger selected touch point
         */
        private void OnFingerDown(LeanFinger finger)
        {
            if (finger.CollidesWithGameObject(_collider, _mainCamera))
            {
                _finger = finger;
                _initialPosition = finger.GetWorldPosition(0, _mainCamera);
            }
        }

        #endregion

        /**
         * Fired if behaviour is enabled
         */
        private void OnEnable()
        {
            LeanTouch.OnFingerDown += OnFingerDown;
            LeanTouch.OnGesture += OnGesture;
            LeanTouch.OnFingerUp += OnFingerUp;
        }

        /**
         * Fired if behaviour is disabled
         */
        private void OnDisable()
        {
            LeanTouch.OnFingerDown -= OnFingerDown;
            LeanTouch.OnGesture -= OnGesture;
            LeanTouch.OnFingerUp -= OnFingerUp;
        }

        /**
         * processes the touch movements and 
         */
        private void ProcessTouchMovement(LeanFinger finger)
        {
            if (_finger.Equals(finger))
            {
                if (!newRotationBehaviour)
                {
                    var position = finger.GetWorldPosition(0, _mainCamera);
                    var direction = _initialPosition - position;

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