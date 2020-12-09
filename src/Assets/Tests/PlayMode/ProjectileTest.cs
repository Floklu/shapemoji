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
        private HarpoonController _harpoonController;
        private CrankController _crankController;
        private const int MAX_PLAYERS = 4; 


        /**
         * LoadScene loads the Scene ones for every Test in ProjectileTest.
         */
        [OneTimeSetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("Scenes/Scene_Playground_2vs2");
        }
        
        /**
         * Prepare testing for given player
         *
         * @param player number of player 
         */
        private void LoadPlayer(int player)
        {
            var team = (player + 1) / 2;
            _harpoonController = GameObject.Find($"Teams/Team_{team}/Player_{player}/Base/HarpoonBase/Harpoon").GetComponent<HarpoonController>();
            _projectile = GameObject.Find($"Teams/Team_{team}/Player_{player}/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");
            _crankController = GameObject.Find($"Teams/Team_{team}/Player_{player}/Base/Wheel").GetComponent<CrankController>();
        }

        /**
         * ShootProjectileTest tests if the Projectile moves after being shot.
         */
        [UnityTest]
        public IEnumerator ShootProjectileTest()
        {
            for (var player = 1; player <= MAX_PLAYERS; player++)
            {
                LoadPlayer(player);
                var location = _projectile.transform.position;
                _harpoonController.ShootProjectile();
                yield return new WaitForSeconds(1);
                Assert.AreNotEqual(_projectile.transform.position, location, $"Player {player}: Projectile didn't move after shot");
            }
        }
        
        /**
         * Tests, if the projectile will be wound in after interaction with the crank
         *
         * @returns true, if position after crank interaction changed after 1 second
         */
        [UnityTest]
        public IEnumerator TestWindInAfterProjectile()
        {
            for (var player = 1; player <= MAX_PLAYERS; player++)
            {
                LoadPlayer(player);
                var location = _projectile.transform.position;
                _harpoonController.ShootProjectile();
                yield return new WaitForSeconds(0.5f);

                _harpoonController.StopProjectileMovement();
                var locationNew = _projectile.transform.position;
                Assert.AreNotEqual(_projectile.transform.position, location, $"Player {player}: Projectile has not been moved since 1 second");
                yield return new WaitForSeconds(1);
                Assert.AreNotEqual(_projectile.transform.position, location,
                    $"Player {player}: Projectile continues after harpoon has been stopped"); 

                _crankController.RotateCrank(180);
                yield return new WaitForSeconds(1);
                Assert.AreNotEqual(_projectile.transform.position, locationNew, $"Player {player}: Projectile has not been properly wound in");
            }
        }


        /**
         * Tests if the harpoon is properly wound in and ready for the next shot
         *
         * @returns true, if harpoon can shoot a second time
         */
        [UnityTest]
        public IEnumerator TestShootAfterWindIn()
        {
            for (var player = 1; player <= MAX_PLAYERS; player++)
            {
                LoadPlayer(player);
            
                var location = _projectile.transform.position;
                _harpoonController.ShootProjectile();
                yield return new WaitForSeconds(0.5f);

                _harpoonController.StopProjectileMovement();
                var locationNew = _projectile.transform.position;
                Assert.AreNotEqual(_projectile.transform.position, location, $"Player {player}: Projectile has not been moved since 1 second");
                yield return new WaitForSeconds(1);
                Assert.AreNotEqual(_projectile.transform.position, location,
                    $"Player {player}: Projectile continues after harpoon has been stopped");

                for (var i = 0; i < 100; i++)
                {
                    _crankController.RotateCrank(180);
                    _crankController.RotateCrank(0);
                }
                yield return new WaitForSeconds(1);
                Assert.AreNotEqual(_projectile.transform.position, locationNew, $"Player {player}: Projectile has not been properly wound in");
            
                // Projectile should have returned to harpoon
                location = _projectile.transform.position;
                _harpoonController.ShootProjectile();
                yield return new WaitForSeconds(1);

                Assert.AreNotEqual(_projectile.transform.position, location, $"Player {player}: Second shot wasn't successful");
            }
        }
        
        /**
         * ShootProjectileAndRotateHarpoonTest tests if the harpoon permits rotations after the projectile was shot.
         *
         * @returns successful if alignment after rotation hasn't changed
         */
        [UnityTest]
        public IEnumerator ShootProjectileAndRotateHarpoonTest()
        {
            for (var player = 1; player <= MAX_PLAYERS; player++)
            {
                LoadPlayer(player);
            
                _harpoonController.ShootProjectile();
                var rotationProjectile = _projectile.transform.rotation.eulerAngles.z;
                yield return new WaitForSeconds(1);
                _harpoonController.RotateHarpoon(rotationProjectile+90);
                yield return new WaitForSeconds(1);
                Assert.AreEqual(rotationProjectile, _projectile.transform.rotation.eulerAngles.z, 0.1f, 
                    $"Player {player}: Harpoon rotated after cannon has been shot");
            }
            
        }
    }
}