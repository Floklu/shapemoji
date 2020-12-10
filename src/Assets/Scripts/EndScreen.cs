using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

/**
 * manages ui of end screen
 */
public class EndScreen : MonoBehaviour
{
    [SerializeField] private Text winnerText;
    [SerializeField] private Text scoreTeam1Text;
    [SerializeField] private Text scoreTeam2Text;
    [SerializeField] private Text creditsText;

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