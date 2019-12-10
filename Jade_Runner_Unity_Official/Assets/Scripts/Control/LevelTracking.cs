using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracking : MonoBehaviour
{
    public int sceneNumber;
    public int chosenScene;
    public bool levelSelected;
    public int previousScene;
    public bool startLoading;
   
    public int oldPreviousScene;

    // Start is called before the first frame update
    void Start()
    {
        oldPreviousScene = previousScene;
        previousScene = sceneNumber;
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneNumber != SceneManager.GetActiveScene().buildIndex)
        {
            oldPreviousScene = previousScene;
            previousScene = sceneNumber;
            sceneNumber = SceneManager.GetActiveScene().buildIndex;
            startLoading = true;

        }
        if (sceneNumber == 1)
        {
            if(startLoading)
            {
                Cursor.visible = false;
                GameObject levelKicker = GameObject.Find("LevelKicker");
                LoadLevel levelLoader = levelKicker.GetComponent<LoadLevel>();
                levelLoader.levelSelected = levelSelected;
                //levelSelected = false;
                levelLoader.previousScene = previousScene;
                levelLoader.chosenScene = chosenScene;
                levelLoader.startReady = true;
            }
        }

    

        DontDestroyOnLoad(transform.gameObject);
    }
}
