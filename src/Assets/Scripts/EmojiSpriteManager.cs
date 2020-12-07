using UnityEngine;

/**
 * This class is used to get new Emoji-Sprites for the Score Area
 */
public class EmojiSpriteManager : MonoBehaviour
{
    [SerializeField] private GameObject emoji;
    private Game _game;
    private int _currentEmojiNumber;
    // Start is called before the first frame update
    void Start()
    {
        _game = Game.Instance;
        _currentEmojiNumber = -1;
    }

    /**
     * Get next one Emoji from the Sprite List
     * @return new Emoji-Sprite
     */
    private Sprite GetNewEmoji()
    {
        _currentEmojiNumber++;
        Sprite newEmoji = _game.GetEmoji(_currentEmojiNumber);
        return newEmoji;
    }

    /**
     * Change Emoji-Sprite
     */
    public void ChangeEmoji()
    {
        emoji.GetComponent<SpriteRenderer>().sprite = GetNewEmoji();
    }
    
    
}
