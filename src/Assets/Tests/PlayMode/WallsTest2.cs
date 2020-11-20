using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class WallsTest2
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
        public IEnumerator WallsTestWithEnumeratorPasses()
        {
            Vector3 screenSize = new Vector3(100,100,0);
            
            if (!(Camera.main is null))
            {
                screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
                screenSize.z = 0;
            }
            else
            {
                Assert.IsTrue(false);
            }


            var harpoon =
                GameObject.Find("Team_2/Player_3/Base/HarpoonBase/Harpoon");
            var projectile =
                GameObject.Find("Team_2/Player_3/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");

            Vector3 harpoonPosition = harpoon.transform.position;

            Vector3 path = screenSize - harpoonPosition;

            float angle = Vector3.Angle(Vector3.up+Vector3.left, path);
            
            harpoon.transform.Rotate(0,0,angle+12.0f);
            
            projectile.GetComponent<Projectile>().Shoot();
            
            
            
            yield return new WaitForSeconds(3);

            Assert.Zero(projectile.GetComponent<Rigidbody2D>().velocity.magnitude);
            
            
            
            
        }
        
    }
}
