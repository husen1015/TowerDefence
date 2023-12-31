using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string firstLevel = "LevelSelect";

    public SceneFader sceneFader;
    private void Start()
    {
        AudioManager.Instance.PlayMainMenuMusic();
    }

    public void Play()
    {
        sceneFader.FadeTo(firstLevel);
    }
    public void Quit()
    {
        Debug.Log("Exsiting...");
        Application.Quit();
    }
}
