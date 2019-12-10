using System.Collections;
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
            if (this.gameObject.tag == "Fruit" && playerLocomotion.health < playerLocomotion.startHealth)
            {
                AkSoundEngine.PostEvent("fruitHealth", gameObject);
                playerLocomotion.health += 1;
                playerLocomotion.currentHealth = playerLocomotion.health;
                playerLocomotion.fruitMeter += 1;
            }
            if (this.gameObject.tag == "PowerFruit" && !playerLocomotion.poweredUp)
            {
                AkSoundEngine.PostEvent("fruitPowerUp", gameObject);
                AkSoundEngine.SetSwitch("PlayerAttack", "PoweredUp", gameObject);
                playerLocomotion.poweredUp = true;
                playerLocomotion.powerUpJig = true;
                playerLocomotion.moveSpeed = playerLocomotion.moveSpeed * 1.5f;
                playerLocomotion.jumpForce = playerLocomotion.jumpForce * 1.25f;
                //put ranged attack code here
            }
            Destroy(gameObject);
        }
    }
}
