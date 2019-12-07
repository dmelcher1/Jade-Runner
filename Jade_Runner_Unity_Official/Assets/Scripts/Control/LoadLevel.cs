using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadLevel : LevelTracking
{
    public Animator animator;
    public Image fader;
    public RawImage playScreen;
    //private LevelTracking levelTracking;
    public VideoPlayer loadingScreenVideo;
    public VideoPlayer startCutSceneVideo;
    public VideoPlayer endCutSceneVideo;
    public VideoPlayer creditsSceneVideo;
    public AudioSource startCutSceneAudio;
    public AudioSource endCutSceneAudio;
    public AudioSource creditsSceneAudio;
    //private float videoLength;
    private float changeLevelDelay = 3.0f;
    //private float transitionDelay = 3.0f;
    private float endCreditsDelay = 3.0f;
    public bool youWon;
    public bool vidActive;

    // Start is called before the first frame update
    void Start()
    {
        //animator.SetBool("FadeOut", true);
        //levelTracking = GameObject.Find("LevelTracker").GetComponent<LevelTracking>();
        StartCoroutine("PlayVideo");
    }

    IEnumerator PlayVideo()
    {
        if(!levelSelected)
        {
            if (previousScene == 0)
            {
                Debug.Log("We're off!");
                startCutSceneVideo.Prepare();
                while (!startCutSceneVideo.isPrepared)
                {
                    yield return new WaitForSeconds(1.0f);
                    break;
                }
                playScreen.texture = startCutSceneVideo.texture;
                //animator.SetBool("FadeOut", false);
                
                startCutSceneVideo.Play();
                startCutSceneAudio.Play();
               
               
               if (startCutSceneVideo.isPrepared && !vidActive)
               {
               Debug.Log("Nugget");
               animator.SetBool("FadeOut", true);
               changeLevelDelay -= 0.1f;
                   if (changeLevelDelay <= 0)
                   {
                         SceneManager.LoadScene(2);
                   }
               }
                
            }
            if (previousScene == 2)
            {
                loadingScreenVideo.Prepare();
                while (!loadingScreenVideo.isPrepared)
                {
                    yield return new WaitForSeconds(1.0f);
                    break;
                }
                playScreen.texture = loadingScreenVideo.texture;
                //animator.SetBool("FadeOut", false);
                
                    loadingScreenVideo.Play();
                    //Might change loadingScreenVideo.isPlaying to a flat wait time instead depending on how long it is
                    if (loadingScreenVideo.isPrepared && !loadingScreenVideo.isPlaying)
                    {
                        animator.SetBool("FadeOut", true);
                        changeLevelDelay -= 0.1f;
                        if (changeLevelDelay <= 0)
                        {
                            SceneManager.LoadScene(3);
                        }
                    }
                
               
            }
            if (previousScene == 3)
            {
                endCutSceneVideo.Prepare();
                while (!endCutSceneVideo.isPrepared)
                {
                    yield return new WaitForSeconds(1.0f);
                    break;
                }
                playScreen.texture = endCutSceneVideo.texture;
                //animator.SetBool("FadeOut", false);
               
                    loadingScreenVideo.Play();
                    //Might change loadingScreenVideo.isPlaying to a flat wait time instead depending on how long it is
                    if (endCutSceneVideo.isPrepared && !loadingScreenVideo.isPlaying)
                    {
                        animator.SetBool("FadeOut", true);
                        endCreditsDelay -= 0.1f;
                        if (endCreditsDelay <= 0)
                        {
                            youWon = true;
                        }
                    }
                
            }
            if (youWon)
            {
                creditsSceneVideo.Prepare();
                while (!creditsSceneVideo.isPrepared)
                {
                    yield return new WaitForSeconds(1.0f);
                    break;
                }
                playScreen.texture = creditsSceneVideo.texture;
                //animator.SetBool("FadeOut", false);
                
                    creditsSceneVideo.Play();
                    creditsSceneAudio.Play();
                    if (creditsSceneVideo.isPrepared && !creditsSceneVideo.isPlaying)
                    {
                        animator.SetBool("FadeOut", true);
                        changeLevelDelay -= 0.1f;
                        if (changeLevelDelay <= 0)
                        {
                            SceneManager.LoadScene(0);
                        }
                    }
                
            }
        }
        else if(levelSelected)
        {
            loadingScreenVideo.Prepare();
            while (!loadingScreenVideo.isPrepared)
            {
                yield return new WaitForSeconds(1.0f);
                break;
            }
            playScreen.texture = loadingScreenVideo.texture;
            //animator.SetBool("FadeOut", false);
           
                loadingScreenVideo.Play();
                //Might change loadingScreenVideo.isPlaying to a flat wait time instead depending on how long it is
                if (loadingScreenVideo.isPrepared && !loadingScreenVideo.isPlaying)
                {
                    animator.SetBool("FadeOut", true);
                    changeLevelDelay -= 0.1f;
                    if (changeLevelDelay <= 0)
                    {
                        if (chosenScene == 2)
                        {
                            SceneManager.LoadScene(2);
                        }
                        else if (chosenScene == 3)
                        {
                            SceneManager.LoadScene(3);
                        }
                    }
                }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(startCutSceneVideo.isPlaying)
        {
            vidActive = true;
        }
        else if(!startCutSceneVideo.isPlaying)
        {
            vidActive = false;
        }
    }
}
