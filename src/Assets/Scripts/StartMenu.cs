using UnityEditor;
using UnityEngine;

/**
 * StartMenu Class has Methods for each Menu Option
 */
public class StartMenu : MonoBehaviour
{
    /**
     * Run menu option: start game
     */
    public void StartGame()
    {
        GameSceneManager.Instance.LoadPlayingScene();
    }

    /**
     * Exit menu option: exit the application
     * Exit code by Florian Kluth
     */
    public void Exit()
    {
        // this quits the game in the unity editor
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        // this quits the game if it's already build and running
        Application.Quit();
    }
    
}
