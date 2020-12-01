using System.Collections;
using Lean.Touch;
using NUnit.Framework;
using Spawner;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    /**
     * Tests the behaviour of Stones with the LeanTouch Bounded Scaling Enabled 
     */
    public class StoneInteractionTest
    {
     
        /**
         * LoadScene loads the Scene ones for every Test in ProjectileTest.
         */
        [OneTimeSetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("Scenes/Scene_Playground_2vs2");
        }
        
        /**
         * Tests, if the stone scales according to the touch input
         *
         * @returns true if size of stone has been successfully manipulated
         */
        [UnityTest]
        public IEnumerator StoneScalingTest()
        {
            var spawner = GameObject.Find("StoneSpawner").GetComponent<HookableGameObjectFactory>();
            var stone = spawner.CreateStone(0, -800);
            stone.AddComponent<LeanSelectable>();
            stone.GetComponent<LeanSelectable>().DeselectOnUp = true;
            stone.GetComponent<LeanSelectable>().IsolateSelectingFingers = true;
            stone.AddComponent<LeanPinchBoundedScale>();
            stone.AddComponent<LeanTouchEvents>();
            
            
            var camera = Camera.main;
            var stonePosition = stone.transform.position;
            var stoneX = stone.transform.localScale.x;

            var worldToScreenPoint1 = camera.WorldToScreenPoint(stonePosition);
            var worldToScreenPoint2 = camera.WorldToScreenPoint(stonePosition);
            worldToScreenPoint2 += new Vector3(10, 0, 0);
            LeanFinger finger1 = new LeanFinger {ScreenPosition = worldToScreenPoint1, Set = true};
            LeanFinger finger2 = new LeanFinger {ScreenPosition = worldToScreenPoint2, Set = true};

            LeanTouch.SimulateFingerDown(finger1);
            LeanTouch.SimulateFingerDown(finger2);

            yield return new WaitForSeconds(1f);

            var deltaFinger = new Vector2(50, 0);
            finger1.ScreenPosition -= deltaFinger;
            finger2.ScreenPosition += deltaFinger;
            finger1.LastScreenPosition = finger1.ScreenPosition;
            finger2.LastScreenPosition = finger2.ScreenPosition;
            finger2.ScreenPosition += deltaFinger;

            yield return new WaitForSeconds(1f);
            finger1.LastScreenPosition = finger1.ScreenPosition;
            finger2.LastScreenPosition = finger2.ScreenPosition;

            finger1.Set = false;
            finger2.Set = false;
            
            Assert.AreNotEqual(stoneX, stone.transform.localScale.x, "Expected Stone different Stone Size after Finger movement");
        }
    }
}