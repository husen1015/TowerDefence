using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string firstLevel = "SampleScene";
    public void Play()
    {
        SceneManager.LoadScene(firstLevel);

    }
    public void Quit()
    {
        Debug.Log("Exsiting...");
        Application.Quit();
    }
}
