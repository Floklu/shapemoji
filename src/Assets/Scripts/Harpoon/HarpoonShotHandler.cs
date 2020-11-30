using System;
using Lean.Touch;
using UnityEngine;

namespace Harpoon
{
    /**
     * handles interactions for shooting the harpoon
     */
    public class HarpoonShotHandler : MonoBehaviour
    {
        private bool _isShot;
        private Collider2D _collider;
        private LeanFinger _finger;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _collider = GetComponent<Collider2D>();
            LeanTouch.OnFingerDown += OnFingerDown;
            LeanTouch.OnFingerUp += OnFingerUp;
        }

        /**
         * Handles OnFingerDown Event of LeanTouch
         *
         * @param finger selected touch point
         */
        private void OnFingerDown(LeanFinger finger)
        {
            if (!finger.CollidesWithGameObject(_collider, _camera)) return;
            _finger = finger;
        }

        /**
         * Handles OnFingerUp Event of LeanTouch
         *
         * @param finger selected touch point
         */
        private void OnFingerUp(LeanFinger finger)
        {
            if (finger.Equals(_finger))
            {
                if (!_isShot)
                {
                    _finger = null;
                    OnShotEvent();
                }                
            }
        }

        /**
         * Resets HarpoonShotHandler
         */
        public void ResetHandler()
        {
            _isShot = false;
        }

        #region Events

        /**
         * invokes event, when Harpoon is shot
         */
        public event EventHandler ShotEvent;

        /**
         * raises ShotEvent
         */
        private void OnShotEvent()
        {
            _isShot = true;
            ShotEvent?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}