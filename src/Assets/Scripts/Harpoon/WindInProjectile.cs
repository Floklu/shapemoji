using UnityEngine;

namespace Harpoon
{
    /**
     * describes behaviour for travel of the projectile from collision to harpoon base
     */
    public class WindInProjectile : MonoBehaviour
    {
        private float _travelDistance;

        /**
         * manages TravelSpeed of projectile
         */
        public float TravelSpeed { get; set; }

        /**
         * refreshes projectile position every frame
         */
        private void Update()
        {
            var projectile = transform;
            var distanceTraveled = TravelSpeed * Time.deltaTime;
            if (_travelDistance < distanceTraveled)
            {
                enabled = false;
                distanceTraveled = -_travelDistance;
            }

            _travelDistance -= distanceTraveled;
            projectile.position -= projectile.right * distanceTraveled;
        }

        /**
         * adds given range to current travel range
         *
         * @param distance distance
         */
        public void AddTravelDistance(object sender, float distance)
        {
            enabled = true;
            _travelDistance += distance;
        }

        public void ResetProjectile()
        {
            _travelDistance = 0;
            TravelSpeed = 0;
        }
    }
}