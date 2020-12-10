
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Manages the transistion between scenes
 */
public class GameSceneManager
{
    private static GameSceneManager _instance;
    private ScoreArea.ScoreArea scoreArea2;
    private ScoreArea.ScoreArea scoreArea1;

    public static GameSceneManager Instance => GetInstance();
    
    public int ScoreTeam1 { get; set; }
    public int ScoreTeam2 { get; set; }

    private void Start()
    {
        scoreArea1 = GameObject.Find("Team_1/ScoreArea").GetComponent<ScoreArea.ScoreArea>();
        scoreArea2 = GameObject.Find("Team_2/ScoreArea").GetComponent<ScoreArea.ScoreArea>();
    }

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
        ScoreTeam1 = scoreArea1.TeamScore;
        ScoreTeam2 = scoreArea2.TeamScore;
    }

    /**
     * Changes to the End- and Creditsscene
     */
    public void LoadEndScene()
    {
        //TODO get score from scoreAreas
        // ReSharper disable once Unity.LoadSceneUnexistingScene 
        SceneManager.LoadScene("Scenes/Scene_End");
    }
}