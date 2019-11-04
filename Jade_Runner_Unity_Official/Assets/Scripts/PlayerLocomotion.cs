using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    public float stickDeadZone = 0.25f;
    public float moveSpeed;
    public float lookSpeed = 5f;
    private float turnAngle;
    private Quaternion targetRotation;

    [SerializeField]
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
    public float fadeDelay;

    public Cinemachine.CinemachineVirtualCamera pathOneCam;

    public Cinemachine.CinemachineVirtualCamera pathTwoCam;

    public Cinemachine.CinemachineVirtualCamera pathThreeCam;

    public Cinemachine.CinemachineVirtualCamera pathFourCam;

    public Cinemachine.CinemachineVirtualCamera pathFiveCam;

    public Cinemachine.CinemachineVirtualCamera pathSixCam;

    public Cinemachine.CinemachineVirtualCamera pathSevenCam;

    public Cinemachine.CinemachineVirtualCamera activeCam;

    public Cinemachine.CinemachineVirtualCamera checkPtCam;

    private Rigidbody rb;

    public Collider triggerCollider;

    public int health;
    public bool hit = false;
   
    public bool dead;

    public GameObject currentCheckpoint;

    //public Color flashColor;
    //public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public GameObject characterBody;

    //public Animator playerAnim;

    //public bool noInput;

    //private float hitLayerWeight;

    //public GameObject recentCheckpoint;

    //public GameObject[] checkpointList;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = currentCheckpoint.transform.position;
        rb = GetComponent<Rigidbody>();
        activeCam = pathOneCam;
    }

    // Update is called once per frame
    void Update()
    {

        //if(!noInput) <- will be used to decide if player is moving or not for idle animation
        //{ }
        JumpControls();

        KillBox();
        DamageDeath();

       
        if (keyboardActive)
        {
            //Debug.Log("Can Move?");
            playerInput.x = Input.GetAxisRaw("Horizontal");
            playerInput.y = Input.GetAxisRaw("Vertical");
        }
        else if (controllerActive)
        {
            //controlstick input here
            playerInput.x = Input.GetAxisRaw("Horizontal2");
            playerInput.y = Input.GetAxisRaw("Vertical2");

            if (playerInput.magnitude < stickDeadZone)
            {
                playerInput = Vector2.zero;
            }
            else
            {
                playerInput = playerInput.normalized * ((playerInput.magnitude - stickDeadZone) / (1 - stickDeadZone));
            }
        }

        //Keeps player facing direction of last input
        if (Mathf.Abs(playerInput.x) < 1 && Mathf.Abs(playerInput.y) < 1) return;

        
        //PlayerInput();
        CalcDirect();
        PlayerRotate();
        PlayerMove();
        //PlayerMoveKeyboard()
        //PlayerMoveController()
    }

    void JumpControls()
    {
        if (keyboardActive && !controllerActive)
        {
            jumpControl = jumpKeyboard;
        }
        else if (controllerActive && !keyboardActive)
        {
            jumpControl = jumpJoystick;
        }

        jumpTimer -= 0.1f;

        if (airBorne && dblJump)
        {
            dblJumpTimer -= 0.1f;
        }

        if (Input.GetButtonUp(jumpControl) && airBorne && jumpTimer <= 2.5f && jumpCount < 1)
        {
            dblJump = true;
            jumpCount += 1;
        }

        if (Input.GetButtonDown(jumpControl))
        {
            //Debug.Log("Jumping");
            if (!airBorne && jumpTimer <= 0)
            {
                jumpTimer = 3.0f;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canJump = false;
                airBorne = true;
            }
        }
        if (Input.GetButtonDown(jumpControl) && dblJumpTimer < 0)
        {
            rb.AddForce(Vector3.up * dblJumpForce, ForceMode.Impulse);
            dblJump = false;
            dblJumpTimer = 0;
        }

        if (Input.GetButtonUp(jumpControl))
        {
            canJump = true;
        }
    }

    //void PlayerInput()
   //{
       
   // }

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

    void KillBox()
    {
        if (inKillbox)
        {
            killTimer -= 0.1f;
        }
        else
        {
            killTimer = 1.0f;
        }
        if (killTimer < 0)
        {
            health = 0;
            killTimer = 3.0f;
        }
    }

    void DamageDeath()
    {
        if (health <= 0)
        {
            dead = true;
            fadeDelay -= 0.1f;
        }
        if (hit && health > 1)
        {
            StartCoroutine("InvisiFrames");
        }
        else if(hit && health <= 1)
        {
            health -= 1;
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("landed!");
            if(canJump)
            {
                airBorne = false;
                dblJump = false;
                dblJumpTimer = 0;
                jumpCount = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("Checkpoint"))
        {
            currentCheckpoint = other.gameObject;
            currentCheckpoint.tag = ("OldPoint");
            checkPtCam = activeCam;
        }
        if (other.gameObject.tag == ("Path1"))
        {
            pathOneCam.gameObject.SetActive(true);
            activeCam = pathOneCam;
            pathOneCam.Priority = 11;
            pathTwoCam.Priority = 1;
            pathThreeCam.Priority = 1;
            pathFourCam.Priority = 1;
            pathFiveCam.Priority = 1;
            pathSixCam.Priority = 1;
            pathSevenCam.Priority = 1;
            pathTwoCam.gameObject.SetActive(false);
            pathThreeCam.gameObject.SetActive(false);
            pathFourCam.gameObject.SetActive(false);
            pathFiveCam.gameObject.SetActive(false);
            pathSixCam.gameObject.SetActive(false);
            pathSevenCam.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == ("Path2"))
        {
            pathTwoCam.gameObject.SetActive(true);
            activeCam = pathTwoCam;
            pathOneCam.Priority = 1;
            pathTwoCam.Priority = 11;
            pathThreeCam.Priority = 1;
            pathFourCam.Priority = 1;
            pathFiveCam.Priority = 1;
            pathSixCam.Priority = 1;
            pathSevenCam.Priority = 1;
            pathOneCam.gameObject.SetActive(false);
            pathThreeCam.gameObject.SetActive(false);
            pathFourCam.gameObject.SetActive(false);
            pathFiveCam.gameObject.SetActive(false);
            pathSixCam.gameObject.SetActive(false);
            pathSevenCam.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == ("Path3"))
        {
            pathThreeCam.gameObject.SetActive(true);
            activeCam = pathThreeCam;
            pathOneCam.Priority = 1;
            pathTwoCam.Priority = 1;
            pathThreeCam.Priority = 11;
            pathFourCam.Priority = 1;
            pathFiveCam.Priority = 1;
            pathSixCam.Priority = 1;
            pathSevenCam.Priority = 1;
            pathTwoCam.gameObject.SetActive(false);
            pathOneCam.gameObject.SetActive(false);
            pathFourCam.gameObject.SetActive(false);
            pathFiveCam.gameObject.SetActive(false);
            pathSixCam.gameObject.SetActive(false);
            pathSevenCam.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == ("Path4"))
        {
            pathFourCam.gameObject.SetActive(true);
            activeCam = pathFourCam;
            pathOneCam.Priority = 1;
            pathTwoCam.Priority = 1;
            pathThreeCam.Priority = 1;
            pathFourCam.Priority = 11;
            pathFiveCam.Priority = 1;
            pathSixCam.Priority = 1;
            pathSevenCam.Priority = 1;
            pathTwoCam.gameObject.SetActive(false);
            pathThreeCam.gameObject.SetActive(false);
            pathOneCam.gameObject.SetActive(false);
            pathFiveCam.gameObject.SetActive(false);
            pathSixCam.gameObject.SetActive(false);
            pathSevenCam.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == ("Path5"))
        {
            pathFiveCam.gameObject.SetActive(true);
            activeCam = pathFiveCam;
            pathOneCam.Priority = 1;
            pathTwoCam.Priority = 1;
            pathThreeCam.Priority = 1;
            pathFourCam.Priority = 1;
            pathFiveCam.Priority = 11;
            pathSixCam.Priority = 1;
            pathSevenCam.Priority = 1;
            pathTwoCam.gameObject.SetActive(false);
            pathThreeCam.gameObject.SetActive(false);
            pathFourCam.gameObject.SetActive(false);
            pathOneCam.gameObject.SetActive(false);
            pathSixCam.gameObject.SetActive(false);
            pathSevenCam.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == ("Path6"))
        {
            pathSixCam.gameObject.SetActive(true);
            activeCam = pathSixCam;
            pathOneCam.Priority = 1;
            pathTwoCam.Priority = 1;
            pathThreeCam.Priority = 1;
            pathFourCam.Priority = 1;
            pathFiveCam.Priority = 1;
            pathSixCam.Priority = 11;
            pathSevenCam.Priority = 1;
            pathTwoCam.gameObject.SetActive(false);
            pathThreeCam.gameObject.SetActive(false);
            pathFourCam.gameObject.SetActive(false);
            pathFiveCam.gameObject.SetActive(false);
            pathOneCam.gameObject.SetActive(false);
            pathSevenCam.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == ("Path7"))
        {
            pathSevenCam.gameObject.SetActive(true);
            activeCam = pathSevenCam;
            pathOneCam.Priority = 1;
            pathTwoCam.Priority = 1;
            pathThreeCam.Priority = 1;
            pathFourCam.Priority = 1;
            pathFiveCam.Priority = 1;
            pathSixCam.Priority = 1;
            pathSevenCam.Priority = 11;
            pathTwoCam.gameObject.SetActive(false);
            pathThreeCam.gameObject.SetActive(false);
            pathFourCam.gameObject.SetActive(false);
            pathFiveCam.gameObject.SetActive(false);
            pathSixCam.gameObject.SetActive(false);
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

        if(other.gameObject.CompareTag("Fireworks"))
        {
            if(!hit)
            {
                health -= 1;
            }
            hit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == ("Killbox"))
        {
            inKillbox = false;
        }
    }

    IEnumerator InvisiFrames()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while (temp < numberOfFlashes)
        {
            characterBody.SetActive(false);
            yield return new WaitForSeconds(flashDuration);
            characterBody.SetActive(true);
            yield return new WaitForSeconds(flashDuration);
            characterBody.SetActive(false);
            yield return new WaitForSeconds(flashDuration);
            characterBody.SetActive(true);
            yield return new WaitForSeconds(flashDuration);
            characterBody.SetActive(false);
            yield return new WaitForSeconds(flashDuration);
            characterBody.SetActive(true);
            temp++;
        }
        triggerCollider.enabled = true;
        hit = false;
    }

}
