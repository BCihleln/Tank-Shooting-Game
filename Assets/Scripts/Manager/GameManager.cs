using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public GameObject Player;
    public GameObject scoreNum, replayButton;
    TextMeshProUGUI scoreText;

    // TODO When Player die, PauseGame game, Show Replay button
    // -> Using delegate or Event?
    // -> Replay Button.SetActive(true)
    // TODO Calculate Score, show on UI (scoreText.text)
    protected override void Awake()
    {
        base.Awake();
        scoreText = scoreNum.GetComponent<TextMeshProUGUI>();

        ContinueGame();
        Cursor.lockState = CursorLockMode.None;

        DontDestroyOnLoad(this);
        // DontDestroyOnLoad(Player);
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    bool isPause = false;
    public void PauseOrContinue()
    {
        if (isPause) ContinueGame();
        else PauseGame();
        isPause = !isPause;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        //TODO Cursor.lockState = CursorLockMode.None;
        //TODO Call PauseGame Menu
    }

    void ContinueGame()
    {
        Time.timeScale = 1;
        //TODO Cursor.lockState = CursorLockMode.Confined;
        //TODO Close PauseGame Menu
    }
}
