using System;
using System.Collections.Generic;
using System.Timers;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

/**
 * Class Game has functionality to quit the game and time display
 */
public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField] private List<Sprite> emojiSprites;

    /**
     * Update is called once per frame.
     * The game is quitting if the escape key is pressed.
     */
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // this quits the game in the unity editor
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            // this quits the game if it's already build and running
            Application.Quit();
        }
    }
   

    // Unity Event function, called when component is enabled
    private void OnEnable()
    {
        Instance = this;
        Random.InitState((int) DateTime.Now.Ticks);
        emojiSprites.Shuffle();
    }

    // Unity Event function, called when component is disabled
    private void OnDisable()
    {
        Instance = null;
    }

    /**
     * Get the n-th emoji from the list of emoji sprites
     * 
     * @param num the position modulo the count of emojis
     * @returns the sprite at the given position
     */
    public Sprite GetEmoji(int num)
    {
        return emojiSprites.Count != 0 ? emojiSprites[num % emojiSprites.Count] : null;
    }

    /**
     * GetSpriteListCount returns the length of the sprite list
     *
     * @return the length of the emoji sprite list
     */
    public int GetSpriteListCount()
    {
        return emojiSprites.Count;
    }

    

}