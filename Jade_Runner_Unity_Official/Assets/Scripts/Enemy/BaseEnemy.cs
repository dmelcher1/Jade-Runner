using System.Collections;
using System.Collections.Generic;
//using UnityEngine.AI;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int health;
    public bool destroyed;
    protected PlayerLocomotion playerLocomotion;
    protected GameObject playerLocation;
    protected Animator statueAnim;

    private void Awake()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player");
        playerLocomotion = playerLocation.GetComponent<PlayerLocomotion>();
        statueAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
