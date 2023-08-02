using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader fader;
    public Button[] levelButtons;
    // Start is called before the first frame update
    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1); // 1 is a default value
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i +1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void Select(string lvlName)
    {
        AudioManager.Instance.StopMainMenuMusic();

        fader.FadeTo(lvlName);
    }
}
