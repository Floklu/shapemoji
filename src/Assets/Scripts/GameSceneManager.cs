
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Manages the transistion between scenes
 */
public class GameSceneManager
{
    private static GameSceneManager _instance;

    public static GameSceneManager Instance => GetInstance();
    
    public int ScoreTeam1 { get; set; }
    public int ScoreTeam2 { get; set; }

    /**
     * returns the current Instance of the GameSceneManager
     *
     * @returns GameSceneManager
     */
    private static GameSceneManager GetInstance()
    {
        if (_instance is null)
            _instance = new GameSceneManager();
        return _instance;
    }

    /**
     * Loads the Playing Scene and initializes the team scores
     */
    public void LoadPlayingScene()
    {
        SceneManager.LoadScene("Scenes/Scene_Playground_2vs2");
        ScoreTeam1 = 0;
        ScoreTeam2 = 0;
    }

    /**
     * Changes to the End- and Creditsscene
     */
    public void LoadEndScene()
    {
        //TODO get score from scoreAreas
        var scoreArea1 = GameObject.Find("Team_1/ScoreArea").GetComponent<ScoreArea.ScoreArea>();
        var scoreArea2 = GameObject.Find("Team_2/ScoreArea").GetComponent<ScoreArea.ScoreArea>();
        
        // ReSharper disable once Unity.LoadSceneUnexistingScene 
        SceneManager.LoadScene("Scenes/Scene_End");
    }
}