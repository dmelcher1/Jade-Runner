using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMove : MonoBehaviour
{
    private Vector3 startPos;
    Collider m_collider;
    //public float moveUp;
    //public float moveDown;
    public GameObject cartStopper;
    private PlayerLocomotion playerLocomotion;
    public bool switchedOn;
    public bool activeCart;
    public bool paired;
    public GameObject pairedCart;
   //public int myID;
    // Start is called before the first frame update
    void Start()
    {
        playerLocomotion = GameObject.FindWithTag("Player").GetComponent<PlayerLocomotion>();
        m_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "CurrentCart")
        {
            activeCart = true;
            if (playerLocomotion.dead)
            {
                switchedOn = false;
            }
            if (switchedOn)
            {
                cartStopper.SetActive(true);
                if(m_collider.enabled)
                {
                    m_collider.enabled = false;
                }
            }
            else if (!switchedOn)
            {
                cartStopper.SetActive(false);
                if(!m_collider.enabled)
                {
                    m_collider.enabled = true;
                }
            }
        }
        else
        {
            activeCart = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Move and Collider Off");

            //transform.Translate(0f, moveUp, 0f);
            if (!playerLocomotion.dead)
                switchedOn = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && gameObject.tag == "CurrentCart" && paired)
        {
            pairedCart.SetActive(true);
        }
    }
}
