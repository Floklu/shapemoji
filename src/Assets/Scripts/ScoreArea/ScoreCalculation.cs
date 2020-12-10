using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

namespace ScoreArea
{
    /**
     * used to calculate score after login emoji
     */
    public class ScoreCalculation : MonoBehaviour
    {
        /**
         * Analyze scorable view, create virtual screenshot, read pixels, analyse pixels, read score, trigger ScoreArea.ChangeScore() in a coroutine
         *
         * @param scoreArea ScoreArea to analyze
         * @param renderer Renderer of score area
         * @param cam Main Camera
         */
        public IEnumerator AnalyzeScoreableView(ScoreArea scoreArea, Renderer renderer, Camera cam)
        {
            var result = new AnalyzeScoreAreaResult();
            var bounds = renderer.bounds;
            // get dimensions of Score Area
            var size = cam.WorldToScreenPoint(bounds.max);
            // get position and transform to screen point
            var position = cam.WorldToScreenPoint(bounds.min);
            // create texture to store "screenshot" in
            var img = new Texture2D((int) (size.x - position.x), (int) (size.y - position.y), TextureFormat.RGB24,
                false);
            // create rectengular at score area
            var rect = new Rect((Vector2) position, (Vector2) (size - position));
            // wait for frame ot be rendered
            yield return new WaitForEndOfFrame();
            // create the image
            img.ReadPixels(rect, 0, 0);
            img.Apply();
            //byte[] toPNG = img.EncodeToPNG();
            //System.IO.File.WriteAllBytes("./screenshot.png", toPNG);
            var pixels = img.GetPixels();
            //analyze pixels
            result = AnalyzePixelMap(pixels);
            //calculate score
            var score = CalculateScore(result);
            //TODO push result into ScoreArea
            GetComponent<ScoreArea>().HandleScore(score);
            yield return null;
        }


        /**
         * analyzes pixel map for colors, identifying emojiCovered, emojiUncovered, scoreAreaCovered
         *
         * @param pixels 1D pixel Map to analyze
         *
         * @return results of analysis in AnalyzeScoreAreaResult
         */
        public static AnalyzeScoreAreaResult AnalyzePixelMap(Color[] pixels)
        {
            var result = new AnalyzeScoreAreaResult();
            int emojiCovered = 0;
            int scoreAreaCovered = 0;
            int emojiUncovered = 0;
            foreach (var pixel in pixels)
            {
                if (pixel.r > 0 && pixel.g > 0)
                {
                    scoreAreaCovered++;
                }
                else if (pixel.b > 0 && pixel.g > 0)
                {
                    emojiCovered++;
                }
                else if (pixel.b > 0)
                {
                    emojiUncovered++;
                }
            }

            result.EmojiCovered = emojiCovered;
            result.BackgroundCovered = scoreAreaCovered;
            result.EmojiUncovered = emojiUncovered;
            return result;
        }

        /**
         * calculates score from AnalyzeScoreAreaResult within range -100:100
         * 50% "good coverage": 0 points
         * 100%: 100 points
         *
         * @param analyzed AnalyzeScoreAreaResults to analyze
         */
        public static int CalculateScore(AnalyzeScoreAreaResult analyzed)
        {
            // emoji size must not be zero!
            if (analyzed.EmojiCovered + analyzed.EmojiUncovered < 1) return 0;
            var score = 200 * (analyzed.EmojiCovered - analyzed.BackgroundCovered) /
                (analyzed.EmojiCovered + analyzed.EmojiUncovered) - 100;
            // do not loose more then 100 points per emoji
            if (score < -100) score = -100;
            return score;
        }
    }
}