using UnityEngine;

/**
 * This class is used to get new Emoji-Sprites for the Score Area
 */
public class EmojiSpriteManager : MonoBehaviour
{
    private Game _game;
    private int _currentEmojiNumber;
    // Start is called before the first frame update
    void Start()
    {
        _game = Game.Instance;
        _currentEmojiNumber = -1;
    }

    /**
     * Method to change current Emoji to the next one from the Sprite List
     * @return new Emoji-Sprite
     */
    public Sprite GetNewEmoji()
    {
        _currentEmojiNumber++;
        Sprite newEmoji = _game.GetEmoji(_currentEmojiNumber);
        return newEmoji;
    }
    
    
}
