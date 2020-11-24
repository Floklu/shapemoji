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
        /*
         * _centerX is x coordinate of inventory center
         * _centerY is y coordinate of inventory center
         */
        private float _centerX, _centerY;
        /*
         * _harpoon, _projectile, _inventory are GameObjects selected by class method LoadPlayer
         */
        private GameObject _harpoon, _projectile, _inventory;

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
         */
        [UnityTest]
        public IEnumerator CornerTest()
        {
            for (int i = 0; i < 4; i++)
            {
                LoadPlayer(i + 1);
                yield return AimAtPoint(_centerX, _centerY);
            }
        }

        /**
         * Test border collision at corner (left side)
         */
        [UnityTest]
        public IEnumerator CornerLeftTest()
        {
            LoadPlayer(3);
            yield return AimAtPoint(_centerX - 1, _centerY);
            LoadPlayer(1);
            yield return AimAtPoint(_centerX, _centerY + 1);
            LoadPlayer(2);
            yield return AimAtPoint(_centerX + 1, _centerY);
            LoadPlayer(4);
            yield return AimAtPoint(_centerX, _centerY - 1);
        }

        /**
         * Test border collision at corner (right side)
         */
        [UnityTest]
        public IEnumerator CornerRightTest()
        {
            LoadPlayer(3);
            yield return AimAtPoint(_centerX, _centerY + 1);
            LoadPlayer(1);
            yield return AimAtPoint(_centerX + 1, _centerY);
            LoadPlayer(2);
            yield return AimAtPoint(_centerX, _centerY - 1);
            LoadPlayer(4);
            yield return AimAtPoint(_centerX - 1, _centerY);
        }


        /**
         * Rotates Harpoon to aim at point
         * @param harpoon Harpoon Object to aim
         * @param pointX x parameter of point to aim in Screen Coordinates
         * @param pointY y parameter of point to aim in Screen Coordinates
         */
        private IEnumerator AimAtPoint(float pointX, float pointY)
        {
            Quaternion q = Quaternion.Euler(_harpoon.transform.eulerAngles);
            Vector3 harpoonRotation = q * Vector3.up;
            Vector3 pointWorldCoord = new Vector3(pointX, pointY, 0);

            Vector3 harpoonPosition = _harpoon.transform.position;
            Vector3 path = pointWorldCoord - harpoonPosition;
            float angle = Vector3.SignedAngle(harpoonRotation, path, Vector3.forward);
            _harpoon.transform.Rotate(0, 0, angle);
            _harpoon.GetComponent<HarpoonController>().Shoot();
            yield return new WaitForSeconds(1.5f);
            Assert.Zero(_projectile.GetComponent<Rigidbody2D>().velocity.magnitude);
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
            _inventory =
                GameObject.Find("Team_" + team + "/Player_" + player +
                                "/Base/Inventory");

            var inventoryPosition = _inventory.transform.position;

            _centerX = inventoryPosition.x;
            _centerY = inventoryPosition.y;
        }
    }
}