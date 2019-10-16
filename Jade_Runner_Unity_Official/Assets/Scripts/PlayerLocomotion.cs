﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    public float stickDeadZone = 0.25f;
    public float moveSpeed;
    public float lookSpeed = 5f;
    private float turnAngle;
    private Quaternion targetRotation;

    private Vector2 playerInput;

    private string jumpControl;

    private string jumpKeyboard = "Jump";
    private string jumpJoystick = "Jump2";

    [SerializeField]
    private int fruitCount;

    public bool airBorne = false;
    public bool dblJump = false;
    public bool inKillbox = false;
    public bool canJump = true;
    public bool keyboardActive;
    public bool controllerActive;
    [SerializeField]
    private float killTimer = 3.0f;
    [SerializeField]
    private float jumpTimer;
    [SerializeField]
    private float dblJumpTimer;
    [SerializeField]
    private int jumpCount;
    public float jumpForce;
    public float dblJumpForce;
    [SerializeField]
    public float fadeDelay = 10.0f;

    public Cinemachine.CinemachineVirtualCamera pathOneCam;

    public Cinemachine.CinemachineVirtualCamera pathTwoCam;

    public Cinemachine.CinemachineVirtualCamera activeCam;

    private Rigidbody rb;

    //public Animator playerAnim;
    //public bool noInput;
    public int health;

    //private float hitLayerWeight;
    public bool dead;

    public GameObject currentCheckpoint;

    //public GameObject recentCheckpoint;

    //public GameObject[] checkpointList;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = currentCheckpoint.transform.position;
        rb = GetComponent<Rigidbody>();
        activeCam = pathOneCam;
        pathTwoCam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(keyboardActive && !controllerActive)
        {
            jumpControl = jumpKeyboard;
        }
        else if(controllerActive && !keyboardActive)
        {
            jumpControl = jumpJoystick;
        }

        jumpTimer -= 0.1f;
        if(inKillbox)
        {
            killTimer -= 0.1f;
        }
        else
        {
            killTimer = 3.0f;
        }
        if(killTimer < 0)
        {
            health -= 1;
            killTimer = 3.0f;
        }
        //NEED CONDITION + CODE FOR DEATH, FADE TO BLACK, AND RESPAWN. GO BACK AND REVIEW PRISONER CODE

        if(airBorne && dblJump)
        {
            dblJumpTimer -= 0.1f;
        }
       
        if(Input.GetButtonUp(jumpControl) && airBorne && jumpTimer <= 2.5f && jumpCount < 1)
        {
            dblJump = true;
            jumpCount += 1;
        }

        //if(!noInput) <- will be used to decide if player is moving or not for idle animation
        //{ }

        if (Input.GetButtonDown(jumpControl))
        {
            if (!airBorne && jumpTimer <= 0)
            {
                jumpTimer = 3.0f;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                airBorne = true;
                canJump = false;
            }
        }
        if(Input.GetButtonDown(jumpControl) && dblJumpTimer < 0)
        {
            rb.AddForce(Vector3.up * dblJumpForce, ForceMode.Impulse);
            dblJump = false;
            dblJumpTimer = 0;
        }

        if(Input.GetButtonUp(jumpControl))
        {
            canJump = true;
        }

        if (health <= 0)
        {
            dead = true;
            fadeDelay -= 0.1f;
        }

        PlayerInput();

        //Keeps player facing direction of last input
        if (Mathf.Abs(playerInput.x) < 1 && Mathf.Abs(playerInput.y) < 1) return;

        CalcDirect();
        PlayerRotate();
        PlayerMove();
        //PlayerMoveKeyboard()
        //PlayerMoveController()
    }

    void PlayerInput()
    {
        if(keyboardActive)
        {
            playerInput.x = Input.GetAxisRaw("Horizontal");
            playerInput.y = Input.GetAxisRaw("Vertical");
        }
        else if(controllerActive)
        {
            //controlstick input here
            playerInput.x = Input.GetAxisRaw("Horizontal2");
            playerInput.y = Input.GetAxisRaw("Vertical2");

            if(playerInput.magnitude < stickDeadZone)
            {
                playerInput = Vector2.zero;
            }
            else
            {
                playerInput = playerInput.normalized * ((playerInput.magnitude - stickDeadZone) / (1 - stickDeadZone));
            }
        }
    }

    void CalcDirect()
    {
        turnAngle = Mathf.Atan2(playerInput.x, playerInput.y);
        turnAngle = Mathf.Rad2Deg * turnAngle;
        turnAngle += activeCam.transform.eulerAngles.y;
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && canJump == true)
        {
            airBorne = false;
            dblJump = false;
            dblJumpTimer = 0;
            jumpCount = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("Checkpoint"))
        {
            currentCheckpoint = other.gameObject;
            currentCheckpoint.tag = ("OldPoint");
        }
        if (other.gameObject.tag == ("Path1"))
        {
            pathOneCam.gameObject.SetActive(true);
            activeCam = pathOneCam;
            pathOneCam.Priority = 11;
            pathTwoCam.Priority = 1;
            pathTwoCam.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == ("Path2"))
        {
            pathTwoCam.gameObject.SetActive(true);
            activeCam = pathTwoCam;
            pathTwoCam.Priority = 11;
            pathOneCam.Priority = 1;
            pathOneCam.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == ("Fruit"))
        {
            Debug.Log("That's Not A Wampa!");
            fruitCount += 1;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == ("Killbox"))
        {
            inKillbox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == ("Killbox"))
        {
            inKillbox = false;
        }
    }

}
