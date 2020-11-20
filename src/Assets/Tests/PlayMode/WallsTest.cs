using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    /**
     * Test class for border collisions
     * @author Andrei Dziubenka
     * @date 2020.11.20
     */
    public class WallsTest
    {
        /**
         * Loads game scene
         */
        [OneTimeSetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("Scenes/Scene_Playground_2vs2");
        }

        /**
         * Test border collision near blue base
         */
        [UnityTest]
        public IEnumerator WallsTestBlueBase()
        {
            var harpoon =
                GameObject.Find("Team_2/Player_3/Base/HarpoonBase/Harpoon");
            var projectile =
                GameObject.Find("Team_2/Player_3/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");

            aimAtPoint(harpoon, Screen.width, 0);

            projectile.GetComponent<Projectile>().Shoot();

            yield return new WaitForSeconds(1.5f);

            Assert.Zero(projectile.GetComponent<Rigidbody2D>().velocity.magnitude);
        }

        /**
         * Test border collision near red base
         */
        [UnityTest]
        public IEnumerator WallsTestRedBase()
        {
            var harpoon =
                GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon");
            var projectile =
                GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");

            aimAtPoint(harpoon, 0, 0);

            projectile.GetComponent<Projectile>().Shoot();

            yield return new WaitForSeconds(1.5f);

            Assert.Zero(projectile.GetComponent<Rigidbody2D>().velocity.magnitude);
        }

        /**
         * Test border collision near green base
         */
        [UnityTest]
        public IEnumerator WallsTestGreenBase()
        {
            var harpoon =
                GameObject.Find("Team_1/Player_2/Base/HarpoonBase/Harpoon");
            var projectile =
                GameObject.Find("Team_1/Player_2/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");

            aimAtPoint(harpoon, 0, Screen.height);

            projectile.GetComponent<Projectile>().Shoot();

            yield return new WaitForSeconds(1.5f);

            Assert.Zero(projectile.GetComponent<Rigidbody2D>().velocity.magnitude);
        }

        /**
         * Test border collision near orange base
         */
        [UnityTest]
        public IEnumerator WallsTestOrangeBase()
        {
            var harpoon =
                GameObject.Find("Team_2/Player_4/Base/HarpoonBase/Harpoon");
            var projectile =
                GameObject.Find("Team_2/Player_4/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");

            aimAtPoint(harpoon, Screen.width, Screen.height);

            projectile.GetComponent<Projectile>().Shoot();

            yield return new WaitForSeconds(1.5f);

            Assert.Zero(projectile.GetComponent<Rigidbody2D>().velocity.magnitude);
        }

        /**
         * Rotates Harpoon to aim at point
         * @param harpoon Harpoon Object to aim
         * @param pointX x parameter of point to aim in Screen Coordinates
         * @param pointY y parameter of point to aim in Screen Coordinates
         */
        private void aimAtPoint(GameObject harpoon, int pointX, int pointY)
        {
            Quaternion q = Quaternion.Euler(harpoon.transform.eulerAngles);
            Vector3 harpoonRotation = q * Vector3.up;

            Vector3 screenSize = new Vector3(0, 0, 0);

            if (!(Camera.main is null))
            {
                screenSize = Camera.main.ScreenToWorldPoint(new Vector3(pointX, pointY, 0));
                screenSize.z = 0;
            }
            else
            {
                Assert.IsTrue(false);
            }

            Vector3 harpoonPosition = harpoon.transform.position;
            Vector3 path = screenSize - harpoonPosition;
            float angle = Vector3.Angle(harpoonRotation, path);
            harpoon.transform.Rotate(0, 0, angle);
        }
    }
}