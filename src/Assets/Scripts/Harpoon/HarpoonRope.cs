using System;
using UnityEngine;

namespace Harpoon
{
    /**
 * @brief This Class is used to stretch the Rope
 * @Author Andrei Dziubenka
 * @Date 11.11.2020
 */
    public class HarpoonRope : MonoBehaviour
    {
        public GameObject projectile;
        public GameObject cannon;
        private Transform _cannonTransform;
        private Transform _projectileTransform;

        private Transform _ropeTransform;

        private SpriteRenderer _spriteRenderer;

        /**
        * Start Method is used to initialize variables
        */
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _ropeTransform = transform;
            _cannonTransform = cannon.transform;
            _projectileTransform = projectile.transform;
        }

        /**
        * Update Method is called every frame
        * Scales the rope according to projectile position
        */
        private void Update()
        {
            var ropeCenter = (cannon.transform.position + projectile.transform.position) / 2f;
            var ropeScale = _ropeTransform.localScale;

            var requiredRopeLength = Vector3.Distance(_cannonTransform.position, _projectileTransform.position);
            var currentRopeLength = _spriteRenderer.bounds.size.magnitude;

            ropeScale.x *= requiredRopeLength / currentRopeLength;
            _ropeTransform.position = ropeCenter;

            if (ropeScale.x < 0.0001f)
            {
                ropeScale.x = 0.0001f;
            }
            
            transform.localScale = ropeScale;
        }
    }
}