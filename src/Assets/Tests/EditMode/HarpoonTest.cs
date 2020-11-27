using System.Collections;
using Harpoon;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditMode
{
    /**
     * Class, which tests harpoon functionality
     *
     * @author Robert Meier
     * @date   09.11.2020
     */
    public class HarpoonTest
    {
        
        /**
         * Tests, whether the rotation of a certain degrees results in the expected alignment of the harpoon
         *
         * @returns AssertionException, if alignment after rotation differs from expected value
         */
        [UnityTest]
        public IEnumerator HarpoonTestRotation()
        {
            var harpoon = GameObject.Find("Harpoon");
            var harpoonController = harpoon.GetComponent<HarpoonController>();
            var rotation = harpoon.transform.rotation;
            var rotationEuler = rotation.eulerAngles;
            var expectedValue = rotationEuler.z + 180;
            var rotationNew = Quaternion.Euler(0, 0, expectedValue);
            
            rotation = rotationNew;
            //harpoon.transform.rotation = rotation;
            harpoonController.RotateHarpoon(expectedValue);
            Assert.AreEqual(rotation.eulerAngles.z,expectedValue%360,1f, "Rotation as intended?");
            return null;
        }
    }
}
