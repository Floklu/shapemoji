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

        private SpriteRenderer _spriteRenderer;

        private Transform _ropeTransform;
        private Transform _cannonTransform;
        private Transform _projectileTransform;

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
            transform.localScale = ropeScale;
            _ropeTransform.position = ropeCenter;
        }

    }
}