using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PLayButtonHoverSound()
    {
        AudioManager.Instance.PLayButtonHoverSound();
    }
    public void PLayButtonSelectSound()
    {
        AudioManager.Instance.PLayButtonSelectSound();

    }
}
