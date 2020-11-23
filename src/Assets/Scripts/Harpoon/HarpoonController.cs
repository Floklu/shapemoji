using System;
using Spawner;
using UnityEngine;
using UnityEngine.Serialization;

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

        
        private void Start()
        {
            var projectile = gameObject.transform.Find("HarpoonCannon/HarpoonProjectile").gameObject;
            var ropeObj = gameObject.transform.Find("HarpoonCannon/HarpoonRope").gameObject;
            
            _rotatableHandler = GetComponent<RotatableHandler>();
            _shotHandler = GetComponent<HarpoonShotHandler>();
            _projectileCollision = projectile.GetComponent<ProjectileCollision>();
            _movingProjectile = projectile.GetComponent<MovingProjectile>();
            _rope = ropeObj.GetComponent<HarpoonRope>();
            
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
        }
        
        /**
         * stops the projectile
         */
        public void StopCannon()
        {
            _movingProjectile.SetVelocity(0);
            _rope.enabled = false;
            _rotatableHandler.enabled = false;
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