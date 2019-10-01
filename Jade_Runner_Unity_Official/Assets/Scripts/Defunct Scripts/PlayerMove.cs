using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController charController;

    public float moveSpeed = 400;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 playerInput;
    private Vector3 playerMove;

    private float horizontal;
    private float vertical;

    private float jumpTimer;
    public float jumpForce;
    public float gravity = 20.0f;
    public bool airBorne;

    [SerializeField]
    private int fruitCount;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpTimer -= 0.1f;

        //if(!noInput) <- will be used to decide if player is moving or not for idle animation
        //{ }

        PlayerMoves();
    }

    void PlayerMoves()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        if (Input.GetButton("Jump") && !airBorne && jumpTimer <= 0)
        {
            jumpTimer = 3.0f;
            moveDirection.y = jumpForce;
            //rb.AddForce(Vector3.up * jumpForce);

            airBorne = true;
        }

        if (!airBorne)
        {
            playerInput = new Vector3(horizontal, 0f, vertical);
            playerInput = transform.TransformDirection(playerInput);
            //playerMove = Vector3.ClampMagnitude(playerInput, 1.0f) * moveSpeed;
            
        }

        moveDirection.y -= gravity * Time.deltaTime;
        charController.Move(playerInput * Time.deltaTime);

        //playerMove = Vector3.ClampMagnitude(playerInput, 1.0f) * moveSpeed;
        //transform.Translate(playerMove, Space.Self);
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
