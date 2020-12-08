using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

namespace ScoreArea
{
    public class ScoreCalculation : MonoBehaviour
    {
        public IEnumerable<WaitForEndOfFrame> AnalyzeScoreableView(ScoreArea scoreArea)
        {
            var result = new AnalyzeScoreAreaResults();
            var renderer = scoreArea.GetComponent<Renderer>();
            var bounds = renderer.bounds;
            // get dimensions of Score Area
            var size = bounds.size;
            var position = bounds.min;
            // create texture to store "screenshot" in
            var img = new Texture2D((int) size.x, (int) size.y, TextureFormat.RGB24, false);
            // create rectengular at score area position
            var rect = new Rect((Vector2)position, (Vector2)size);
            // wait for frame ot be rendered
            yield return new WaitForEndOfFrame();
            // create the image
            img.ReadPixels(rect, 0, 0);
            img.Apply();
            var pixels = img.GetPixels();


            result = AnalyzePixelMap(pixels);
            //TODO push result into ScoreArea
            //@FLO here I need to call a function inside ScoreArea, something like ReceiveNewResults(result) which will then trigger all your stuff
            yield return null;
        }


        private AnalyzeScoreAreaResults AnalyzePixelMap(Color[] pixels)
        {
            var result = new AnalyzeScoreAreaResults();
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
                else if ( pixel.b > 0)
                {
                    emojiUncovered++;
                }
            }
            result.SetEmojiCovered(emojiCovered);
            result.SetBackgroundCovered(scoreAreaCovered);
            result.SetEmojiUncovered(emojiUncovered);
            return result;
        }
   

        
        
    }
}
