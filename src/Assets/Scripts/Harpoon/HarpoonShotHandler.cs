using System;
using UnityEngine;

namespace Harpoon
{
    /**
     * handles interactions for shooting the harpoon
     */
    public class HarpoonShotHandler : MonoBehaviour
    {
        private bool _isShot;

        /**
        * Triggers ShotEvent
        */
        private void OnMouseUp()
        {
            if (!_isShot)
            {
                OnShotEvent();
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