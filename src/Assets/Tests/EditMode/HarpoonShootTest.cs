using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Tests.EditMode
{
    public class HarpoonShootTest
    {
        [SetUp]
        public void SetUp()
        {
            EditorSceneManager.OpenScene($"Assets/Scenes/Scene_Playground_2vs2.unity");
        }

        /**
         * Tests, if Harpoon Objects for Team1/Player_1 exist
         */
        [Test]
        public void HarpoonShootTestObjectsExistencePasses()
        {
            var harpoon = GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon");
            var cannon = GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon/HarpoonCannon");
            var projectile =
                GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");
            var rope = GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonRope");

            Assert.NotNull(harpoon);
            Assert.NotNull(cannon);
            Assert.NotNull(projectile);
            Assert.NotNull(rope);
        }
    }
}