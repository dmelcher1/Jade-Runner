using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutsceneMX : MonoBehaviour
{
    public GameObject endMusicTrigger;

    private void Awake()
    {
        if (endMusicTrigger == null)
        {
            DontDestroyOnLoad(endMusicTrigger);
        }
        else if (endMusicTrigger == this)
        {
            Destroy(endMusicTrigger);
        }
    }

    private void OnTriggerEnter()
    {
        GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().EndForrestLevel();
        Debug.Log("music switch to EndForrestLevel");
    }
}
