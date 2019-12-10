using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutsceneMX : MonoBehaviour
{
    public GameObject endMusicTrigger;

    void Update()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnTriggerEnter()
    {
        GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayEndCredits();
        //AkSoundEngine.SetSwitch("Music_Switches", "EndCredits", gameObject);
        Debug.Log("music switch to EndForrestLevel");
    }
}
