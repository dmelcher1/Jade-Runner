using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FootSteps : MonoBehaviour
{

    uint bankID;

    public void LoadBank()
    {
        AkSoundEngine.LoadBank("JadeRunner_Soundbank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
    }
    // Update is called once per frame
    private void Step()
    {
        AkSoundEngine.PostEvent("footStep", gameObject);

    }
    //[SerializeField]
    //private AudioClip[] footStep;

    //private AudioSource footStepSource;

    //private void Awake()
    //{
    //    footStepSource = GetComponent<AudioSource>(); 
    //}

    //private void Step()
    //{
    //    AudioClip footStep = GetRandomClip();
    //    footStepSource.PlayOneShot(footStep);
    //}


    //private AudioClip GetRandomClip()
    //{
    //    return footStep[UnityEngine.Random.Range(0, footStep.Length)];
    //}

}
