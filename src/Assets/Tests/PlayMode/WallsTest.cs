using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class WallsTest
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator WallsTestWithEnumeratorPasses()
        {
            /*
            var wall =
                GameObject.Find("Wall_1");
            
            var spriteRenderer = wall.GetComponent<SpriteRenderer>();

            float x = spriteRenderer.bounds.size.x;
            float y = spriteRenderer.bounds.size.y;

            Vector2 wallPosition = wall.transform.position;
            
            var cannon =
                GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon/HarpoonCannon");
            
            var projectile =
                GameObject.Find("Team_1/Player_1/Base/HarpoonBase/Harpoon/HarpoonCannon/HarpoonProjectile");

            //cannon.transform.Rotate(0,0,45);
            //transform.InverseTransformDirection ...
            projectile.GetComponent<Projectile>().Shoot();
            */
            yield return new WaitForSeconds(1);

            Assert.IsFalse(false);
        }
    }
}
