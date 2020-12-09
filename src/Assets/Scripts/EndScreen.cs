using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/**
 * manages ui of end screen
 */
public class EndScreen : MonoBehaviour
{
    [SerializeField] private Text winnerText;
    [SerializeField] private Text scoreTeam1Text;
    [SerializeField] private Text scoreTeam2Text;
    [SerializeField] private Text creditsText;


    void Start()
    {
        var gameSceneManager = GameSceneManager.Instance;

        //TODO remove when triggered in game
        gameSceneManager.ScoreTeam1 = 100;
        gameSceneManager.ScoreTeam2 = 102;
        
        var scoreTeam1 = gameSceneManager.ScoreTeam1;
        var scoreTeam2 = gameSceneManager.ScoreTeam2;
        
        //decide winner
        String winner;
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
        creditsText.text += "\n" + readTextFile("Assets/Media/Text/credits.txt");

    }

    /**
     * reads a file and returns input as string
     *
     * @param filePath complete or relative path to file
     */
    private static String readTextFile(string filePath)
    {
        StreamReader reader = new StreamReader(filePath);
        var fileContent = reader.ReadToEnd();
        reader.Close();
        return fileContent;
    }
}
