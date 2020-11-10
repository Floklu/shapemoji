using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HarpoonTest
    {
        
        /**
         * Tests, whether the rotation of a certain degrees results in the expected alignment of the harpoon
         */
        [UnityTest]
        public IEnumerator HarpoonTestRotation()
        {
            var harpoon = GameObject.Find("Harpoon");
            var rotation = harpoon.transform.rotation;
            var rotationEuler = rotation.eulerAngles;
            var expectedValue = rotationEuler.z + 180;
            var rotationNew = Quaternion.Euler(0, 0, expectedValue);
            
            rotation = rotationNew;
            harpoon.transform.rotation = rotation;
            Assert.AreEqual(rotation.eulerAngles.z,expectedValue%360,1f);
            yield return null;
        }
    }
}
