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
        levelTracker = GameObject.Find("LevelTracker");
        levelTracking = levelTracker.GetComponent<LevelTracking>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void VillageLevel()
    {
        levelTracking.levelSelected = true;
        chosenScene = 2;
        levelTracking.chosenScene = chosenScene;
        SceneManager.LoadScene(1);
    }

    public void BambooLevel()
    {
        levelTracking.levelSelected = true;
        chosenScene = 3;
        levelTracking.chosenScene = chosenScene;
        SceneManager.LoadScene(1);
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void HowToPlay()
    {
        mainMenu.SetActive(false);
        howToPlay.SetActive(true);
    }

    public void LevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        howToPlay.SetActive(false);
    }
}
