using UnityEngine;

/**
 * This class is used to get new Emoji-Sprites for the Score Area
 */
public class EmojiSpriteManager : MonoBehaviour
{
    [SerializeField] private GameObject emoji;

    private int _currentEmojiNumber;
    private SpriteRenderer _emojiSprite;
    private Game _game;

    // Start is called before the first frame update
    private void Start()
    {
        _game = Game.Instance;
        _emojiSprite = emoji.GetComponent<SpriteRenderer>();
        ChangeEmojiSprite();
    }

    /**
     * Get next Emoji from the Sprite List
     * @return new Emoji-Sprite
     */
    private Sprite GetNewEmojiSprite()
    {
        var newEmoji = _game.GetEmoji(_currentEmojiNumber);
        _currentEmojiNumber++;
        return newEmoji;
    }

    /**
     * Change Emoji-Sprite
     */
    public void ChangeEmojiSprite()
    {
        _emojiSprite.sprite = GetNewEmojiSprite();
    }

    /**
     * ChangeColorOfEmojiSpriteToBlue changes the color of the emoji to blue
     */
    public void ChangeColorOfEmojiSpriteToBlue()
    {
        _emojiSprite.color = new Color(0, 0, 1, 1);
    }

    /**
     * RemoveColorFromEmoji changes the emoji sprite to its original color
     */
    public void RemoveColorFromEmoji()
    {
        _emojiSprite.color = Color.white;
    }
}