using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Tests.EditMode
{
    /**
     * Tests in Edit Mode for User Story Shoot Harpoon
     * 
     * @author Andrei Dziubenka
     * @date 18.11.2020
     */
    public class HarpoonShootTest
    {
        /**
         * Loads game scene
         */
        [SetUp]
        public void SetUp()
        {
            EditorSceneManager.OpenScene($"Assets/Scenes/Scene_Playground_2vs2.unity");
        }

        /**
         * Tests, if Harpoon Objects exist
         */
        [Test]
        public void HarpoonTestObjectsExistence()
        {
            for (int i = 0; i < 4; i++)
            {
                checkHarpoonExistence(i / 2 + 1, i + 1);
            }
        }

        /**
         * tests, if harpoon for specified team/player exists
         * @param team Team Number
         * @param player Player Number
         */
        private void checkHarpoonExistence(int team, int player)
        {
            Debug.Log("Team_" + team + "/Player_" + player + "/Base/HarpoonBase/Harpoon");
            var harpoon = GameObject.Find("Team_" + team + "/Player_" + player + "/Base/HarpoonBase/Harpoon");
            var cannon =
                GameObject.Find("Team_" + team + "/Player_" + player + "/Base/HarpoonBase/Harpoon/HarpoonCannon");
            var projectile =
                GameObject.Find("Team_" + team + "/Player_" + player +
                                "/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");
            var rope = GameObject.Find("Team_" + team + "/Player_" + player +
                                       "/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonRope");

            Assert.NotNull(harpoon);
            Assert.NotNull(cannon);
            Assert.NotNull(projectile);
            Assert.NotNull(rope);
        }
    }
}