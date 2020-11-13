using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Spawner;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AttachObject
    {
        // A Test behaves as an ordinary method
        [SetUp]
        public void SetUp()
        {
            EditorSceneManager.OpenScene($"Assets/Scenes/Scene_Playground_2vs2.unity");
        }

        ///Belongs to 153_Lock_Stone_at_Harpoon
        [UnityTest]
        public IEnumerator AttachObjectWithEnumeratorPasses()
        {
            
            // create projectile
            var projectile = GameObject.Find("HarpoonProjectile")?.GetComponent<Projectile>();;
            Assert.NotNull(projectile);
            
            //create Stone
            var stoneFactory = GameObject.Find("StoneSpawner")?.GetComponent<StoneFactory>();
            Assert.NotNull(stoneFactory);
            
            
            var x = Random.Range(0,100);
            var y = Random.Range(0,100);
            var stone = stoneFactory.CreateStone(x, y);
            
            //attach stone to projectile
            projectile.AttachObject(stone);

            // test projectile for child 
            Assert.AreEqual(projectile.transform.childCount, 1);
            Assert.AreEqual(projectile.transform.GetChild(0), stone.transform);
            // test for parent
            Assert.AreEqual(stone.transform.parent, projectile.transform);
            //TODO
            /*
            // move projectile 
            var projectileRigidBody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidBody.MovePosition(new Vector3(100,0,10));
            var positionStone = projectile.transform.position;
            var positionStoneOld = new Vector3(positionStone.x, positionStone.y, positionStone.z);
            //skip frames
            for (int i = 0; i < 10; i++)
            {
                yield return null;
            }
            // test if stone moved too
            Assert.AreNotEqual(projectile.transform.position, positionStoneOld);
            */

            // unattach object
            projectile.UnattachObject();
            Assert.AreEqual(projectile.transform.childCount, 0);
            Assert.AreEqual(stone.transform.parent, null);
            yield return null; 
        }
    }
}
