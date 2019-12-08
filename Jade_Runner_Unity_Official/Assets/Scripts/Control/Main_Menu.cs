using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : LevelTracking
{
    public GameObject mainMenu;
    public GameObject levelSelect;
    public GameObject howToPlay;

    // Start is called before the first frame update
    void Start()
    {
        
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
        levelSelected = true;
        SceneManager.LoadScene(2);
    }

    public void BambooLevel()
    {
        levelSelected = true;
        SceneManager.LoadScene(3);
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
