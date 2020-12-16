using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    [SerializeField] private int finishTime;
    [SerializeField] private GameObject textTimeCountdown;
    [SerializeField] private GameObject textTimeRemaining1;
    [SerializeField] private GameObject textTimeRemaining2;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject buttonExitGame;
    [SerializeField] private GameObject buttonRestartGame;
    [SerializeField] private GameObject buttonViewCredits;
    [SerializeField] private GameObject buttonResume;
    [SerializeField] private Button buttonTutorial;
    [SerializeField] private GameObject buttonMainMenu;
    
    
    private Button _buttonExitGame;
    private Button _buttonRestartGame;
    private Button _buttonResume;
    private Button _buttonViewCredits;
    private Button _mainMenuButton;
    private bool _duringStartCountDown;

    private bool _isPaused;
    private bool _menuOpen;
    private Text _timeCountdownText;
    private int _timeLeft;
    private Text _timeRemainingText1;

    private Text _timeRemainingText2;

    private int _timestamp;

    /**
     * Called in the beginning of the game, once
     */
    private void Start()
    {
        // init time 
        _timeCountdownText = textTimeCountdown.GetComponent<Text>();
        _timeRemainingText1 = textTimeRemaining1.GetComponent<Text>();
        _timeRemainingText2 = textTimeRemaining2.GetComponent<Text>();
        _timeCountdownText.text = "";
        _timeLeft = finishTime;
        _timestamp = (int) DateTimeOffset.Now.ToUnixTimeSeconds();

        //set state paused and build pause menu
        _isPaused = true;

        SetMenuState(false);
        _buttonExitGame = buttonExitGame.GetComponent<Button>();
        _buttonExitGame.onClick.AddListener(Game.Instance.StopGame);
        _buttonRestartGame = buttonRestartGame.GetComponent<Button>();
        _buttonRestartGame.onClick.AddListener(Game.Instance.RestartGame);
        _buttonViewCredits = buttonViewCredits.GetComponent<Button>();
        _buttonViewCredits.onClick.AddListener(GameSceneManager.Instance.LoadEndScene);
        _buttonResume = buttonResume.GetComponent<Button>();
        _buttonResume.onClick.AddListener(TogglePauseMenu);
        buttonTutorial.onClick.AddListener(GameSceneManager.Instance.LoadTutorialScene);
        _mainMenuButton = buttonMainMenu.GetComponent<Button>();
        _mainMenuButton.onClick.AddListener(GameSceneManager.Instance.LoadStartMenuScene);
        TimeUpdateEvent();
    }


    /**
     * Update is called once per frame.
     * Time display is refreshed.
     * 
     */
    private void Update()
    {
        if (!_isPaused) TimeUpdateEvent();

        //on esc toggle pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    /**
     * open or close menu
     *
     * @param state to set
     */
    private void SetMenuState(bool state)
    {
        _menuOpen = state;
        pauseMenu.SetActive(state);
    }

    /**
     * handle countdown event
     *
     * @param state state to set
     */
    public void SetDuringStartCountdown(bool state)
    {
        _duringStartCountDown = state;
        _timestamp = (int) DateTimeOffset.Now.ToUnixTimeSeconds();
        _isPaused = state;
        if (_menuOpen && state == false)
        {
            _isPaused = true;
        }
    }

    /**
     * starts or ends pause and shows menu accordingly
     */
    private void TogglePauseMenu()
    {
        //toggle paused state
        if (_duringStartCountDown)
        {
            var state = pauseMenu.activeSelf != true;
            SetMenuState(state);
        }
        else
        {
            _isPaused = (_isPaused != true);
            if (_isPaused)
            {
                Time.timeScale = 0;
                SetMenuState(true);
            }
            else
            {
                Time.timeScale = 1;
                _timestamp = (int) DateTimeOffset.Now.ToUnixTimeSeconds();
                SetMenuState(false);
            }
        }
    }

    /**
     * trigger everything related to time update
     */
    private void TimeUpdateEvent()
    {
        TimeLeftIterator();
        UpdateTimerDisplay();
        // start countdown
        if (_timeLeft <= 5) _timeCountdownText.text = _timeLeft.ToString();
        //end game
        if (_timeLeft <= 0) GameSceneManager.Instance.LoadEndScene();
    }

    /**
     * iterates the time left
     */
    private void TimeLeftIterator()
    {
        var newTime = (int) DateTimeOffset.Now.ToUnixTimeSeconds();
        _timeLeft += _timestamp - newTime;
        _timestamp = newTime;
    }

    /**
     * remaining time display
     */
    private void UpdateTimerDisplay()
    {
        int seconds = _timeLeft % 60;
        int minutes = _timeLeft / 60;
        string toDisplay = $"{minutes:D2}:{seconds:D2}";
        _timeRemainingText2.text = toDisplay;
        _timeRemainingText1.text = toDisplay;
    }

    /**
     * returns time left in s 
     */
    public int GetTimeRemaining()
    {
        return _timeLeft;
    }

    /**
     * SetFinishTime sets the finish time. Used in test
     *
     * @param time
     */
    public void SetTimeLeft(int time)
    {
        _timeLeft = time;
    }
}