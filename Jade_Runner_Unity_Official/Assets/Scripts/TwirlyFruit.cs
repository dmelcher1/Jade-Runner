﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwirlyFruit : MonoBehaviour
{
    private PlayerLocomotion playerLocomotion;

    void Start()
    {
        playerLocomotion = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "Fruit" || this.gameObject.tag == "PowerFruit")
        {
            this.transform.Rotate(0, 1, 0, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("That's Not A Wampa!");
            playerLocomotion.fruitCount += 1;
            if (this.gameObject.tag == "Fruit" && playerLocomotion.health < 3)
            {
                playerLocomotion.fruitMeter += 1;
            }
            if (this.gameObject.tag == "PowerFruit" && other.gameObject.tag == "Player")
            {
                playerLocomotion.poweredUp = true;
                playerLocomotion.poweredUpInstanceTimer = 0.1f;
                playerLocomotion.moveSpeed = playerLocomotion.moveSpeed * 1.5f;
                playerLocomotion.jumpForce = playerLocomotion.jumpForce * 1.25f;
                //put ranged attack code here
            }
            Destroy(gameObject);
        }
    }
}
