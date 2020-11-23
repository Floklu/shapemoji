using System;
using UnityEngine;

namespace Harpoon
{
    /**
     * manages the behaviour of the crank
     */
    public class CrankController : MonoBehaviour
    {

        private RotatableHandler _rotatableHandler;
        private float _rotation;

        private void Start()
        {
            _rotatableHandler = GetComponent<RotatableHandler>();
            _rotatableHandler.RotationEvent += RotatableHandlerOnRotationEvent;
            _rotation = 0; 
        }

        private void RotatableHandlerOnRotationEvent(object sender, float rotation)
        {
            RotateCrank(rotation);
            _rotation = rotation;
        }
        
        public void RotateCrank(float rotation)
        {
            transform.rotation = Quaternion.Euler(0,0,rotation);
        }
    }
}