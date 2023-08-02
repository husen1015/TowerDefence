using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SceneFader sceneFader;
    public static int currLevel = 1;
    public int Balance;
    public int startBalance = 400;
    public int startingLives = 5;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverUi;
    public GameObject lvlWonUI;

    public GameObject pauseUI;
    public static int roundsPlayed;
    private bool gameOver = false;
    int livesLeft;
    public static GameManager Instance { get; private set; }

    BuildManager buildManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("cannot have two instances of singleton class");
            return; 
        }
        Instance = this;
    }
    private void Start()
    {
        AudioManager.Instance.StopMainMenuMusic();
        livesLeft = startingLives;
        buildManager = BuildManager.Instance;
        Balance = startBalance;
        moneyText.text = $"{startBalance.ToString()}$";
        livesText.text = $"Lives Left: {livesLeft}";
        roundsPlayed= 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            endGame();
        }
        if (Input.GetMouseButtonDown(1))
        {
            buildManager.Unselect();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePauseGame();
        }
    }
    public void incrementBalance(int balance)
    {
        Balance += balance;
        moneyText.text = $"{Balance.ToString()}$";
    }
    public void incrementLives(int livesNum)
    {
        livesLeft += livesNum;
        if (!(livesLeft < 0))
        {
            livesText.text = $"Lives Left: {livesLeft}";
        }
        else
        {
            //end game here
            endGame();
        }
    }
    private void togglePauseGame()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        if(pauseUI.activeSelf)
        {
            //pause the game
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void Resume()
    {
        togglePauseGame();
    }
    public void Restart()
    {
        //unpause then restart the scene
        togglePauseGame();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void Menu()
    {
        togglePauseGame();
        sceneFader.FadeTo("MainMenu");

    }
    private void endGame()
    {
        gameOver = true;
        gameOverUi.SetActive(true);
    }
    public void WinLevel()
    {
        //TODO-this may need to change
        gameOver = true;

        currLevel++;
        Debug.Log($"curr lvl: {currLevel} playerprefs: {PlayerPrefs.GetInt("levelReached")}");
        //if reaching this level for the first time update player prefs
        if (PlayerPrefs.GetInt("levelReached") < currLevel)
        {
            PlayerPrefs.SetInt("levelReached", currLevel);
        }
        lvlWonUI.SetActive(true);
    }
    public bool GameOver { get { return gameOver; } }
}
