using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int Balance;
    public int startBalance = 400;
    public int startingLives = 5;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverUi;
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
            buildManager.unselectTurret();
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
    private void endGame()
    {
        gameOver = true;
        gameOverUi.SetActive(true);
    }
    public bool GameOver { get { return gameOver; } }
}
