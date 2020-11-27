using System.Collections;
using Harpoon;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditMode
{
    public class CrankRotationTest
    {
        private const int MAX_PLAYER = 4;

        /**
         * checks, if the crank handles high rotation values properly
         *
         * @returns true, if values above 360 and below 0 produce the expected values
         */
        [UnityTest]
        public IEnumerator CrankRotationTestHighValues()
        {
            for (var player = 1; player <= MAX_PLAYER; player++)
            {
                var team = (player + 1) / 2;
                float pullDistance = 0;
                var crank = GameObject.Find($"Teams/Team_{team}/Player_{player}/Base/Wheel").GetComponent<CrankController>();
                var expectedValue = (float)240/360 * crank.rangePerRevolution;
                
                crank.CrankRotationEvent += (sender, value) => pullDistance += value;
                crank.RotateCrank(-3000);
                Assert.AreEqual(expectedValue, pullDistance, 0.1f, $"Player {player}: crank rotation results in wrong value");
            }
            
            yield return null;
        }
    }
}