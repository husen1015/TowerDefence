using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWon : MonoBehaviour
{
    public SceneFader sceneFader;

    public TextMeshProUGUI roundsPlayedText;
    private void OnEnable()
    {
        //roundsPlayedText.text = GameManager.roundsPlayed.ToString();
        StartCoroutine(AnimateRounds());
    }
    public void ContinueToNextLevel()
    {
        int lvl = GameManager.currLevel;
        string sceneName = lvl < 10 ? $"Lvl0{lvl.ToString()}" : $"Lvl{lvl.ToString()}";
        sceneFader.FadeTo(sceneName);
    }
    public void Menu()
    {
        sceneFader.FadeTo("MainMenu");

    }
    //animate the rounds displayed as if counting from 0
    IEnumerator AnimateRounds()
    {
        roundsPlayedText.text = "0";
        int round = 0;
        yield return new WaitForSeconds(.7f);
        while (round < GameManager.roundsPlayed)
        {
            round++;
            roundsPlayedText.text = round.ToString();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
