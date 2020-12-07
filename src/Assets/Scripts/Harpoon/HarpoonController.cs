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
        private Collider2D _cannonCollider; //needed to better handle collision while wound in
        private CrankController _crankController;
        private Inventory _inventory;

        private bool _isWoundIn;
        private MovingProjectile _movingProjectile;
        private HookableObject _objectHooked;
        private ProjectileCollision _projectileCollision;
        private GameObject _projectileObj;
        private bool _projectileShot;
        private HarpoonRope _rope;

        private RotatableHandler _rotatableHandler;
        private HarpoonShotHandler _shotHandler;
        private WindInProjectile _windInProjectile;


        private void Start()
        {
            _cannonCollider = gameObject.transform.Find("HarpoonCannon").gameObject.GetComponent<BoxCollider2D>();
            _projectileObj = gameObject.transform.Find("HarpoonCannon/HarpoonProjectile").gameObject;
            var ropeObj = gameObject.transform.Find("HarpoonCannon/HarpoonRope").gameObject;
            _crankController = gameObject.transform.Find("../../Wheel").gameObject.GetComponent<CrankController>();

            _inventory = gameObject.transform.Find("../../Inventory").gameObject.GetComponent<Inventory>();

            _rotatableHandler = GetComponent<RotatableHandler>();
            _shotHandler = GetComponent<HarpoonShotHandler>();
            _projectileCollision = _projectileObj.GetComponent<ProjectileCollision>();
            _movingProjectile = _projectileObj.GetComponent<MovingProjectile>();
            _rope = ropeObj.GetComponent<HarpoonRope>();
            _windInProjectile = _projectileObj.GetComponent<WindInProjectile>();
            _cannonCollider.enabled = false;
            _crankController.EnableController(false);

            _crankController.CrankRotationEvent += _windInProjectile.AddTravelDistance;
            _rotatableHandler.RotationEvent += OnRotationEvent;
            _shotHandler.ShotEvent += OnShotEvent;
            _projectileCollision.CollisionEvent += ProjectileOnCollisionEvent;
            HookableObjectController.AddHarpoonController(this);
        }

        /**
         * shoots the projectile from the cannon
         */
        public void ShootProjectile()
        {
            _movingProjectile.SetVelocity(projectileSpeed);
            _rope.enabled = true;
            _isWoundIn = false;
            _rotatableHandler.enabled = false;
            _projectileShot = true;
        }

        /**
         * stops the projectile
         */
        public void StopProjectileMovement()
        {
            if (_isWoundIn) //Harpoon has been wound in
            {
                ResetCannon();
            }
            else //Harpoon hasn't been wound in
            {
                _cannonCollider.enabled = true;
                _movingProjectile.SetVelocity(0);

                _crankController.EnableController(true);

                //prepare windInProjectile functionality
                _windInProjectile.ResetProjectile();
                _windInProjectile.TravelSpeed = projectileSpeed;
                _isWoundIn = true;
            }
        }

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

            _projectileShot = false;
            _isWoundIn = false;
        }

        /**
         * rotates Harpoon to certain degree
         *
         * @param rotation rotates object to chosen degree
         */
        public void RotateHarpoon(float rotation)
        {
            if (!_projectileShot && !_isWoundIn) transform.rotation = Quaternion.Euler(0, 0, rotation);
        }

        /**
         * called on collision of HookableObject
         *
         * returns false if an object is already hooked
         * 
         * @param hookableObject: object which collided
         * @param projectile which had collision
        */
        public bool NotifyCollisionWithHookableObject(HookableObject hookableObject, GameObject collidedObject)
        {
            if (collidedObject.Equals(_projectileObj) && _objectHooked == null)
            {
                _objectHooked = hookableObject;
                return true;
            }

            return false;
        }

        public void NotifyRemoveHookableObject(HookableObject hookableObject)
        {
            if (hookableObject.Equals(_objectHooked)) _objectHooked = null;
        }

        #region EventHandling

        /**
         * implements CollisionEvent
         * @param sender sender of event
         * @param eventArg is empty
         */
        private void ProjectileOnCollisionEvent(object sender, Collider2D collidedObject)
        {
            if (_isWoundIn)
            {
                if (collidedObject.Equals(_cannonCollider))
                {
                    StopProjectileMovement();
                    if (_objectHooked != null) HookableObjectController.OnWoundIn(_objectHooked, _inventory);
                }
            }
            else
            {
                StopProjectileMovement();
            }
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