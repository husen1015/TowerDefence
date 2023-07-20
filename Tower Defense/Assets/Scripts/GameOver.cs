using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;

    public TextMeshProUGUI roundsPlayedText;
    private void OnEnable()
    {
        roundsPlayedText.text = GameManager.roundsPlayed.ToString();
    }
    public void Restart()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        sceneFader.FadeTo("MainMenu");

    }
}
