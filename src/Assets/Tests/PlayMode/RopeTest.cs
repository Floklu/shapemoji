using System.Collections;
using Harpoon;
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
            yield return new WaitForSeconds(10);
            var rope1 = GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonRope");
            var rope2 = GameObject.Find("Team_1/Player_2/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonRope");

            var harpoon1 = GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon");
            var harpoon2 = GameObject.Find("Team_1/Player_2/Base/HarpoonBase/Harpoon");
            
            harpoon1.GetComponent<HarpoonController>().ShootProjectile();
            harpoon2.GetComponent<HarpoonController>().ShootProjectile();
            yield return new WaitForSeconds(0.1f);

            float length1 = rope1.gameObject.GetComponent<SpriteRenderer>().bounds.size.magnitude;
            float length2 = rope2.gameObject.GetComponent<SpriteRenderer>().bounds.size.magnitude;

            Assert.AreEqual(length1, length2, 0.1);
        }
    }
}