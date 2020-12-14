using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * Class Game has functionality to quit the game and time display
 */
public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField] private List<Sprite> emojiSprites;
    [SerializeField] private List<GameObject> teams;

    private List<Team> _teams;
    public List<Team> Teams => _teams;

    private void Start()
    {
        _teams = teams.Select(x => x.GetComponent<Team>()).ToList();
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
        GameSceneManager.Instance.LoadCurrentScene();
    }

    /**
     * jump to end screen to show credits
     */
    private void ViewCredits()
    {
        GameSceneManager.Instance.LoadEndScene();
    }
}