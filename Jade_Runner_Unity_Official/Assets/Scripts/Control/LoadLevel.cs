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
    public VideoPlayer creditsSceneVideo;
    public AudioSource startCutSceneAudio;
    public AudioSource endCutSceneAudio;
    public AudioSource creditsSceneAudio;
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
    

    // Start is called before the first frame update
    void Start()
    {
        //animator.SetBool("FadeOut", true);
        //levelTracking = GameObject.Find("LevelTracker").GetComponent<LevelTracking>();
        //levelSelected = levelTracking.levelSelected;
        //previousScene = levelTracking.previousScene;
        //chosenScene = levelTracking.chosenScene;
        StartCoroutine("PlayVideo");
    }

    IEnumerator PlayVideo()
    {
        
        if(!levelSelected)
        {
            if (previousScene == 0)
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
                if(startCutSceneVideo.isPrepared)
                {
                    animator.enabled = true;
                }
                playScreen.texture = startCutSceneVideo.texture;
                //animator.SetBool("FadeOut", false);
                
                startCutSceneVideo.Play();
                vidReady = true;
                startCutSceneAudio.Play();
            }
            if (previousScene == 2)
            {
                vidRunTime = loadingScreenVideo.clip.length;
                endTime = vidRunTime - 1;
                loadingScreenVideo.Prepare();
                while (!loadingScreenVideo.isPrepared)
                {
                    yield return new WaitForSeconds(1.0f);
                    break;
                }
                playScreen.texture = loadingScreenVideo.texture;
                //animator.SetBool("FadeOut", false);
                loadingScreenVideo.Play();
                GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayForrest();
            }
            if (previousScene == 3)
            {
                vidRunTime = endCutSceneVideo.clip.length;
                endTime = vidRunTime - 1;
                endCutSceneVideo.Prepare();
                while (!endCutSceneVideo.isPrepared)
                {
                    yield return new WaitForSeconds(1.0f);
                    break;
                }
                playScreen.texture = endCutSceneVideo.texture;
                //animator.SetBool("FadeOut", false);
               
                endCutSceneVideo.Play();

                if(youWon)
                {
                    GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayEndCredits();
                    vidRunTime = creditsSceneVideo.clip.length;
                    endTime = vidRunTime - 1;
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
                }
                    
                
            }
            
            else if(levelSelected)
            {
                vidRunTime = loadingScreenVideo.clip.length;
                endTime = vidRunTime - 1;
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
                    
            }
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
        if (vidReady)
        {
            if(!levelSelected)
            {
                if (previousScene == 0)
                {
                    currentRunTime = startCutSceneVideo.time;
                    if (currentRunTime >= endTime)
                    {
                        Debug.Log("Start Scene Over!");
                        animator.SetBool("FadeOut", true);
                        changeLevelDelay -= 0.1f;
                        if (changeLevelDelay <= 0)
                        {
                            SceneManager.LoadScene(2);
                        }
                    }
                }
                    
                else if (previousScene == 2)
                {
                    currentRunTime = loadingScreenVideo.time;
                    //Might change loadingScreenVideo.isPlaying to a flat wait time instead depending on how long it is
                    if (currentRunTime >= endTime)
                    {
                        Debug.Log("Loading Screen Done!");
                        animator.SetBool("FadeOut", true);
                        changeLevelDelay -= 0.1f;
                        if (changeLevelDelay <= 0)
                        {
                            SceneManager.LoadScene(3);
                        }
                    }
                }
                    
                else if (previousScene == 3 && !youWon)
                {
                    currentRunTime = endCutSceneVideo.time;
                    //Might change loadingScreenVideo.isPlaying to a flat wait time instead depending on how long it is
                    if (currentRunTime >= endTime)
                    {
                        Debug.Log("End Scene Over!");
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
                    animator.SetBool("FadeOut", false);
                    currentRunTime = creditsSceneVideo.time;
                    if (currentRunTime >= endTime)
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
        }
            else if(levelSelected)
            {
                currentRunTime = loadingScreenVideo.time;
                if (currentRunTime >= endTime)
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
}
