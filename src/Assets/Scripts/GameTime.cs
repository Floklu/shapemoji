using System;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    private static Timer timer;

    [SerializeField] private int finishTime;
    [SerializeField] private GameObject textTimeCountdown;
    [SerializeField] private GameObject textTimeRemaining1;
    [SerializeField] private GameObject textTimeRemaining2;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject buttonExitGame;
    [SerializeField] private GameObject buttonRestartGame;
    [SerializeField] private GameObject buttonViewCredits;
    
    private bool _isPaused;
    private Button _buttonExitGame;
    private Button _buttonRestartGame;
    private Button _buttonViewCredits;

    /**
     * current time in unix format, will be updated by timer
     */
    private int currentTime;


    /**
     * game start time in unix format
     */
    private int startTime;

    private Text timeRemainingText2;
    private Text timeRemainingText1;
    private Text timeCountdownText;


    /**
     * Called in the beginning of the game, once
     */
    private void Start()
    {
        timeCountdownText = textTimeCountdown.GetComponent<Text>();
        timeRemainingText1 = textTimeRemaining1.GetComponent<Text>();
        timeRemainingText2 = textTimeRemaining2.GetComponent<Text>();
        timeCountdownText.text = "";
        startTime = (int) DateTimeOffset.Now.ToUnixTimeSeconds();
        currentTime = (int) DateTimeOffset.Now.ToUnixTimeSeconds();
        
        //set state not paused and build pause menu
        _isPaused = false;
        pauseMenu.SetActive(false);
        _buttonExitGame = buttonExitGame.GetComponent<Button>();
        _buttonExitGame.onClick.AddListener( Game.Instance.StopGame) ;
        _buttonRestartGame = buttonRestartGame.GetComponent<Button>();
        _buttonRestartGame.onClick.AddListener(Game.Instance.RestartGame);
        _buttonViewCredits = buttonViewCredits.GetComponent<Button>();
        _buttonViewCredits.onClick.AddListener(GameSceneManager.Instance.LoadEndScene);
    }

    /**
     * Update is called once per frame.
     * Time display is refreshed.
     * 
     */
    private void Update()
    {
        currentTime = (int) DateTimeOffset.Now.ToUnixTimeSeconds();
        TimeDisplay();
        CountdownDisplay();
        CheckGameOver();
        
        //on esc toggle pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //toggle paused state
            _isPaused = (_isPaused != true);
            if (_isPaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
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
    public int GetRemainingTime()
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
        string toDisplay = $"{minutes:D2}:{seconds:D2}";
        timeRemainingText2.text = toDisplay;
        timeRemainingText1.text = toDisplay;
    }

    /**
     * Countdown display for remaining 5 seconds
     */
    private void CountdownDisplay()
    {
        if (GetRemainingTime() <= 5)
        {
            timeCountdownText.text = Convert.ToString(GetRemainingTime());
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

    /**
     * SetFinishTime sets the finish time. Used in test
     *
     * @param time
     */
    public void SetFinishTime(int time)
    {
        finishTime = time;
    }
}