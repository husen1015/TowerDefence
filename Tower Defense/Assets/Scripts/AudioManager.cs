using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    FMOD.Studio.EventInstance mainMenuMusic;
    string uiHoverSound = "event:/short_click_celected";
    string uiSelectSound = "event:/ping_tech";


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance != null)
        {
            Debug.LogError("cannot have two instances of singleton class");
            return;
        }
        Instance = this;
        mainMenuMusic = RuntimeManager.CreateInstance("event:/MainMenuMusic");

    }

    public static AudioManager Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        //mainMenuMusic = RuntimeManager.CreateInstance("event:/MainMenuMusic");
        //mainMenuMusic.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.current.transform));
        //RuntimeManager.AttachInstanceToGameObject(mainMenuMusic, Camera.current.transform);
        //mainMenuMusic.start();
    }

    public void PlayMainMenuMusic()
    {
        mainMenuMusic.start();
    }
    public void StopMainMenuMusic()
    {
        mainMenuMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    public void PLayButtonHoverSound()
    {
        RuntimeManager.PlayOneShot(uiHoverSound);
    }
    public void PLayButtonSelectSound()
    {
        RuntimeManager.PlayOneShot(uiSelectSound);
    }
}
