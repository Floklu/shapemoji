
namespace ScoreArea
{
    public class AnalyzeScoreAreaResults
    {
        private int _emojiCovered;
        private int _emojiUncovered;
        private int _backgroundCovered;

        public void SetEmojiCovered(int i)
        {
            _emojiCovered = i;
        }

        public void SetEmojiUncovered(int i)
        {
            _emojiUncovered = i;
        }

        public void SetBackgroundCovered(int i)
        {
            _backgroundCovered = i;
        }

        public int GetEmojiCovered()
        {
            return _emojiCovered;
        }

        public int GetEmojiUncovered()
        {
            return _emojiUncovered;
        }

        public int GetBackgroundCovered()
        {
            return _backgroundCovered;
        }
        
    }
}
