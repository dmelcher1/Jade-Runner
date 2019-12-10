using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutsceneMX : MonoBehaviour
{
    public GameObject endMusicTrigger;

    private void Update()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void OnTriggerEnter()
    {
        GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().EndForrestLevel();
        Debug.Log("music switch to EndForrestLevel");
    }
}
