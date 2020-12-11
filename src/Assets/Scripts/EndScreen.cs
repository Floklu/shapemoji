using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

/**
 * manages ui of end screen
 */
public class EndScreen : MonoBehaviour
{
    [SerializeField] private Text winnerText;
    [SerializeField] private Text scoreTeam1Text;
    [SerializeField] private Text scoreTeam2Text;
    [SerializeField] private Text creditsText;
    [SerializeField] private GameObject buttonEndGame;
    [SerializeField] private GameObject buttonRestartGame;

    private Button _buttonEndGame;
    private Button _buttonRestartGame;
    
    
    /**
     * executed after construction, reads scores, declares winner, updates view texts
     */
    private void Start()
    {
        var gameSceneManager = GameSceneManager.Instance;
        var scoreTeam1 = gameSceneManager.ScoreTeam1;
        var scoreTeam2 = gameSceneManager.ScoreTeam2;

        //decide winner
        string winner;
        if (scoreTeam1 > scoreTeam2)
        {
            winner = "Team 1";
        }
        else if (scoreTeam2 > scoreTeam1)
        {
            winner = "Team 2";
        }
        else
        {
            winner = "None! What happened here?";
        }

        //update textes
        winnerText.text += winner;
        scoreTeam1Text.text += "\n \n" + scoreTeam1.ToString() + "P";
        scoreTeam2Text.text += "\n \n" + scoreTeam2.ToString() + "P";
        creditsText.text += "\n" + ReadTextFile("Assets/Media/Text/credits.txt");
        
        _buttonEndGame = buttonEndGame.GetComponent<Button>();
        _buttonEndGame.onClick.AddListener( () =>Game.Instance.StopGame() ) ;
        _buttonRestartGame = buttonRestartGame.GetComponent<Button>();
        _buttonRestartGame.onClick.AddListener( GameSceneManager.Instance.LoadPlayingScene ) ;
    }


    /**
     * reads a file and returns input as string
     *
     * @param filePath complete or relative path to file
     */
    private static string ReadTextFile(string filePath)
    {
        StreamReader reader = new StreamReader(filePath);
        var fileContent = reader.ReadToEnd();
        reader.Close();
        return fileContent;
    }
}