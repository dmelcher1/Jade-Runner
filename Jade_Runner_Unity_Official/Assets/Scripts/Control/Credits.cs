using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Credits : MonoBehaviour
{
    public Animator animator;
    public Image fader;
    public RawImage playScreen;
    public VideoPlayer creditsSceneVideo;
    public AudioSource creditsSceneAudio;
    public float changeLevelDelay;
    public double vidRunTime;
    public double currentRunTime;
    public double endTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("PlayVideo");
    }

    IEnumerator PlayVideo ()
    {
        
        vidRunTime = creditsSceneVideo.clip.length;
        endTime = vidRunTime - 1;
        creditsSceneVideo.Prepare();
        while (!creditsSceneVideo.isPrepared)
        {
            yield return new WaitForSeconds(1.0f);
            break;
        }
        if (creditsSceneVideo.isPrepared)
        {
            animator.enabled = true;
        }
        playScreen.texture = creditsSceneVideo.texture;
        //animator.SetBool("FadeOut", false);

        creditsSceneVideo.Play();
        GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayEndCredits();
        creditsSceneAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayEndCredits();
        currentRunTime = creditsSceneVideo.time;
        if (currentRunTime >= endTime)
        {
            creditsSceneVideo.Pause();
            animator.SetBool("FadeOut", true);
            changeLevelDelay -= 0.1f;
            if (changeLevelDelay <= 0)
            {
                //SceneManager.LoadScene(1);
                SceneManager.LoadScene(0);
            }
        }
    }
}

