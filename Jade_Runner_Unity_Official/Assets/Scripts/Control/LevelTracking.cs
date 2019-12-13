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
    public bool theEnd;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("DestroyThis"));
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
                //mainMenu = false;
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
        if(sceneNumber == 3)
        {
            if(GameObject.Find("LevelEnd").GetComponent<GameController>().beatLevel)
            {
                theEnd = true;
            }
        }
        if(theEnd)
        {
            GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().PlayEndCredits();
            //GameObject.Find("WwiseGlobal").GetComponent<AudioManager>().
        }

        DontDestroyOnLoad(transform.gameObject);
    }
}
