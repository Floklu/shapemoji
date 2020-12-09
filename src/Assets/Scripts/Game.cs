﻿using System;
using System.Timers;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/**
 * Class Game has functionality to quit the game and time display
 */
public class Game : MonoBehaviour
{
    /**
     * game start time in unix format
     */
    private int startTime;
    /**
     * current time in unix format, will be updated by timer
     */
    private int currentTime;
    /**
     * game duration
     */
    [SerializeField] private int finishTime;
    [SerializeField] private GameObject textTimeCountdown;
    [SerializeField] private GameObject textTimeRemaining1;
    [SerializeField] private GameObject textTimeRemaining2;
    private static Timer timer;

    /**
     * Called in the beginning of the game, once
     */
    private void Start()
    {
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
     * The game is quitting if the escape key is pressed.
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
        textTimeRemaining1.GetComponent<Text>().text = $"{minutes:D2}:{seconds:D2}";
        textTimeRemaining2.GetComponent<Text>().text = $"{minutes:D2}:{seconds:D2}";
    }
    
    /**
     * Countdown display for remaining 5 seconds
     */
    private void CountdownDisplay()
    {
        if (GetRemainingTime() <= 5)
        {
            textTimeCountdown.GetComponent<Text>().text = Convert.ToString(GetRemainingTime());
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