using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;

/**
 * Class Game has functionality to quit the game and time display
 */
public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField] private List<Sprite> emojiSprites;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject buttonExitGame;
    [SerializeField] private GameObject buttonRestartGame;
    [SerializeField] private GameObject buttonViewCredits;
    
    private bool _isPaused;
    private Button _buttonExitGame;
    private Button _buttonRestartGame;
    private Button _buttonViewCredits;

    /**
     * run once at start
     * deactive pause menu
     */
    private void Start()
    {
        _isPaused = false;
        pauseMenu.SetActive(false);
        _buttonExitGame = buttonExitGame.GetComponent<Button>();
        _buttonExitGame.onClick.AddListener( StopGame) ;
        _buttonRestartGame = buttonRestartGame.GetComponent<Button>();
        _buttonRestartGame.onClick.AddListener(RestartGame);
        _buttonViewCredits = buttonViewCredits.GetComponent<Button>();
        _buttonViewCredits.onClick.AddListener(ViewCredits);
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

    /**
     * stops and exits game
     */
    public void StopGame()
    {
        // this quits the game in the unity editor
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        // this quits the game if it's already build and running
        Application.Quit();
    }

    /**
     * restart the game 
     */
    public void RestartGame()
    {
        GameSceneManager.Instance.LoadPlayingScene();

    }
    
    /**
     * jump to end screen to show credits
     */
    private void ViewCredits()
    {
        GameSceneManager.Instance.LoadEndScene();

    }
}