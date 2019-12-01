using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Rhino : BaseEnemy
{
    public GameObject playerDetector;
    private PlayerDetector playerDetectorScript;
    public GameObject rhinoRenderer;
    private Rigidbody rb;
    [SerializeField]
    private float rhinoVelocity;
    public float chargeSpeed;
    public bool charge;

    // Start is called before the first frame update
    void Start()
    {
        playerDetectorScript = playerDetector.GetComponent<PlayerDetector>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rhinoVelocity = rb.velocity.x;
        statueAnim.SetFloat("RhinoVelocity", rhinoVelocity);
        if(playerDetectorScript.playerDetected)
        {
            //Instantiate Dust Cloud at transform.position
            statueAnim.enabled = true;
            rhinoRenderer.SetActive(true);
            //charge = true;
        }
        statueAnim.SetBool("Charging", charge);
        if(charge)
        {
            rb.velocity = Vector3.right * chargeSpeed * -1;
        }
        else if(!charge)
        {
            rb.velocity = Vector3.zero;
        }
        if (health <= 0)
        {
            destroyed = true;
        }
        statueAnim.SetBool("Destroyed", destroyed);
        if (destroyed)
        {
            Debug.Log("Please die?");
            StartCoroutine("DestroyEnemy");
            destroyed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Stopper" || other.gameObject.tag == "Player")
        {
            Debug.Log("Kablooie!");
            charge = false;
 
            destroyed = true;
        }
    }
}
