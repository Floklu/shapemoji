using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ScoreArea;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditMode
{
    /**
     * reads png as pixel maps and calculates scores. Images are stored in test_images and need to be named according to expected result
     */
    public class AnalyzeAndScorePixelMap
    {
        [UnityTest]
        public IEnumerator AnalyzeAndScorePixelMapWithEnumeratorPasses()
        {
            //open images in dir test_images and read expected results from file names
            var images = new List<Color[]>();
            var expectedScores = new List<int>();

            //read all pngs, convert to color[] and store in images, parse expected results from filename and store in expectedScores
            foreach (var file in Directory.EnumerateFiles("./Assets/Tests/EditMode/test_images", "*.png"))
            {
                var fileInBytes = File.ReadAllBytes(file);
                var texture = new Texture2D(2, 2);
                texture.LoadImage(fileInBytes);
                images.Add(texture.GetPixels());
                //remove string but integer
                var scoreInt = file.Replace("./Assets/Tests/EditMode/test_images/", "");
                expectedScores.Add(Int32.Parse(scoreInt.Replace(".png", "")));
            }

            //check if files are read correctly
            Assert.AreEqual(images.Count, expectedScores.Count, "could not read expected values from file names");
            Assert.AreNotSame(images.Count, 0, "no images found");

            // analyze, calculate score and compare
            for (int i = 0; i < images.Count; i++)
            {
                var analyzedResult = ScoreCalculation.AnalyzePixelMap(images[i]);
                Assert.NotNull(analyzedResult, "ScoreCalculation returned null");
                var score = ScoreCalculation.CalculateScore(analyzedResult);
                Assert.AreEqual(score, expectedScores[i], "score calculation returned unexpected result");
            }

            return null;
        }
    }
}