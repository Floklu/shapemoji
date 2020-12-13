using UnityEngine;

/**
 * StartMenu Class has Methods for each Menu Option
 */
public class StartMenu : MonoBehaviour
{
    /**
     * Run menu option: start game
     */
    public void StartGame2vs2()
    {
        GameSceneManager.Instance.LoadPlayingScene2vs2();
    }

    /**
     * Starts the 1vs1 game
     */
    public void StartGame1vs1()
    {
        GameSceneManager.Instance.LoadPlayingScene1vs1();
    }

    /**
     * Exit menu option: exit the application
     */
    public void Exit()
    {
        Game.Instance.StopGame();
    }
}