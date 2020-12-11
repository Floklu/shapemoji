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
    
}
