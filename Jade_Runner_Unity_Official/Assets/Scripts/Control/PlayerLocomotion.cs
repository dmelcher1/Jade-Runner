using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    //private GameController gameController;

    [SerializeField]
    private float playerMagnitude;

    public float stickDeadZone = 0.25f;
    public float moveSpeed;
    private float moveSpeedConstant;
    //public float lungeSpeed;
    [SerializeField]
    private float currentSpeed;
    private float verticalSpeed;
    [SerializeField]
    private float stepTimer;
    public float lookSpeed = 5f;
    private float turnAngle;
    private Quaternion targetRotation;
    public GameObject[] handFires;
    public GameObject[] tigerClaws;
    private BoxCollider clawRight;
    private BoxCollider clawLeft;

    //IMPORTANT FOR ROTATION
    //private float playerRot = 80.0f;
    //private float rotDeg = 120f;
    //private float horizontal = 0.0f;
    //private float vertical = 0.0f;

    [SerializeField]
    private Vector2 playerInput;

    [SerializeField]
    private int temp = 0;

    private string jumpControl;
    private string attackControl;

    private string jumpKeyboard = "Jump";
    private string jumpJoystick = "Jump2";

    private string attackKeyboard = "Attack";
    private string attackTrigger = "Attack2";

    
    public int fruitCount;
    
    public int fruitMeter;

    [SerializeField]
    private float lastLeg = 0;
    public bool airBorne = false;
    public bool dblJump = false;
    public bool inKillbox = false;
    public bool canJump = true;
    public bool keyboardActive;
    public bool controllerActive;
    public bool singleAttack = false;
    public bool multiAttack = false;
    public bool attackActive = false;
    public bool poweredUp = false;
    public bool powerUpJig = false;
    public bool haltAttack = false;
    
    //public bool canAttack = true;
    [SerializeField]
    private float killTimer = 3.0f;
    [SerializeField]
    private float jumpTimer;
    [SerializeField]
    private float dblJumpTimer;
    [SerializeField]
    private int jumpCount;
    public float jumpForce;
    private float jumpForceConstant;
    public float dblJumpForce;

    [SerializeField]
    private float poweredUpInstanceTimer = 1.0f;
    [SerializeField]
    private float poweredUpDurationTimer = 10.0f;
    private float poweredUpTimerReset;
    private float poweredUpInstanceTimerReset;

    [SerializeField]
    private float multiAttackTimer;
    [SerializeField]
    private float singleAttackTimer;
    //[SerializeField]
    //private int attackCount;

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
    public int startHealth;
    public int currentHealth;

    public bool hit = false;
    public bool attackedByEnemy = false;
   
    public bool dead;
    private float die;

    public GameObject currentCheckpoint;

    //public Color flashColor;
    //public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public GameObject characterBody;

    private Animator playerAnim;

    //public Animation runLoop;

    public bool noInput;

    //private float hitLayerWeight;

    //public GameObject recentCheckpoint;

    //public GameObject[] checkpointList;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = health;
        this.transform.position = currentCheckpoint.transform.position;
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        clawRight = tigerClaws[0].GetComponent<BoxCollider>();
        clawLeft = tigerClaws[1].GetComponent<BoxCollider>();
        poweredUpTimerReset = poweredUpDurationTimer;
        poweredUpInstanceTimerReset = poweredUpInstanceTimer;
        moveSpeedConstant = moveSpeed;
        jumpForceConstant = jumpForce;
        currentHealth = health;
        activeCam = pathOneCam;
        stepTimer = 0.03f;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {

        //if(!noInput) <- will be used to decide if player is moving or not for idle animation
        //{ }
        //singleAttack = true;
        
        //KEEP JUMP HERE!
        JumpControls();
        AttackControls();
        KillBox();
        DamageDeath();
        //FruitCollection();
        PoweredUpState();

        if(!dead && !powerUpJig)
        {
            if (keyboardActive)
            {
                currentSpeed = new Vector2(playerInput.x, playerInput.y).sqrMagnitude;
                
                playerAnim.SetFloat("Speed", currentSpeed);

                //Debug.Log("Can Move?");
               
                playerInput.x = Input.GetAxisRaw("Horizontal");
                
                playerInput.y = Input.GetAxisRaw("Vertical");
            }
            else if (controllerActive)
            {
                currentSpeed = new Vector2(playerInput.x, playerInput.y).sqrMagnitude;
                
                playerAnim.SetFloat("Speed", currentSpeed);

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

            CalcDirect();
            PlayerRotate();
            PlayerMove();
        }
        
        
        
        
        playerAnim.SetBool("Hit", hit);
        
        playerAnim.SetBool("Dead", dead);

        //playerAnim.SetFloat("PowerTimer", poweredUpInstanceTimer);

        


        playerMagnitude = playerInput.magnitude;

        //Debug.Log("Anything?");
        //Debug.Log(poweredUpInstanceTimer);
    }

    void JumpControls()
    {
        if(!dead && !powerUpJig)
        {
            //Debug.Log("Jumping");
            if (keyboardActive && !controllerActive)
            {
                jumpControl = jumpKeyboard;
            }
            else if (controllerActive && !keyboardActive)
            {
                jumpControl = jumpJoystick;
            }

            jumpTimer -= 0.1f * Time.deltaTime;

            if (airBorne && dblJump)
            {
                dblJumpTimer -= 0.1f * Time.deltaTime;
            }

            if (Input.GetButtonUp(jumpControl) && airBorne && jumpTimer <= 0.3f && jumpCount < 1)
            {

                dblJump = true;
                jumpCount += 1;
                
            }

            if (Input.GetButtonDown(jumpControl))
            {
                //Debug.Log("Jumping");
                if (!airBorne && jumpTimer <= 0)
                {
                    jumpTimer = 0.1f;
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    AkSoundEngine.PostEvent("playerJump", gameObject);
                    canJump = false;
                    airBorne = true;
                }
            }
            if (Input.GetButtonDown(jumpControl) && dblJumpTimer < 0)
            {
                rb.AddForce(Vector3.up * dblJumpForce, ForceMode.Impulse);
                playerAnim.SetTrigger("DblJump");
               // AkSoundEngine.PostEvent("playerJump2", gameObject);
                dblJump = false;
                dblJumpTimer = 0;
            }

            if (Input.GetButtonUp(jumpControl))
            {
                canJump = true;
            }

            playerAnim.SetBool("Airborne", airBorne);
            playerAnim.SetBool("DoubleJump", dblJump);
            playerAnim.SetFloat("VerticalSpeed", rb.velocity.y);
            playerAnim.SetFloat("JumpTime", jumpTimer);
        }
    }

    void AttackControls()
    {
        //multiAttack = true;
        //Debug.Log(multiAttack);
        if (!dead && !powerUpJig)
        {
            if (keyboardActive && !controllerActive)
            {
                attackControl = attackKeyboard;
            }
            else if (controllerActive && !keyboardActive)
            {
                attackControl = attackTrigger;
            }

            multiAttackTimer -= 0.1f * Time.deltaTime;
            singleAttackTimer -= 0.1f * Time.deltaTime;
            

            if (!airBorne)
            {
                if (Input.GetButtonDown(attackControl) && !singleAttack)
                {
                    if (multiAttackTimer <= 0)
                    {
                        
                        //canAttack = false;
                        if (singleAttackTimer < -0.09f)
                        {
                            singleAttack = true;
                            //Debug.Log(singleAttack);
                            singleAttackTimer = 0.0f;
                            if(singleAttack)
                            {
                                multiAttackTimer = 0.1f;
                            }
                        }
                        
                    }
                    haltAttack = false;
                }

                if (Input.GetButton(attackControl))
                {
                    //multiAttack = true;
                    if (multiAttackTimer <= 0.07)
                    {
                        //Debug.Log("Multi?");
                        singleAttack = false;
                        multiAttack = true;
                    }
                }

                if (Input.GetButtonUp(attackControl))
                {
                    singleAttack = false;
                    multiAttack = false;
                }
            }
            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack-Single") ||
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack-ContinueCombo") || 
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack-ExitCombo"))
            {
                clawRight.enabled = true;
            }
            else
            {
                clawRight.enabled = false;
            }
       
            if(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack-Combo"))
            {
                clawLeft.enabled = true;
            }
            else
            {
                clawLeft.enabled = false;
            }

            if(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack-Single"))
            {
                attackActive = true;
            }
            else
            {
                attackActive = false;
            }
            if (multiAttack && (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack-ContinueCombo") ||
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack-ExitCombo") || 
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack-Combo")))
            {
                stepTimer -= 0.1f * Time.deltaTime;
                if(stepTimer < -0.03)
                {
                    stepTimer = 0.03f;
                }
            }
            if((playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Running - Start") || 
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle - Healthy - Loop")) && !singleAttack)
            {
                //haltAttack = true;
            }
            if(haltAttack)
            {
                multiAttack = false;
            }
            
            if(!multiAttack)
            {
                stepTimer = 0.03f;
            }

            playerAnim.SetBool("MultiAttack", multiAttack);
            playerAnim.SetBool("SingleAttack", singleAttack);
            
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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, turnAngle, 0)), Time.deltaTime * lookSpeed);
    }

    void PlayerMove()
    {
        //transform.Translate(0, 0, vertical);
        if(!attackActive && stepTimer > 0)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        
        //if (attackActive)
        //{
             
        //    transform.position += 
        //}

        //if(!multiAttack)
        //{
        //    transform.position += transform.forward * lungeSpeed * Time.deltaTime;
        //}

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
            hit = false;
            fadeDelay -= 0.1f;
        }
        if (hit & !dead && health >= 1)
        {
            StartCoroutine("InvisiFrames");
            hit = false;
        }
        if(health == 1 && lastLeg < 10.0f)
        {
            lastLeg += 0.1f;
        }
        else if(health > 1)
        {
            lastLeg = 0;
        }
        if (dead && die < 10.0f)
        {
            die += 0.1f;
        }
        else if(!dead)
        {
            die = 0;
        }
        playerAnim.SetFloat("Dying", lastLeg);
        playerAnim.SetFloat("DyingCrouch", die);
        playerAnim.SetBool("Dead", dead);
    }

    //void FruitCollection()
    //{
    //    if(fruitMeter == 10)
    //    {
    //        //health += 1;
    //        currentHealth = health;
    //        fruitMeter = 0;
    //    }
    //}

    void PoweredUpState()
    {
        if(poweredUp && poweredUpDurationTimer > 0)
        {
            //powerUpJig = true;
            poweredUpDurationTimer -= 1.0f * Time.deltaTime;
            poweredUpInstanceTimer -= 1.0f * Time.deltaTime;
            //if (poweredUpInstanceTimer < 0.01)
            //{
            //    poweredUpInstanceTimer = 0;
            //}
            //Debug.Log("Still got juice!");
            foreach (GameObject fire in handFires)
            {
                fire.SetActive(true);
            }
        }
        else if(poweredUpDurationTimer < 0)
        {
            foreach (GameObject fire in handFires)
            {
                fire.SetActive(false);
            }
            poweredUp = false;
            moveSpeed = moveSpeedConstant;
            jumpForce = jumpForceConstant;
            poweredUpDurationTimer = poweredUpTimerReset;
            AkSoundEngine.SetSwitch("PlayerAttacks", "Normal", gameObject);
            Debug.Log("PlayerAttack to normal switch");
        }
        if(poweredUpInstanceTimer < 0)
        {
            powerUpJig = false;
            if(!poweredUp)
            poweredUpInstanceTimer = poweredUpInstanceTimerReset;
        }
        playerAnim.SetBool("PoweredUp", poweredUp);
        playerAnim.SetBool("PowerDance", powerUpJig);

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("landed!");
            if(canJump)
            {
                airBorne = false;
                playerAnim.ResetTrigger("DblJump");
                dblJump = false;
                dblJumpTimer = 0;
                jumpCount = 0;
            }
        }
        //if(collision.gameObject.CompareTag("Debug"))
        //{
        //    Debug.Log("What the fuck?");
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Checkpoint"))
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
            if (pathFourCam != null)
            {
                pathFourCam.Priority = 1;
                pathFiveCam.Priority = 1;
                pathSixCam.Priority = 1;
                pathSevenCam.Priority = 1;
            }
            pathTwoCam.gameObject.SetActive(false);
            pathThreeCam.gameObject.SetActive(false);
            if (pathFourCam != null)
            {
                pathFourCam.gameObject.SetActive(false);
                pathFiveCam.gameObject.SetActive(false);
                pathSixCam.gameObject.SetActive(false);
                pathSevenCam.gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.tag == ("Path2"))
        {
            pathTwoCam.gameObject.SetActive(true);
            activeCam = pathTwoCam;
            pathOneCam.Priority = 1;
            pathTwoCam.Priority = 11;
            pathThreeCam.Priority = 1;
            if (pathFourCam != null)
            {
                pathFourCam.Priority = 1;
                pathFiveCam.Priority = 1;
                pathSixCam.Priority = 1;
                pathSevenCam.Priority = 1;
            }
            pathOneCam.gameObject.SetActive(false);
            pathThreeCam.gameObject.SetActive(false);
            if (pathFourCam != null)
            {
                pathFourCam.gameObject.SetActive(false);
                pathFiveCam.gameObject.SetActive(false);
                pathSixCam.gameObject.SetActive(false);
                pathSevenCam.gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.tag == ("Path3"))
        {
            pathThreeCam.gameObject.SetActive(true);
            activeCam = pathThreeCam;
            pathOneCam.Priority = 1;
            pathTwoCam.Priority = 1;
            pathThreeCam.Priority = 11;
            if (pathFourCam != null)
            {
                pathFourCam.Priority = 1;
                pathFiveCam.Priority = 1;
                pathSixCam.Priority = 1;
                pathSevenCam.Priority = 1;
            }
            pathTwoCam.gameObject.SetActive(false);
            pathOneCam.gameObject.SetActive(false);
            if (pathFourCam != null)
            {
                pathFourCam.gameObject.SetActive(false);
                pathFiveCam.gameObject.SetActive(false);
                pathSixCam.gameObject.SetActive(false);
                pathSevenCam.gameObject.SetActive(false);
            }
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
        //if(other.gameObject.tag == "PowerFruit")
        //{
        //    poweredUp = true;
        //    poweredUpInstanceTimer = 0.1f;
        //    moveSpeed = moveSpeed * 1.5f;
        //    jumpForce = jumpForce * 1.25f;
        //    //put ranged attack code here
        //}

        //if (other.gameObject.tag == "Fruit")
        //{
        //    Debug.Log("That's Not A Wampa!");
        //    fruitCount += 1;
        //    if(health < 3)
        //    {
        //        fruitMeter += 1;
        //    }
        //    Destroy(other.gameObject);
        //}
        if (other.gameObject.tag == "Killbox")
        {
            inKillbox = true;
        }

        if (other.gameObject.CompareTag("Fireworks"))
        {
            if (!hit)
            {
                Debug.Log("Ouch");
                health -= 1;
            }
            hit = true;
        }

        if(attackedByEnemy)
        {
            Debug.Log("Arrg!");
            health -= 1;
            hit = true;
            attackedByEnemy = false;
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
        //Debug.Log("Zapped");
        temp = 0;
        triggerCollider.enabled = false;
        while (temp < numberOfFlashes)
        {
            characterBody.SetActive(false);
            yield return new WaitForSeconds(flashDuration);
            characterBody.SetActive(true);
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        if(temp >= numberOfFlashes)
        {
            currentHealth = health;
            triggerCollider.enabled = true;
            //hit = false;
        }
    }

}
