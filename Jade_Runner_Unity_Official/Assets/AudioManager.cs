using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    uint bankID;

    //loads the sound bank cuz we gotta
    public void Awake()
    {
        AkSoundEngine.LoadBank("JadeRunner_Soundbank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
    }

    // Start is called before the first frame update
    void Start()
    {
        //posts event to set up music and sfx correctly
        AkSoundEngine.PostEvent("inTheBeginning", gameObject);
        AkSoundEngine.SetSwitch("Music_Switches", "Village_1", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
