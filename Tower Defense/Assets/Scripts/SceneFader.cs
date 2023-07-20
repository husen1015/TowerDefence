using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image img;
    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene)); 
    }
    IEnumerator FadeIn()
    {
        float t = 1f;
        while (t > 0f)
        {
            t-= Time.deltaTime;
            img.color = new Color(0f, 0f, 0f, t);
            yield return 0; //wait a frame and continue

        }
    }
    IEnumerator FadeOut(string scene)
    {
        float t = 0f;
        while (t > 1f)
        {
            t -= Time.deltaTime;
            img.color = new Color(0f, 0f, 0f, t);
            yield return 0; //wait a frame and continue

        }
        SceneManager.LoadScene(scene);
    }
}
