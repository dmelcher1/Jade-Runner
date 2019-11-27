using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPt_Light : MonoBehaviour
{
    //public GameObject checkLamp;
    private PlayerLocomotion playerLocomotion;
    public Light urGoodMan;
    public bool tripped;
    public bool flareUp = true;
    private float transitionTime = 10.0f;

    public Color startColor;
    public Color endColor;

    // Start is called before the first frame update
    void Start()
    {
        playerLocomotion = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLocomotion>();
        //urGoodMan = gameObject.transform.GetChild(0).gameObject.GetComponent<Light>();
        startColor = urGoodMan.color;
        endColor = Color.green;
        //red = urGoodMan.color.r;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerLocomotion.currentCheckpoint == this.gameObject)
        {
            tripped = true;
        }
        if(tripped)
        {
            if(urGoodMan.intensity < 10.0f && flareUp)
            {
                urGoodMan.intensity += 0.5f;
            }
            if(urGoodMan.intensity >= 10.0f)
            {
                flareUp = false;
            }
            if(!flareUp && urGoodMan.intensity > 3.0f)
            {
                urGoodMan.intensity -= 0.2f;
            }
            if(urGoodMan.color != endColor)
            {
                urGoodMan.color = Color.Lerp(startColor, endColor, transitionTime);
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(gameObject.CompareTag("Player"))
    //    {
    //        tripped = true;
    //    }
    //}
}
