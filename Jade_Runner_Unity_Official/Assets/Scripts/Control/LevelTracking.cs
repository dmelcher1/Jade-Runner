using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracking : MonoBehaviour
{
    public int sceneNumber;
    private static int chosenScene;
    private static int previousScene;
    private int oldPreviousScene;

    // Start is called before the first frame update
    void Start()
    {
        oldPreviousScene = previousScene;
        previousScene = sceneNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
