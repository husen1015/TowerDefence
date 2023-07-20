using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string firstLevel = "LevelSelect";
    public SceneFader sceneFader;
    public void Play()
    {
        //SceneManager.LoadScene(firstLevel);
        sceneFader.FadeTo(firstLevel);

    }
    public void Quit()
    {
        Debug.Log("Exsiting...");
        Application.Quit();
    }
}
