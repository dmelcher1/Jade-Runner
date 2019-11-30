﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BoxCollider levelEnder;
    //^This might need to be modified to a GameObject, 
    //since we might decide to make the level enders their own objects
    public bool levelComplete;
    public bool beatLevel = false;
    //public bool faded = false;
   
    private float changeLevelDelay = 5.0f;
    public PlayerLocomotion playerLocomotion;
    //public Animator levelAnim;
    public Animator animator; 
        //FOR SCENE FADE???
    public GameObject player;
    //public Animator playerAnimator;
    //public Slider healthSlider -> will likely make our own UI for this
    public Image fader;
    //public GameObject fader;
    //public Color alpha;
    private int startHealth;
    private float startFadeDelay;
    private string currentScene;

    
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        levelEnder = GetComponent<BoxCollider>();
        //playerAnimator = player.GetComponent<Animator>();
        playerLocomotion = GameObject.FindObjectOfType<PlayerLocomotion>();
        //fader = GetComponent<Image>();
        //alpha = fader.color;
        startHealth = playerLocomotion.health;
        startFadeDelay = playerLocomotion.fadeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Quit"))
        {
            Application.Quit();
        }

        if(beatLevel == true)
        {
            changeLevelDelay -= 0.1f;
            if(changeLevelDelay <= 0.0f)
            {
                //StartCoroutine("Reset");
                SceneManager.LoadScene(currentScene);
                //SceneManager.LoadScene("NextLevel"); :P
            }
        }
        
        if(playerLocomotion.dead == true && playerLocomotion.fadeDelay <= 0.0f)
        {
            FadeOnDeath();
        }
        if(animator.GetBool("FadeOut") && fader.color.a == 1 && beatLevel != true)
        {
            FadeOver();
        }
    }

    public void FadeOnDeath()
    {
        animator.SetBool("FadeOut", true);
        //faded = true;
    }

    public void FadeOver()
    {
        player.transform.position = playerLocomotion.currentCheckpoint.transform.position;
        player.transform.rotation = playerLocomotion.currentCheckpoint.transform.rotation;
        playerLocomotion.activeCam = playerLocomotion.checkPtCam;
        playerLocomotion.health = startHealth;
        playerLocomotion.currentHealth = playerLocomotion.health;
        playerLocomotion.dead = false;
        playerLocomotion.fadeDelay = startFadeDelay;
        animator.SetBool("FadeOut", false);
        //faded = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            beatLevel = true;
            FadeOnDeath();
            levelEnder.enabled = false;
        }
    }

    IEnumerator Reset ()
    {
        yield return new WaitForSeconds(10.0f);

        SceneManager.LoadScene(currentScene);
    }
}
