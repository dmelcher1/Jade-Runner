using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MovementAudio : MonoBehaviour
{

    //Plays footstep sfx based on animation
    private void Step()
    {
        AkSoundEngine.PostEvent("footStep", gameObject);

    }
    //Plays Attack sfx based on animation
    private void Attack()
    {
        AkSoundEngine.PostEvent("Attack", gameObject);
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
