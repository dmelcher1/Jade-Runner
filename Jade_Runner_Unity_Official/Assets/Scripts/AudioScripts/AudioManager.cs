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
    }

    public void PlayVillageScene()
    {
        AkSoundEngine.PostEvent("villageStart", gameObject);
    }
}
