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
    private void Start()
    {
        _game = Game.Instance;
        _currentEmojiNumber = -1;
        ChangeEmoji();
    }

    /**
     * Get next Emoji from the Sprite List
     * @return new Emoji-Sprite
     */
    private Sprite GetNewEmoji()
    {
        _currentEmojiNumber++;
        var newEmoji = _game.GetEmoji(_currentEmojiNumber);
        return newEmoji;
    }

    /**
     * Change Emoji-Sprite
     */
    public void ChangeEmoji()
    {
        emoji.GetComponent<SpriteRenderer>().sprite = GetNewEmoji();
    }

    /**
     * ChangeColorOfEmojiSpriteToBlue changes the color of the emoji to blue
     */
    public void ChangeColorOfEmojiSpriteToBlue()
    {
        emoji.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
    }

    /**
     * RemoveColorFromEmoji changes the emoji sprite to its original color
     */
    public void RemoveColorFromEmoji()
    {
        emoji.GetComponent<SpriteRenderer>().color = Color.white;
    }
}