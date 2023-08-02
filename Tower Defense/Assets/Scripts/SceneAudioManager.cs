using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudioManager : MonoBehaviour
{
    
    AudioManager audioManager;
    public static SceneAudioManager Instance { get; private set; }
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
        audioManager = AudioManager.Instance;
    }
    public void PLayButtonHoverSound()
    {
        audioManager.PLayButtonHoverSound();
    }
    public void PLayButtonSelectSound()
    {
        audioManager.PLayButtonSelectSound();
    }
}
