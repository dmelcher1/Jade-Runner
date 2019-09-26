using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //private in future if == to fullSpeed; fullSpeed will be publicly altered as needed
    public float moveSpeed;

    [SerializeField]
    private float currentSpeed;

    private int fruitCount;
    
    public bool airBorne;
    private float horizontal;
    private float vertical;

    private float jumpTimer;
    public float jumpForce;

    private Rigidbody rb;

    public Transform dollyOne;

    //public Animator playerAnim;
    //public bool noInput;
    //public int health;

    //private float hitLayerWeight;
    //public bool dead;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        jumpTimer -= 0.1f;

        //if(!noInput) <- will be used to decide if player is moving or not for idle animation
        //{ }


        if(Input.GetButton("Jump") && !airBorne && jumpTimer <= 0)
        {
            jumpTimer = 3.0f;
            rb.AddForce(Vector3.up * jumpForce);
            airBorne = true;
        }

        PlayerMoves();
	}

    void PlayerMoves()
    {
        currentSpeed = new Vector2(horizontal, vertical).sqrMagnitude;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 playerInput = new Vector3(horizontal, 0f, vertical);

        Vector3 playerMove = Vector3.ClampMagnitude(playerInput, 1.0f) * moveSpeed;
        //This needs to be altered to match camera perspective
        //**
        //playerInput = dollyOne.transform.TransformDirection(playerInput);
        //**
        transform.Translate(playerMove, Space.Self);
        //playerAnim.SetFloat("Speed", currentSpeed); <- for animation later on


        //Testing cam/player/input adjustment below

        //Vector3 dollyForward = dollyOne.forward;
        //Vector3 dollyRight = dollyOne.right;

        //dollyForward.y = 0;
        //dollyRight.y = 0;
        //dollyForward = dollyForward.normalized;
        //dollyRight = dollyRight.normalized;

        //transform.position += (dollyForward * playerInput.y + dollyRight * playerInput.x) * Time.deltaTime * 5;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground")) //Don't forget to actually make this tag!
        {
            airBorne = false;
        }
        if(collision.gameObject.CompareTag("Fruit"))
        {
            Debug.Log("That's Not A Wampa!");
            fruitCount += 1;
            Destroy(collision.gameObject);
        }
    }
}
