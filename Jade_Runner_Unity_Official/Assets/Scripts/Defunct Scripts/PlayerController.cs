using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private in future if == to fullSpeed; fullSpeed will be publicly altered as needed
    public float moveSpeed;
    [SerializeField]
    private float currentSpeed;
    private float lookSpeed = 5f;
    private float turnAngle;
    private Quaternion targetRotation;

    private Vector3 playerInput;

    private int fruitCount;

    public float horizontal;
    public float vertical;

    public bool airBorne;
    private float jumpTimer;
    public float jumpForce;
   
    private Rigidbody rb;

    public Transform activeCam;

    //public Animator playerAnim;
    //public bool noInput;
    //public int health;

    //private float hitLayerWeight;
    //public bool dead;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        
       if (Mathf.Abs(playerInput.x) < 1 && Mathf.Abs(playerInput.z) < 1) return;

        CalcDirect();
        PlayerRotate();
        PlayerMove();


        //jumpTimer -= 0.1f;

        ////if(!noInput) <- will be used to decide if player is moving or not for idle animation
        ////{ }


        //if (Input.GetButton("Jump") && !airBorne && jumpTimer <= 0)
        //{
        //    jumpTimer = 3.0f;
        //    rb.AddForce(Vector3.up * jumpForce);
        //    airBorne = true;
        //}

        //PlayerMoves();
    }

    void PlayerInput()
    {
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.z = Input.GetAxisRaw("Vertical");


    }

    void CalcDirect()
    {
        turnAngle = Mathf.Atan2(playerInput.x, playerInput.z);
        turnAngle = Mathf.Rad2Deg * turnAngle;
        turnAngle += activeCam.eulerAngles.z;
    }

    void PlayerRotate()
    {
        targetRotation = Quaternion.Euler(0, turnAngle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
    }

    void PlayerMove()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void PlayerMoves()
    {
        //Vector3 playerForward = activeCam.forward.normalized;
        //Vector3 playerRight = activeCam.right.normalized;

        //currentSpeed = new Vector2(horizontal, vertical).sqrMagnitude;

        //horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");
        //Vector3 playerInput = new Vector3(horizontal, 0f, vertical);




        ////Vector3 playerMove = Vector3.ClampMagnitude(playerInput, 1.0f) * moveSpeed * Time.deltaTime;
        //Vector3 playerMove = playerInput * moveSpeed * Time.deltaTime;
        ////This needs to be altered to match camera perspective
        ////**
        ////playerInput = dollyOne.transform.TransformDirection(playerInput);
        ////**

        ////Vector3 playerMove = (playerForward * vertical * moveSpeed) + (playerRight * horizontal * moveSpeed);


        ////(DON'T TOUCH)
        //if (playerInput.magnitude > 0)
        //{
        //    Quaternion lookDirection = Quaternion.LookRotation(playerInput);

        //    transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * lookSpeed);
        //}
        ////(DON'T TOUCH)

        ////rb.MovePosition(playerMove * Time.deltaTime);
        ////^Potentially reactivate
        ////transform.Translate(playerMove, Space.Self);
        //transform.Translate(playerMove);

       
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
        if (collision.gameObject.CompareTag("Ground")) //Don't forget to actually make this tag!
        {
            airBorne = false;
        }
        if (collision.gameObject.CompareTag("Fruit"))
        {
            Debug.Log("That's Not A Wampa!");
            fruitCount += 1;
            Destroy(collision.gameObject);
        }
    }
}
