using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    uint bankID;
    public static AudioManager audioManager;


    //loads the sound bank cuz we gotta
    public void Awake()
    {
        if (audioManager == null)
        {
            DontDestroyOnLoad(gameObject);
            audioManager = this;
        }
        else if (audioManager != this)
        {
            Destroy(gameObject);
        }
        AkSoundEngine.LoadBank("JadeRunner_Soundbank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
    }

    // Start is called before the first frame update
    void Start()
    {
        //posts event to set up music and sfx correctly
        AkSoundEngine.PostEvent("inTheBeginning", gameObject);

        //Make sure to comment out BEFORE YOU LEAVE LEVEL.
        //Its for testing purposes only
        AkSoundEngine.SetSwitch("Music_Switches", "Forrest_1", gameObject);
    }

    public void PlayVillageScene()
    {
        AkSoundEngine.PostEvent("startToVillage", gameObject);
    }

    public void PlayVillageImmediately()
    {
        AkSoundEngine.SetSwitch("Music_Switches", "VillageImmediate", gameObject);
    }

    public void BackToMenu()
    {
        AkSoundEngine.SetSwitch("Music_Switches", "Menu", gameObject);
    }

    public  void EndForrestLevel()
    {
        AkSoundEngine.SetSwitch("Music_Switches", "ForrestToCutScene", gameObject);
    }

    public void PlayEndCredits()
    {
        AkSoundEngine.SetSwitch("Music_Switches", "EndCredits", gameObject);
    }

    public void PlayForrest()
    {
        AkSoundEngine.SetSwitch("Music_Switches", "Forrest_1", gameObject);
    }
}
