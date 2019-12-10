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

    private void OnTriggerEnter()
    {
        GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayEndCredits();
        Debug.Log("music switch to EndForrestLevel");
    }
}
