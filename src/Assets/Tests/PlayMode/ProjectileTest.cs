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
            var projectile = GameObject.Find("HarpoonProjectile");
            var location = projectile.transform.position;
            var harpoon = GameObject.Find("Harpoon");
            harpoon.GetComponent<HarpoonController>().Shoot();

            yield return new WaitForSeconds(1);

            Assert.AreNotEqual(projectile.transform.position, location);
        }

        /**
         * Tests, if the projectile will be wound in after interaction with the crank
         *
         * @returns true, if position after crank interaction changed after 1 second
         */
        [UnityTest]
        public IEnumerator TestWindInAfterProjectile()
        {
            var projectile = GameObject.Find("HarpoonProjectile");
            var location = projectile.transform.position;
            var harpoon = GameObject.Find("Harpoon").GetComponent<HarpoonController>();
            var wheel = GameObject.Find("Wheel").GetComponent<CrankController>();
            harpoon.Shoot();

            yield return new WaitForSeconds(1);

            harpoon.StopCannon();
            var locationNew = projectile.transform.position;
            Assert.AreNotEqual(projectile.transform.position, location, "Projectile has not been moved since 1 second");
            yield return new WaitForSeconds(1);
            Assert.AreNotEqual(projectile.transform.position, location,
                "Projectile continues after harpoon has been stopped"); 

            wheel.RotateCrank(180);

            yield return new WaitForSeconds(1);

            Assert.AreNotEqual(projectile.transform.position, locationNew, "Projectile has not been properly wound in");

        }

        /**
         * Tests if the harpoon is properly wound in and ready for the next shot
         *
         * @returns true, if harpoon can shoot a second time
         */
        [UnityTest]
        public IEnumerator TestShootAfterWindIn()
        {
            var projectile = GameObject.Find("HarpoonProjectile");
            var location = projectile.transform.position;
            var harpoon = GameObject.Find("Harpoon").GetComponent<HarpoonController>();
            var wheel = GameObject.Find("Wheel").GetComponent<CrankController>();
            harpoon.Shoot();

            yield return new WaitForSeconds(1);

            harpoon.StopCannon();
            var locationNew = projectile.transform.position;
            Assert.AreNotEqual(projectile.transform.position, location, "Projectile has not been moved since 1 second");
            yield return new WaitForSeconds(1);
            Assert.AreNotEqual(projectile.transform.position, location,
                "Projectile continues after harpoon has been stopped");

            for (var i = 0; i < 100; i++)
            {
                wheel.RotateCrank(180);
                wheel.RotateCrank(0);
            }
            yield return new WaitForSeconds(5);
            Assert.AreNotEqual(projectile.transform.position, locationNew, "Projectile has not been properly wound in");
            
            // Projectile should have returned to harpoon
            location = projectile.transform.position;
            harpoon.Shoot();
            yield return new WaitForSeconds(1);

            Assert.AreNotEqual(projectile.transform.position, location, "Second shot wasn't successful");
        }
    }
}