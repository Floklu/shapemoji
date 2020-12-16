using System;
using System.Collections.Generic;
using Harpoon;
using UnityEngine;
using UnityEngine.UI;

/**
 * Class to handle countdown in the beginning of the game
 */
public class StartCountdownTime : MonoBehaviour
{
    /**
     * Countdown Text, UI on the playing scene
     */
    [SerializeField] private Text textTimeCountdown;

    /**
     * Countdown duration in seconds
     */
    [SerializeField] private int countdownDuration;

    /**
     * Time Remaining Text (first), UI on the playing scene
     */
    [SerializeField] private GameObject textTimeRemaining1;
    private Text  _textTimeRemaining1;

    /**
     * Time Remaining Text (second), UI on the playing scene
     */
    [SerializeField] private GameObject textTimeRemaining2;
    private Text  _textTimeRemaining2;

    /**
     * List of harpoon handlers
     */
    [SerializeField] private List<HarpoonController> harpoonControllers;

    /**
     * Time passed since start of countdown in seconds
     */
    private int _elapsedTime;

    private GameTime _gameTime;

    /**
     * Timestamp, when the countdown started, Unix time in seconds
     */
    private int _startTime;

    // Start is called before the first frame update
    private void Start()
    {
        _textTimeRemaining1 = textTimeRemaining1.GetComponent<Text>();
        _textTimeRemaining2 = textTimeRemaining2.GetComponent<Text>();
        _gameTime = GetComponent<GameTime>();
        _gameTime.SetDuringStartCountdown(true);
        SetShootHandlers(false);
        _elapsedTime = 0;
        _startTime = (int) DateTimeOffset.Now.ToUnixTimeSeconds();
        textTimeRemaining1.SetActive(false);
        textTimeRemaining2.SetActive(false);
        _textTimeRemaining1.text = "";
        _textTimeRemaining2.text = "";
    }

    // Update is called once per frame
    private void Update()
    {
        _elapsedTime = (int) DateTimeOffset.Now.ToUnixTimeSeconds() - _startTime;
        textTimeCountdown.text = (countdownDuration - _elapsedTime).ToString();

        if (_elapsedTime >= countdownDuration)
        {
            textTimeRemaining1.SetActive(true);
            textTimeRemaining2.SetActive(true);
            enabled = false;
            textTimeCountdown.text = "";
            SetShootHandlers(true);
            _gameTime.SetDuringStartCountdown(false);
        }
    }

    /**
     * Enable/Disable Player Harpoons to shoot
     * @param enable true -> enable player harpoons
     */
    private void SetShootHandlers(bool enable)
    {
        foreach (HarpoonController controller in harpoonControllers)
        {
            controller.enabled = enable;
        }
    }
}