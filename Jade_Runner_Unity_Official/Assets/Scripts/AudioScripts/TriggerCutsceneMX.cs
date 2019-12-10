using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutsceneMX : MonoBehaviour
{
    private void OnTriggerEnter()
    {
        GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().EndForrestLevel();
    }
}
