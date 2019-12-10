using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Rhino : BaseEnemy
{
    public int health;
    public GameObject playerDetector;
    private PlayerDetector playerDetectorScript;
    public GameObject rhinoRenderer;
    private Rigidbody rb;
    [SerializeField]
    private float rhinoVelocity;
    public float chargeSpeed;
    public bool charge;
    public bool hitByPlayer;
    public GameObject spawnSplosion;
    public GameObject deathSplosion;
    public GameObject deathSplosionUpper;
    public Transform explosionTarget;
    public bool spawned;

    // Start is called before the first frame update
    void Start()
    {
        playerDetectorScript = playerDetector.GetComponent<PlayerDetector>();
        rb = GetComponent<Rigidbody>();
        spawned = true;
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
            if(spawned)
            {
                Instantiate(spawnSplosion, explosionTarget.position, explosionTarget.rotation);
                spawned = false;
            }
            
            //charge = true;
        }
        statueAnim.SetBool("Charging", charge);
        if(charge)
        {
            //rb.AddRelativeForce(Vector3.right * chargeSpeed * -1);
            transform.Translate(Vector3.forward * chargeSpeed * Time.deltaTime, Space.Self);
            //rb.velocity = Vector3.right * chargeSpeed * -1;
            //transform.position = transform.right * chargeSpeed * -1 * Time.deltaTime;
        }
        else if(!charge)
        {
            rb.velocity = Vector3.zero;
        }
        if (hitByPlayer)
        {
            destroyed = true;
        }
        statueAnim.SetBool("Destroyed", destroyed);
        if (destroyed)
        {
            Debug.Log("Please die?");
            Instantiate(deathSplosion, explosionTarget.position, explosionTarget.rotation);
            //Instantiate(deathSplosionUpper, new Vector3(explosionTarget.position.x, explosionTarget.position.y + 1, explosionTarget.position.z), explosionTarget.rotation);
            StartCoroutine("DestroyEnemy");
            destroyed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.gameObject.tag == "Player" ||
        if(other.gameObject.tag == "Stopper")
        {
            Debug.Log("Kablooie!");
            charge = false;
            destroyed = true;
        }
        else if(charge && other.gameObject.tag == "Player" && !hitByPlayer)
        {
            Debug.Log("Kablooie!");
            playerLocomotion.attackedByEnemy = true;
            destroyed = true;
            charge = false;
            
        }

        if (other.gameObject.tag == "TigerClaw")
        {
            hitByPlayer = true;
        }
    }
}
