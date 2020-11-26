using System.Collections;
using Harpoon;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
// ReSharper disable Unity.InefficientPropertyAccess, tells Rider to ignore Property Access Issues (okay for Test)

namespace Tests.PlayMode
{
    /**
     * Class, which tests projectile functionality
     *
     * @author Florian Kluth
     * @date   17.11.2020
     */
    public class ProjectileTest
    {
        private GameObject _projectile;
        //private GameObject _harpoon;
        private HarpoonController _harpoonController;
        private CrankController _crankController;


        /**
         * LoadScene loads the Scene ones for every Test in ProjectileTest.
         */
        [OneTimeSetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("Scenes/Scene_Playground_2vs2");
        }

        /**
         * ShootProjectileTest tests if the Projectile moves after being shot.
         */
        [UnityTest]
        public IEnumerator ShootProjectileTest()
        {
            LoadProjectile();
            LoadHarpoonController();
            var location = _projectile.transform.position;
            
            _harpoonController.ShootProjectile();

            yield return new WaitForSeconds(1);

            Assert.AreNotEqual(_projectile.transform.position, location);
        }

        private void LoadHarpoonController()
        {
            _harpoonController = GameObject.Find("Harpoon").GetComponent<HarpoonController>();
        }

        private void LoadProjectile()
        {
            _projectile = GameObject.Find("HarpoonProjectile");
        }
        private void LoadCrankController()
        {
            _crankController = GameObject.Find("Wheel").GetComponent<CrankController>();
        }

        /**
         * Tests, if the projectile will be wound in after interaction with the crank
         *
         * @returns true, if position after crank interaction changed after 1 second
         */
        [UnityTest]
        public IEnumerator TestWindInAfterProjectile()
        {
            LoadProjectile();
            LoadHarpoonController();
            LoadCrankController();
            var location = _projectile.transform.position;
            
            _harpoonController.ShootProjectile();

            yield return new WaitForSeconds(1);

            _harpoonController.StopProjectileMovement();
            var locationNew = _projectile.transform.position;
            Assert.AreNotEqual(_projectile.transform.position, location, "Projectile has not been moved since 1 second");
            yield return new WaitForSeconds(1);
            Assert.AreNotEqual(_projectile.transform.position, location,
                "Projectile continues after harpoon has been stopped"); 

            _crankController.RotateCrank(180);

            yield return new WaitForSeconds(1);

            Assert.AreNotEqual(_projectile.transform.position, locationNew, "Projectile has not been properly wound in");

        }


        /**
         * Tests if the harpoon is properly wound in and ready for the next shot
         *
         * @returns true, if harpoon can shoot a second time
         */
        [UnityTest]
        public IEnumerator TestShootAfterWindIn()
        {
            LoadProjectile();
            LoadHarpoonController();
            LoadCrankController();
            
            var location = _projectile.transform.position;
            _harpoonController.ShootProjectile();
            yield return new WaitForSeconds(1);

            _harpoonController.StopProjectileMovement();
            var locationNew = _projectile.transform.position;
            Assert.AreNotEqual(_projectile.transform.position, location, "Projectile has not been moved since 1 second");
            yield return new WaitForSeconds(1);
            Assert.AreNotEqual(_projectile.transform.position, location,
                "Projectile continues after harpoon has been stopped");

            for (var i = 0; i < 100; i++)
            {
                _crankController.RotateCrank(180);
                _crankController.RotateCrank(0);
            }
            yield return new WaitForSeconds(5);
            Assert.AreNotEqual(_projectile.transform.position, locationNew, "Projectile has not been properly wound in");
            
            // Projectile should have returned to harpoon
            location = _projectile.transform.position;
            _harpoonController.ShootProjectile();
            yield return new WaitForSeconds(1);

            Assert.AreNotEqual(_projectile.transform.position, location, "Second shot wasn't successful");
        }
        
        /**
         * ShootProjectileAndRotateHarpoonTest tests if the harpoon permits rotations after the projectile was shot.
         *
         * @returns successful if alignment after rotation hasn't changed
         */
        [UnityTest]
        public IEnumerator ShootProjectileAndRotateHarpoonTest()
        {
            LoadProjectile();
            LoadHarpoonController();
            
            _harpoonController.ShootProjectile();
            var rotationProjectile = _projectile.transform.rotation.eulerAngles.z;
            yield return new WaitForSeconds(1);
            _harpoonController.RotateHarpoon(rotationProjectile+90);
            yield return new WaitForSeconds(1);
            Assert.AreEqual(rotationProjectile, _projectile.transform.rotation.eulerAngles.z, 0.1f, "Harpoon rotated after cannon has been shot");
        }
    }
}