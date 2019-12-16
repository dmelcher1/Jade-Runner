using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public Animator animator;
    public Image fader;
    public RawImage playScreen;
    //private LevelTracking levelTracking;
    public VideoPlayer loadingScreenVideo;
    public VideoPlayer startCutSceneVideo;
    public VideoPlayer endCutSceneVideo;
    //public VideoPlayer creditsSceneVideo;
    public AudioSource startCutSceneAudio;
    public AudioSource endCutSceneAudio;
    //public AudioSource creditsSceneAudio;
    //private float videoLength;
    public float changeLevelDelay;
    //private float transitionDelay = 3.0f;
    private float endCreditsDelay = 3.0f;
    public bool youWon;
    public double vidRunTime;
    public double currentRunTime;
    public double endTime;
    public bool vidReady;
    public bool levelSelected;
    public int previousScene;
    public int chosenScene;
    public LevelTracking levelTracking;
    public bool startReady;
    public bool testingMode;
    public bool startScenePlay;
    public bool endScenePlay;
    public bool creditScenePlay;
    public bool villageToBamboo;
    public bool loadToVillage;
    public bool loadToBamboo;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        levelTracking = GameObject.Find("LevelTracker").GetComponent<LevelTracking>();
        loadingScreenVideo.time = 0;
        startCutSceneVideo.time = 0;
        endCutSceneVideo.time = 0;
        
        startReady = false;
        //StartCoroutine("PlayVideo");
    }

    IEnumerator PlayVideo()
    {
        //yield return new WaitForSeconds(2.0f);

        if (!levelSelected)
        {
            //yield return new WaitForSeconds(1.0f);
            if (previousScene == 0 || startScenePlay)
            {
                vidRunTime = startCutSceneVideo.clip.length;
                endTime = vidRunTime - 1;
                Debug.Log("We're off!");
                startCutSceneVideo.Prepare();
                while (!startCutSceneVideo.isPrepared)
                {
                    yield return new WaitForSeconds(1.0f);
                    break;
                }
                if (startCutSceneVideo.isPrepared)
                {
                    animator.enabled = true;
                }
                playScreen.texture = startCutSceneVideo.texture;
                //animator.SetBool("FadeOut", false);

                startCutSceneVideo.Play();
                vidReady = true;
                startCutSceneAudio.Play();
            }
            if (previousScene == 2 || villageToBamboo)
            {
                vidRunTime = loadingScreenVideo.clip.length;
                endTime = vidRunTime - 1;
                loadingScreenVideo.Prepare();
                while (!loadingScreenVideo.isPrepared)
                {
                    yield return new WaitForSeconds(1.0f);
                    break;
                }
                if (loadingScreenVideo.isPrepared)
                {
                    animator.enabled = true;
                }
                playScreen.texture = loadingScreenVideo.texture;
                //animator.SetBool("FadeOut", false);
                loadingScreenVideo.Play();
                vidReady = true;
                GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayForrest();
                GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().StopMenuAmb();
            }
            if (previousScene == 3 || endScenePlay)
            {

                vidRunTime = endCutSceneVideo.clip.length;
                endTime = vidRunTime - 1;
                endCutSceneVideo.Prepare();
                while (!endCutSceneVideo.isPrepared)
                {
                    yield return new WaitForSeconds(1.0f);
                    break;
                }
                if (endCutSceneVideo.isPrepared)
                {
                    animator.enabled = true;
                }
                playScreen.texture = endCutSceneVideo.texture;
                //animator.SetBool("FadeOut", false);

                endCutSceneVideo.Play();
                //GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayEndCredits();
                endCutSceneAudio.Play();
                vidReady = true;
                //GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayEndCredits();
                //AkSoundEngine.SetSwitch("Music_Switches", "EndCredits", gameObject);
                //if (youWon)
                //{

                //    vidRunTime = creditsSceneVideo.clip.length;
                //    endTime = vidRunTime - 1;
                //    creditsSceneVideo.Prepare();
                //    while (!creditsSceneVideo.isPrepared)
                //    {
                //        yield return new WaitForSeconds(1.0f);
                //        break;
                //    }
                //    if (creditsSceneVideo.isPrepared)
                //    {
                //        animator.enabled = true;
                //    }
                //    playScreen.texture = creditsSceneVideo.texture;
                //    //animator.SetBool("FadeOut", false);

                //    creditsSceneVideo.Play();
                //    vidReady = true;
                //    creditsSceneAudio.Play();
                //}
            }
        }
        else if (levelSelected || testingMode)
        {
            vidRunTime = loadingScreenVideo.clip.length;
            endTime = vidRunTime - 1;
            loadingScreenVideo.Prepare();
            while (!loadingScreenVideo.isPrepared)
            {
                yield return new WaitForSeconds(1.0f);
                break;
            }
            if (loadingScreenVideo.isPrepared)
            {
                animator.enabled = true;
            }
            playScreen.texture = loadingScreenVideo.texture;
            //animator.SetBool("FadeOut", false);
            loadingScreenVideo.Play();
            vidReady = true;
            //Might change loadingScreenVideo.isPlaying to a flat wait time instead depending on how long it is

        }
        //else if (youWon)
        //{
        //vidRunTime = creditsSceneVideo.clip.length;
        //endTime = vidRunTime - 1;
        //creditsSceneVideo.Prepare();
        //while (!creditsSceneVideo.isPrepared)
        //{
        //    yield return new WaitForSeconds(1.0f);
        //    break;
        //}
        //playScreen.texture = creditsSceneVideo.texture;
        ////animator.SetBool("FadeOut", false);

        //creditsSceneVideo.Play();
        //creditsSceneAudio.Play();


        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if(endCutSceneAudio.isPlaying)
        //{
        //    Debug.Log("Can you hear me?");
        //}
        if (startReady)
            StartCoroutine("PlayVideo");

        if (vidReady)
        {
            if (!levelSelected || (!levelSelected && testingMode))
            {
                if (previousScene == 0 || startScenePlay)
                {
                    currentRunTime = startCutSceneVideo.time;
                    if (currentRunTime >= endTime)
                    {
                        startCutSceneVideo.Pause();
                        Debug.Log("Start Scene Over!");
                        animator.SetBool("FadeOut", true);
                        changeLevelDelay -= 0.1f;
                        if (changeLevelDelay <= 0)
                        {
                            SceneManager.LoadScene(2);
                        }
                    }
                }

                else if (previousScene == 2 || villageToBamboo)
                {
                    currentRunTime = loadingScreenVideo.time;
                    //Might change loadingScreenVideo.isPlaying to a flat wait time instead depending on how long it is
                    if (currentRunTime >= endTime)
                    {
                        loadingScreenVideo.Pause();
                        Debug.Log("Loading Screen Done!");
                        animator.SetBool("FadeOut", true);
                        changeLevelDelay -= 0.1f;
                        if (changeLevelDelay <= 0)
                        {
                            SceneManager.LoadScene(3);

                        }
                    }
                }

                else if (previousScene == 3 || endScenePlay)
                {
                    currentRunTime = endCutSceneVideo.time;
                    //Might change loadingScreenVideo.isPlaying to a flat wait time instead depending on how long it is
                    if (currentRunTime >= endTime)
                    {
                        //GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayEndCredits();
                        //Debug.Log("Playing End Credit Music");
                        endCutSceneVideo.Pause();
                        Debug.Log("End Scene Over!");
                        animator.SetBool("FadeOut", true);
                        endCreditsDelay -= 0.1f;
                        if (endCreditsDelay <= 0)
                        {
                            SceneManager.LoadScene(4);
                            youWon = true;
                        }
                    }
                }
                //if (youWon == true)
                //{
                //   animator.SetBool("FadeOut", false);
                   //GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayEndCredits();
                //    currentRunTime = creditsSceneVideo.time;
                //    if (currentRunTime >= endTime)
                //    {
                //        creditsSceneVideo.Pause();
                //        animator.SetBool("FadeOut", true);
                //        changeLevelDelay -= 0.1f;
                //        if (changeLevelDelay <= 0)
                //        {
                //            SceneManager.LoadScene(0);
                //        }
                //    }
                //}
            }
            else if (levelSelected || (levelSelected && testingMode))
            {
                currentRunTime = loadingScreenVideo.time;
                if (currentRunTime >= endTime)
                {
                    loadingScreenVideo.Pause();
                    animator.SetBool("FadeOut", true);
                    changeLevelDelay -= 0.1f;
                    if (changeLevelDelay <= 0)
                    {
                        if (chosenScene == 2 || loadToVillage)
                        {
                            SceneManager.LoadScene(2);
                        }
                        else if (chosenScene == 3 || loadToBamboo)
                        {
                            SceneManager.LoadScene(3);
                        }
                    }
                }
            }
        }

        //vidReady = false;
        levelTracking.startLoading = false;
    }
}
