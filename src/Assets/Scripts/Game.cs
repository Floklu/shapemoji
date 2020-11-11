using UnityEditor;
using UnityEngine;

/**
 * Class Game has functionality to quit the game.
 */
public class Game : MonoBehaviour
{
    /**
     * Update is called once per frame.
     * The game is quitting if the escape key is pressed.
     */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // this quits the game in the unity editor
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #endif 
            // this quits the game if it's already build and running
            Application.Quit();
        }
    }
}