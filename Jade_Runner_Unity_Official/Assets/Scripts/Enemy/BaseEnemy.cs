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
        //if(health <= 0)
        //{
        //    destroyed = true;
        //}
        //statueAnim.SetBool("Destroyed", destroyed);
        //if (destroyed)
        //{
        //    Debug.Log("Please die?");
        //    StartCoroutine("DestroyEnemy");
        //}
    }

    IEnumerator DestroyEnemy()
    {
        //Instantiate puff of dust at transform.position
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
