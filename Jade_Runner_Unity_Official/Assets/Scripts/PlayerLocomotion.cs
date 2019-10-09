using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    public float moveSpeed;
    public float lookSpeed = 5f;
    private float turnAngle;
    private Quaternion targetRotation;

    private Vector2 playerInput;

    [SerializeField]
    private int fruitCount;

    public bool airBorne = false;
    public bool dblJump = false;
    public bool inKillbox = false;
    private float killTimer = 10.0f;
    [SerializeField]
    private float jumpTimer;
    [SerializeField]
    private float dblJumpTimer;
    [SerializeField]
    private int jumpCount;
    public float jumpForce;
    public float dblJumpForce;

    public Cinemachine.CinemachineVirtualCamera pathOneCam;

    public Cinemachine.CinemachineVirtualCamera pathTwoCam;

    public Cinemachine.CinemachineVirtualCamera activeCam;

    private Rigidbody rb;

    //public Animator playerAnim;
    //public bool noInput;
    public int health;

    //private float hitLayerWeight;
    public bool dead;

    public GameObject recentCheckpoint;

    public GameObject[] checkpointList;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        activeCam = pathOneCam;
        pathTwoCam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        jumpTimer -= 0.1f;
        if(inKillbox)
        {
            killTimer -= 0.1f;
        }
        else
        {
            killTimer = 10.0f;
        }
        if(killTimer < 0)
        {
            health -= 1;
            killTimer = 10.0f;
        }
        //NEED CONDITION + CODE FOR DEATH, FADE TO BLACK, AND RESPAWN. GO BACK AND REVIEW PRISONER CODE

        if(airBorne && dblJump)
        {
            dblJumpTimer -= 0.1f;
        }
       
        if(Input.GetButtonUp("Jump") && airBorne && jumpTimer <= 2.5f && jumpCount < 1)
        {
            dblJump = true;
            jumpCount += 1;
        }

        //if(!noInput) <- will be used to decide if player is moving or not for idle animation
        //{ }

        if (Input.GetButton("Jump"))
        {
            if (!airBorne && jumpTimer <= 0)
            {
                jumpTimer = 3.0f;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                airBorne = true;
            }
        }
        if(Input.GetButton("Jump") && dblJumpTimer < 0)
        {
            rb.AddForce(Vector3.up * dblJumpForce, ForceMode.Impulse);
            dblJump = false;
            dblJumpTimer = 0;
        }

        PlayerInput();

        //Keeps player facing direction of last input
        if (Mathf.Abs(playerInput.x) < 1 && Mathf.Abs(playerInput.y) < 1) return;

        CalcDirect();
        PlayerRotate();
        PlayerMove();
    }

    void PlayerInput()
    {
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.y = Input.GetAxisRaw("Vertical");
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !Input.GetButton("Jump"))
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
            recentCheckpoint.tag = ("OldPoint");
            recentCheckpoint = other.gameObject;
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
