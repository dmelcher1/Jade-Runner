using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelect;
    public GameObject howToPlay;
    private GameObject levelTracker;
    private LevelTracking levelTracking;
    private int chosenScene;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        levelTracker = GameObject.Find("LevelTracker");
        levelTracking = levelTracker.GetComponent<LevelTracking>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        AkSoundEngine.PostEvent("UI_Select", gameObject);
        GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayVillageScene();
        GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().MuteMenuAmb();
        Debug.Log("PlayingVillageSceneMusic");
        //AkSoundEngine.PostEvent("villageStart", gameObject);
        SceneManager.LoadScene(1);
        
    }

    public void VillageLevel()
    {
        AkSoundEngine.PostEvent("UI_Select", gameObject);
        GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayVillageImmediately();
        Debug.Log("PlayingVillageSceneMusic");
        levelTracking.levelSelected = true;
        chosenScene = 2;
        levelTracking.chosenScene = chosenScene;
        SceneManager.LoadScene(1);
    }

    public void BambooLevel()
    {
        AkSoundEngine.PostEvent("UI_Select", gameObject);
        GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayForrest();
        GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().StopMenuAmb();
        levelTracking.levelSelected = true;
        chosenScene = 3;
        levelTracking.chosenScene = chosenScene;
        SceneManager.LoadScene(1);
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
        AkSoundEngine.PostEvent("UI_Select", gameObject);
    }

    public void HowToPlay()
    {
        mainMenu.SetActive(false);
        howToPlay.SetActive(true);
        AkSoundEngine.PostEvent("UI_Hover", gameObject);
    }

    public void LevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        AkSoundEngine.PostEvent("UI_Hover", gameObject);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        howToPlay.SetActive(false);
        AkSoundEngine.PostEvent("UI_Hover", gameObject);
    }
}
