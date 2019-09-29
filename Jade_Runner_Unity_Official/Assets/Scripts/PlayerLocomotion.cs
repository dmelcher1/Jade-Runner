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

    public Transform activeCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

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
        turnAngle += activeCam.eulerAngles.y;
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

}
