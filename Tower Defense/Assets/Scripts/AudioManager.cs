using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    FMOD.Studio.EventInstance mainMenuMusic;
    FMOD.Studio.EventInstance lvlMusic1;

    FMOD.Studio.EventInstance laserSound;

    string uiHoverSound = "event:/short_click_celected";
    string uiSelectSound = "event:/ping_tech";
    string turretPlacementSound = "event:/placement_Sound";
    string laserSoundPath = "event:/laser_sound";
    string levelMusic1 = "event:/Hitman";
    public FMOD.Studio.EventInstance currentAmbience;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance != null)
        {
            return;
        }
        Instance = this;
        mainMenuMusic = RuntimeManager.CreateInstance("event:/MainMenuMusic");
        currentAmbience = mainMenuMusic;
        lvlMusic1 = RuntimeManager.CreateInstance(levelMusic1);
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
        currentAmbience.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        mainMenuMusic.start();
        currentAmbience = mainMenuMusic;
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
    public void PlayTurretPlacementSound()
    {
        Debug.Log("placiong");
        RuntimeManager.PlayOneShot(turretPlacementSound);
    }
    public void PlayShootingSound(string firingSound)
    {
        RuntimeManager.PlayOneShot(firingSound);

    }
    public void PlayLaserShot()
    {
        FMOD.RESULT result = laserSound.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        Debug.Log(result == FMOD.RESULT.OK && state == FMOD.Studio.PLAYBACK_STATE.PLAYING);
        Debug.Log($" result = {result }" );

        if (result == FMOD.RESULT.OK && state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            // If the sound is still playing, do not create a new instance and return early
            return;
        }
        
        laserSound = RuntimeManager.CreateInstance(laserSoundPath);
        laserSound.start();

        
    }
    public void StopLaserSound()
    {
        laserSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    public void PlayLvlMusic()
    {
        currentAmbience.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        lvlMusic1.start();
        currentAmbience = lvlMusic1;
    }
}
