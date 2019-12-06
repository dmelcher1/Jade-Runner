using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LoadLevel : MonoBehaviour
{
    public Animator animator;
    public Image fader;
    private LevelTracking levelTracking;
    public VideoPlayer loadingScreen;
    public VideoPlayer startCutScene;
    public VideoPlayer endCutScene;

    // Start is called before the first frame update
    void Start()
    {
        levelTracking = GameObject.Find("LevelTracker").GetComponent<LevelTracking>();
        StartCoroutine("PlayVideo");
    }

    //IEnumerator PlayVideo()
    //{
    //    if()
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
