using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

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
            projectile.GetComponent<Projectile>().Shoot();

            yield return new WaitForSeconds(1);

            Assert.AreNotEqual(projectile.transform.position, location);
        }
    }
}