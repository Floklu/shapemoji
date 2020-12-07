using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace ScoreArea
{
    public class ScoreCalculation : MonoBehaviour
    {
        public AnalyzeScoreAreaResults AnalyzeScoreableView(ScoreArea scoreArea)
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
            // create the image
            img.ReadPixels(rect, 0, 0);
            img.Apply();
            var pixels = img.GetPixels();
            
            return result;
        }
   

        
        
    }
}
