namespace ScoreArea
{
    /**
     * class storing information about pixel color analysus
     *
     */
    public class AnalyzeScoreAreaResult
    {
        private int _backgroundCovered;
        private int _emojiCovered;
        private int _emojiUncovered;

        public int EmojiCovered
        {
            get => _emojiCovered;
            set => _emojiCovered = value;
        }

        public int EmojiUncovered
        {
            get => _emojiUncovered;
            set => _emojiUncovered = value;
        }

        public int BackgroundCovered
        {
            get => _backgroundCovered;
            set => _backgroundCovered = value;
        }
    }
}