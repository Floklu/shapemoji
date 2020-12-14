using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Manages the transistion between scenes
 */
public class GameSceneManager
{
    private static GameSceneManager instance;
    private ScoreArea.ScoreArea scoreArea1;
    private ScoreArea.ScoreArea scoreArea2;
    private string playingScene;

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
        if (instance is null)
            instance = new GameSceneManager();
        return instance;
    }

    /**
     * Loads the Playing Scene and initializes the team scores
     */
    public void LoadPlayingScene2vs2()
    {
        Time.timeScale = 1;
        playingScene = "Scenes/Scene_Playground_2vs2";
        SceneManager.LoadScene("Scenes/Scene_Playground_2vs2");
    }

    /**
     * Changes to the End- and Creditsscene
     */
    public void LoadEndScene()
    {
        scoreArea1 = GameObject.Find("Team_1/ScoreArea").GetComponent<ScoreArea.ScoreArea>();
        scoreArea2 = GameObject.Find("Team_2/ScoreArea").GetComponent<ScoreArea.ScoreArea>();
        ScoreTeam1 = scoreArea1.TeamScore;
        ScoreTeam2 = scoreArea2.TeamScore;
        SceneManager.LoadScene("Scenes/Scene_End");
    }

    /**
     * Loads the 1vs1 Playing Scene
     */
    public void LoadPlayingScene1vs1()
    {
        Time.timeScale = 1;
        playingScene = "Scenes/Scene_Playground_1vs1";
        SceneManager.LoadScene("Scenes/Scene_Playground_1vs1");
    }

    /**
     * Loads the current Scene
     */
    public void LoadCurrentScene()
    {
        Time.timeScale = 1;
        // doesn't work if the 2vs2 or 1vs1 is started on it's own in editor(without start menu)
        SceneManager.LoadScene(playingScene);
    }
}