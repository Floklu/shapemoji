using UnityEngine;

/**
 * Class StartButton for the Button Start in the Start Menu
 */
public class StartButton : MonoBehaviour
{
    /**
     * Is triggered when mouse is up
     * Loads playing scene through GameSceneManager
     */
    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        GameSceneManager.Instance.LoadPlayingScene();
    }
}
