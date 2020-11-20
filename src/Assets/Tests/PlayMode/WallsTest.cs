using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
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

        
        [UnityTest]
        public IEnumerator WallsTestBlueBase()
        {
            var harpoon =
                GameObject.Find("Team_2/Player_3/Base/HarpoonBase/Harpoon");
            var projectile =
                GameObject.Find("Team_2/Player_3/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");

            aimAtWall(harpoon, projectile, Screen.width, 0,Vector3.up+Vector3.left);

            projectile.GetComponent<Projectile>().Shoot();

            yield return new WaitForSeconds(1.5f);

            Assert.Zero(projectile.GetComponent<Rigidbody2D>().velocity.magnitude);
        }
        
        
        [UnityTest]
        public IEnumerator WallsTestRedBase()
        {
            var harpoon =
                GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon");
            var projectile =
                GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");

            aimAtWall(harpoon, projectile, 0, 0,Vector3.up+Vector3.right);

            projectile.GetComponent<Projectile>().Shoot();

            yield return new WaitForSeconds(1.5f);

            Assert.Zero(projectile.GetComponent<Rigidbody2D>().velocity.magnitude);
        }
        
        [UnityTest]
        public IEnumerator WallsTestGreenBase()
        {
            var harpoon =
                GameObject.Find("Team_1/Player_2/Base/HarpoonBase/Harpoon");
            var projectile =
                GameObject.Find("Team_1/Player_2/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");

            aimAtWall(harpoon, projectile, 0, Screen.height,Vector3.down+Vector3.right);

            projectile.GetComponent<Projectile>().Shoot();

            yield return new WaitForSeconds(1.5f);

            Assert.Zero(projectile.GetComponent<Rigidbody2D>().velocity.magnitude);
        }
        
        [UnityTest]
        public IEnumerator WallsTestOrangeBase()
        {
            var harpoon =
                GameObject.Find("Team_2/Player_4/Base/HarpoonBase/Harpoon");
            var projectile =
                GameObject.Find("Team_2/Player_4/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");

            aimAtWall(harpoon, projectile, Screen.width, Screen.height,Vector3.down+Vector3.left);

            projectile.GetComponent<Projectile>().Shoot();

            yield return new WaitForSeconds(1.5f);

            Assert.Zero(projectile.GetComponent<Rigidbody2D>().velocity.magnitude);
        }

        private void aimAtWall(GameObject harpoon, GameObject projectile, int pointX, int pointY,Vector3 harpoonRotation)
        {
            Vector3 screenSize = new Vector3(100, 100, 0);

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