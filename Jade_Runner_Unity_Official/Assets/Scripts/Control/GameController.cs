using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameController : LevelTracking
{
    public BoxCollider levelEnder;
    //^This might need to be modified to a GameObject, 
    //since we might decide to make the level enders their own objects
    public bool beatLevel = false;
    //public bool faded = false;
    //public LevelTracking levelTracking;
    //public GameObject levelTracker;
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
    private float startFadeDelay;
    private string currentScene;
    //private int nextScene;
    //private Scene loadingScene;
    //public bool menuScene;
    private GameObject levelTracker;
    public LevelTracking levelTracking;
    public Image healthUI;
    public Sprite[] healthSprites;
    public bool paused;
    public GameObject pauseMenu;
    public GameObject howToPlayMenu;

    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        levelTracker = GameObject.Find("LevelTracker");
        levelTracking = levelTracker.GetComponent<LevelTracking>();
        levelSelected = false;
        levelTracking.levelSelected = levelSelected;

        chosenScene = 0;
        currentScene = SceneManager.GetActiveScene().name;
        
        levelEnder = GetComponent<BoxCollider>();
        
        playerLocomotion = GameObject.FindObjectOfType<PlayerLocomotion>();
        
        
        startFadeDelay = playerLocomotion.fadeDelay;
        //animator.SetBool("FadeOut", false);
        //if(SceneManager.GetActiveScene().name == "VillageMenuScene")
        //{
        //    menuScene = true;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        HealthUI();
        Pause();

        //if(Input.GetButton("Quit"))
        //{
        //    Application.Quit();
        //}

        if(beatLevel == true)
        {
            changeLevelDelay -= 0.1f;
            if(changeLevelDelay <= 0.0f)
            {
                //StartCoroutine("Reset");
                //SceneManager.LoadScene(currentScene);
                //if(sceneNumber == 3)
                //{
                //    GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayEndCredits();
                //}
                SceneManager.LoadScene("LoadScreen");
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
        playerLocomotion.health = playerLocomotion.startHealth;
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

    void HealthUI()
    {
        if (playerLocomotion.health == 4 && !playerLocomotion.poweredUp)
        {
            healthUI.sprite = healthSprites[0];
        }
        if (playerLocomotion.health == 3 && !playerLocomotion.poweredUp)
        {
            healthUI.sprite = healthSprites[1];
        }
        if (playerLocomotion.health == 2 && !playerLocomotion.poweredUp)
        {
            healthUI.sprite = healthSprites[2];
        }
        if (playerLocomotion.health == 1 && !playerLocomotion.poweredUp)
        {
            healthUI.sprite = healthSprites[3];
        }
        if (playerLocomotion.health == 4 && playerLocomotion.poweredUp)
        {
            healthUI.sprite = healthSprites[4];
        }
        if (playerLocomotion.health == 3 && playerLocomotion.poweredUp)
        {
            healthUI.sprite = healthSprites[5];
        }
        if (playerLocomotion.health == 2 && playerLocomotion.poweredUp)
        {
            healthUI.sprite = healthSprites[6];
        }
        if (playerLocomotion.health == 1 && playerLocomotion.poweredUp)
        {
            healthUI.sprite = healthSprites[7];
        }
        if (playerLocomotion.health == 0)
        {
            healthUI.sprite = healthSprites[8];
        }
    }

    //IEnumerator Reset ()
    //{
    //    yield return new WaitForSeconds(10.0f);

    //    SceneManager.LoadScene(currentScene);
    //}

    public void Pause()
    {
        if(Input.GetButton("Pause") || Input.GetButton("Pause2"))
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void HowToPlay()
    {
        pauseMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
    }

    public void BackButton()
    {
        howToPlayMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void QuitToMenu()
    {
        //SceneManager.LoadScene(1);
        //levelTracking.chosenScene = 0;
        //levelTracking.previousScene = 0;
        //levelTracking.oldPreviousScene = 0;
        //levelTracking.startLoading = false;
        //levelTracking.levelSelected = false;
        levelTracker.tag = "DestroyThis";
        GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().BackToMenu();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting!");
        Application.Quit();
    }


}
