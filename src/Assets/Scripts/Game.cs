using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

/**
 * Class Game has functionality to quit the game and time display
 */
public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField] private List<Sprite> emojiSprites;
    [SerializeField] private GameObject pauseMenu;
    
    private bool _isPaused;

    /**
     * run once at start
     * deactive pause menu
     */
    private void Start()
    {
        _isPaused = false;
        pauseMenu.SetActive(false);
    }
    
    /**
     * Update is called once per frame.
     * The game is quitting if the escape key is pressed.
     */
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //toggle paused state
            _isPaused = (_isPaused != true);
            if (_isPaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }



            /*
            // this quits the game in the unity editor
    #if UNITY_EDITOR
            //EditorApplication.isPlaying = false;
    #endif
            // this quits the game if it's already build and running
            //Application.Quit();
            Time.timeScale = 0;
            */
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