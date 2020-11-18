using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    /**
     * Tests in Play Mode for Rope in User Story Shoot Harpoon
     * @author Andrei Dziubenka
     * @date 18.11.2020
     */
    public class RopeTest
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
         * Tests, if ropes from different projectiles have the same length after flying 1 second
         */
        [UnityTest]
        public IEnumerator RopeTestSameLengthening()
        {
            var projectile1 =
                GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");
            var rope1 = GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonRope");

            var projectile2 =
                GameObject.Find("Team_1/Player_2/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");
            var rope2 = GameObject.Find("Team_1/Player_2/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonRope");

            projectile1.GetComponent<Projectile>().Shoot();
            projectile2.GetComponent<Projectile>().Shoot();
            yield return new WaitForSeconds(1);

            float length1 = rope1.gameObject.GetComponent<SpriteRenderer>().bounds.size.magnitude;
            float length2 = rope2.gameObject.GetComponent<SpriteRenderer>().bounds.size.magnitude;

            Assert.AreEqual(length1, length2, 0.1);
        }
    }
}