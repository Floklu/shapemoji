using System;
using UnityEngine;

namespace Harpoon
{
    /**
     * manages the behaviour of the crank
     */
    public class CrankController : MonoBehaviour
    {
        /**
         * defines the distance of the wind-in-operation
         */
        public float rangePerRevolution = 10f;

        private RotatableHandler _rotatableHandler;
        private float _rotation;

        private void Start()
        {
            _rotatableHandler = GetComponent<RotatableHandler>();
            _rotatableHandler.RotationEvent += OnRotationEvent;
        }

        /**
         * Handles RotationEvents
         *
         * @param sender sender of the event
         * @param rotation degree of the rotation
         */
        private void OnRotationEvent(object sender, float rotation)
        {
            RotateCrank(rotation);
        }

        /**
         * Rotates the Crank to certain degree
         *
         * @param rotation degree of rotation
         */
        public void RotateCrank(float rotation)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotation);

            var windInRange = 0f;

            if (_rotation > rotation)
            {
                windInRange = (360 - _rotation) / 360 * rangePerRevolution;
                _rotation = 0;
            }

            windInRange += (rotation - _rotation) / 360 * rangePerRevolution;
            OnCrankRotationEvent(windInRange);

            _rotation = rotation;
        }

        /**
         * enables MonoBehaviour for Controller
         *
         * @param status bool to enable (true) or disable (false) Controller
         */
        public void EnableController(bool status)
        {
            enabled = status;
            if (_rotatableHandler != null) _rotatableHandler.enabled = status;
        }

        #region Events

        /**
         * Raises CrankRotationEvent
         */
        public event EventHandler<float> CrankRotationEvent;

        /**
         * invokes CrankRotationEvent
         *
         * @param windInRange distance of the wind in operation for the projectile
         */
        private void OnCrankRotationEvent(float windInRange)
        {
            CrankRotationEvent?.Invoke(this, windInRange);
        }

        #endregion
    }
}