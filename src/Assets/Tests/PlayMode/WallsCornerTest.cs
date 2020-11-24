using System.Collections;
using Harpoon;
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
    public class WallsCornerTest
    {
        private GameObject _harpoon,_projectile;

        /**
         * Setup test environment
         */
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("Scenes/Scene_Playground_2vs2");
        }

        /**
         * Test border collision at corner (both sides)
         * Projectile is a Polygon Collider, therefore a precise aim is not possible
         */
        [UnityTest]
        public IEnumerator CornerTest()
        {
            LoadPlayer(3);
            yield return AimAtPoint(Screen.width, 1);
            LoadPlayer(1);
            yield return AimAtPoint(0, 0);
            LoadPlayer(2);
            yield return AimAtPoint(0, Screen.height);
            LoadPlayer(4);
            yield return AimAtPoint(Screen.width, Screen.height);

        }
        
        /**
         * Test border collision at corner (left side)
         * Projectile is a Polygon Collider, therefore a precise aim is not possible
         */
        [UnityTest]
        public IEnumerator CornerLeftTest()
        {
            LoadPlayer(3);
            yield return AimAtPoint(Screen.width, 0);
            LoadPlayer(1);
            yield return AimAtPoint(0, 5);
            LoadPlayer(2);
            yield return AimAtPoint(2, Screen.height);
            LoadPlayer(4);
            yield return AimAtPoint(Screen.width, Screen.height-6);

        }
        
        /**
         * Test border collision at corner (right side)
         * Projectile is a Polygon Collider, therefore a precise aim is not possible
         */
        [UnityTest]
        public IEnumerator CornerRightTest()
        {
            LoadPlayer(3);
            yield return AimAtPoint(Screen.width, 6);
            LoadPlayer(1);
            yield return AimAtPoint(1, 0);
            LoadPlayer(2);
            yield return AimAtPoint(0, Screen.height-5);
            LoadPlayer(4);
            yield return AimAtPoint(Screen.width-3, Screen.height);

        }

        /**
         * Rotates Harpoon to aim at point
         * @param harpoon Harpoon Object to aim
         * @param pointX x parameter of point to aim in Screen Coordinates
         * @param pointY y parameter of point to aim in Screen Coordinates
         */
        private IEnumerator AimAtPoint(int pointX, int pointY)
        {
            Quaternion q = Quaternion.Euler(_harpoon.transform.eulerAngles);
            Vector3 harpoonRotation = q * Vector3.up;
            Vector3 pointWorldCoord = new Vector3(0, 0, 0);

            if (!(Camera.main is null))
            {
                pointWorldCoord = Camera.main.ScreenToWorldPoint(new Vector3(pointX, pointY, 0));
                pointWorldCoord.z = 0;
            }
            else
            {
                Assert.IsTrue(false);
            }

            Vector3 harpoonPosition = _harpoon.transform.position;
            Vector3 path = pointWorldCoord - harpoonPosition;
            float angle = Vector3.SignedAngle(harpoonRotation, path,Vector3.forward);
            _harpoon.transform.Rotate(0, 0, angle);
            _harpoon.GetComponent<HarpoonController>().Shoot();

            yield return new WaitForSeconds(5.0f);
            
            Assert.Zero(_projectile.GetComponent<Rigidbody2D>().velocity.magnitude);
            var distanceFromTarget = (_projectile.transform.position + path.normalized * _projectile.GetComponent<SpriteRenderer>().bounds.size.magnitude / 2.0f -
                                      pointWorldCoord);
            Assert.AreEqual(0.0f,distanceFromTarget.magnitude,85);
            
            //Assert.AreEqual(1,1);

        }

        /**
         * Load Harpoon and Projectile of a player
         * @param player player number
         */
        private void LoadPlayer(int player)
        {
            var team = (player + 1) / 2;
            _harpoon = GameObject.Find("Team_" + team + "/Player_" + player + "/Base/HarpoonBase/Harpoon");
            _projectile =
                GameObject.Find("Team_" + team + "/Player_" + player +
                                "/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");

        }
    }
}