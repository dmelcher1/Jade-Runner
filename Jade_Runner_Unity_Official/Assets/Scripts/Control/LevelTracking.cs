using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracking : MonoBehaviour
{
    public int sceneNumber;
    public static int chosenScene;
    public static bool levelSelected;
    public static int previousScene;
    //public int oldPreviousScene;

    // Start is called before the first frame update
    void Start()
    {
       // oldPreviousScene = previousScene;
        previousScene = sceneNumber;
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Scene Number: " + sceneNumber);
        Debug.Log("Previous Scene: " + previousScene);
        Debug.Log("Level Selected: " + levelSelected);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
