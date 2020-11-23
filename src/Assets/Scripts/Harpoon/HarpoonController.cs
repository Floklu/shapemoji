using System;
using UnityEngine;

namespace Harpoon
{
    /**
    * HarpoonController contains the functionality of the Harpoon
    */
    public class HarpoonController : MonoBehaviour
    {
        public float projectileSpeed = 500;
        
        private RotatableHandler _rotatableHandler;
        private HarpoonShotHandler _shotHandler;
        private ProjectileCollision _projectileCollision;
        private MovingProjectile _movingProjectile;
        private HarpoonRope _rope;
        private WindInProjectile _windInProjectile;
        private CrankController _crankController;
        
        private bool _woundIn = true;
        private BoxCollider2D _cannonCollider; //needed to better handle collision while wound in


        private void Start()
        {
            _cannonCollider = gameObject.transform.Find("HarpoonCannon").gameObject.GetComponent<BoxCollider2D>();
            var projectile = gameObject.transform.Find("HarpoonCannon/HarpoonProjectile").gameObject;
            var ropeObj = gameObject.transform.Find("HarpoonCannon/HarpoonRope").gameObject;
            _crankController = gameObject.transform.Find("../../Wheel").gameObject.GetComponent<CrankController>();

            _rotatableHandler = GetComponent<RotatableHandler>();
            _shotHandler = GetComponent<HarpoonShotHandler>();
            _projectileCollision = projectile.GetComponent<ProjectileCollision>();
            _movingProjectile = projectile.GetComponent<MovingProjectile>();
            _rope = ropeObj.GetComponent<HarpoonRope>();
            _windInProjectile = projectile.GetComponent<WindInProjectile>();
            _crankController.CrankRotationEvent += _windInProjectile.AddTravelDistance;
            
            _rotatableHandler.RotationEvent += OnRotationEvent;
            _shotHandler.ShotEvent += OnShotEvent;
            _projectileCollision.CollisionEvent += ProjectileOnCollisionEvent;
        }

        /**
         * shoots the projectile from the cannon
         */
        public void Shoot()
        {
            _movingProjectile.SetVelocity(projectileSpeed);
            _rope.enabled = true;
            _woundIn = false;
        }
        
        /**
         * stops the projectile
         */
        public void StopCannon()
        {
            if (_woundIn) //Harpoon has been wound in
            {
                ResetCannon();
                //TODO handle WoundIn specific behaviour
            }
            else //Harpoon hasn't been wound in
            {
                _cannonCollider.enabled = true;
                _rotatableHandler.enabled = false;
                _movingProjectile.SetVelocity(0);

                _crankController.EnableController(true);
                
                //prepare windInProjectile functionality
                _windInProjectile.ResetProjectile();
                _windInProjectile.TravelSpeed = projectileSpeed;
                _woundIn = true;
            }
            
        }

        //ReSharper disable once UnusedMember.Global
        //TODO used when projectile is wound in
        /**
         * resets Cannon behaviour
         */
        public void ResetCannon()
        {
            _rotatableHandler.enabled = true;
            _rope.enabled = false;
            _windInProjectile.TravelSpeed = 0;
            _windInProjectile.enabled = false;
            _shotHandler.ResetHandler();
            _windInProjectile.ResetProjectile();
            _cannonCollider.enabled = false;
            _crankController.EnableController(false);
        }

        /**
         * rotates Harpoon to certain degree
         *
         * @param rotation rotates object to chosen degree
         */
        public void RotateHarpoon(float rotation)
        {
            transform.rotation = Quaternion.Euler(0,0,rotation);
        }

        /**
         * called on collision of HookableObject
         * 
         * @param hookableObject: object which collided
         * @param projectile which had collision
        */
        public void NotifyCollisionWithHookableObject(HookableObject hookableObject, GameObject projectile)
        {
            throw new NotImplementedException();
        }                
         
        
        #region EventHandling
        
        /**
         * implements CollisionEvent
         * @param sender sender of event
         * @param eventArg is empty
         */
        private void ProjectileOnCollisionEvent(object sender, EventArgs eventArg)
        {
            StopCannon();
        }
        
        /**
         * implements ShotEvent
         *
         * @param sender sender of event
         * @param eventArg is empty
         */
        private void OnShotEvent(object sender, EventArgs eventArg)
        {
            Shoot();
        }

        /**
         * implements RotationEvent
         *
         * @param sender sender of event
         * @param eventArg is empty
         */
        private void OnRotationEvent(object sender, float rotation)
        {
            RotateHarpoon(rotation);
        }
        
        #endregion
    }
}