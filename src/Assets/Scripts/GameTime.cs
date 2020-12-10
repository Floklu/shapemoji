using System;
using System.Collections.Generic;
using System.Timers;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    
    [SerializeField] private int finishTime;
    [SerializeField] private GameObject textTimeCountdown;
    [SerializeField] private GameObject textTimeRemaining1;
    [SerializeField] private GameObject textTimeRemaining2;
    private static Timer timer;

    
    /**
     * game start time in unix format
     */
    private int startTime;
    /**
     * current time in unix format, will be updated by timer
     */
    private int currentTime;

    private Text text;
    private Text text1;
    private Text text2;


    /**
     * Called in the beginning of the game, once
     */
    private void Start()
    {
        text2 = textTimeCountdown.GetComponent<Text>();
        text1 = textTimeRemaining2.GetComponent<Text>();
        text = textTimeRemaining1.GetComponent<Text>();
        textTimeCountdown.GetComponent<Text>().text = "";
        startTime = (int) DateTimeOffset.Now.ToUnixTimeSeconds();
        currentTime = (int) DateTimeOffset.Now.ToUnixTimeSeconds();
        timer = new Timer(1000);
        timer.Elapsed += OnTimerUpdate;
        timer.AutoReset = true;
        timer.Enabled = true;
    }
    
    /**
     * Update is called once per frame.
     * Time display is refreshed.
     * 
     */
    private void Update()
    {
        TimeDisplay();
        CountdownDisplay();
        CheckGameOver();
        
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
    
    /**
     * Update elapsed time
     */
    private void OnTimerUpdate(object source, ElapsedEventArgs e)
    {
        currentTime = (int) DateTimeOffset.Now.ToUnixTimeSeconds();
    }

    /**
     * find out remaining time until game end
     * @return remaining time
     */
    private int GetRemainingTime()
    {
        return finishTime - currentTime + startTime;
    }

    /**
     * remaining time display
     */
    private void TimeDisplay()
    {
        int time = GetRemainingTime();
        int seconds = time % 60;
        int minutes = time / 60;
        text.text = $"{minutes:D2}:{seconds:D2}";
        text1.text = $"{minutes:D2}:{seconds:D2}";
    }
    
    /**
     * Countdown display for remaining 5 seconds
     */
    private void CountdownDisplay()
    {
        if (GetRemainingTime() <= 5)
        {
            text2.text = Convert.ToString(GetRemainingTime());
        }
        
    }

    /**
     * check, if time is over and load end scene
     */
    private void CheckGameOver()
    {
        if (GetRemainingTime() <= 0)
        {
            timer.Enabled = false;
            GameSceneManager.Instance.LoadEndScene();
        }
    }
}
