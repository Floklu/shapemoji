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
        
        private bool _windIn;
        private Collider2D _cannonCollider; //needed to better handle collision while wound in
        private HookableObject _objectHooked;
        private GameObject _inventory;
        private GameObject _projectile;


        private void Start()
        {
            _cannonCollider = gameObject.transform.Find("HarpoonCannon").gameObject.GetComponent<BoxCollider2D>();
            _projectile = gameObject.transform.Find("HarpoonCannon/HarpoonProjectile").gameObject;
            var ropeObj = gameObject.transform.Find("HarpoonCannon/HarpoonRope").gameObject;
            _crankController = gameObject.transform.Find("../../Wheel").gameObject.GetComponent<CrankController>();

            _inventory = gameObject.transform.Find("../../Inventory").gameObject;
            
            _rotatableHandler = GetComponent<RotatableHandler>();
            _shotHandler = GetComponent<HarpoonShotHandler>();
            _projectileCollision = _projectile.GetComponent<ProjectileCollision>();
            _movingProjectile = _projectile.GetComponent<MovingProjectile>();
            _rope = ropeObj.GetComponent<HarpoonRope>();
            _windInProjectile = _projectile.GetComponent<WindInProjectile>();
            _cannonCollider.enabled = false;
            
            _crankController.CrankRotationEvent += _windInProjectile.AddTravelDistance;
            _rotatableHandler.RotationEvent += OnRotationEvent;
            _shotHandler.ShotEvent += OnShotEvent;
            _projectileCollision.CollisionEvent += ProjectileOnCollisionEvent;

        }

        /**
         * shoots the projectile from the cannon
         */
        public void ShootProjectile()
        {
            _movingProjectile.SetVelocity(projectileSpeed);
            _rope.enabled = true;
            _windIn = false;
        }
        
        /**
         * stops the projectile
         */
        public void StopProjectileMovement()
        {
            if (_windIn) //Harpoon has been wound in
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
                _windIn = true;
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
        public void NotifyCollisionWithHookableObject(HookableObject hookableObject, GameObject collidedObject)
        {
            if (collidedObject == _projectile)
            {
                _objectHooked = hookableObject;
                _movingProjectile.AttachObject(hookableObject.gameObject);

            }

        }                
         
        
        #region EventHandling
        
        /**
         * implements CollisionEvent
         * @param sender sender of event
         * @param eventArg is empty
         */
        private void ProjectileOnCollisionEvent(object sender, Collider2D collidedObject)
        {
            if (collidedObject == _cannonCollider)
            {
                if (_objectHooked != null)
                {
                    _movingProjectile.UnattachObject();
                    HookableObjectController.OnWoundIn(_objectHooked,_inventory);
                }
                _objectHooked = null;
               
            }
            
            StopProjectileMovement();
            
        }
        
        /**
         * implements ShotEvent
         *
         * @param sender sender of event
         * @param eventArg is empty
         */
        private void OnShotEvent(object sender, EventArgs eventArg)
        {
            ShootProjectile();
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