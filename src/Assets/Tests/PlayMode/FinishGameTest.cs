using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class FinishGameTest
    {
        /**
         * Setup test environment
         */
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("Scenes/Scene_Playground_2vs2");
        }

        /**
         * Test if the game changes scenes after the timer runs out
         */
        [UnityTest]
        public IEnumerator ChangeToFinishSceneTest()
        {
            GameObject.Find("PlayingField").GetComponent<GameTime>().SetFinishTime(2);
            yield return new WaitForSeconds(4);
            Assert.IsTrue(SceneManager.GetActiveScene().name == "Scene_End");
        }
    }
}