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
        if (this.gameObject.tag == "Fruit")
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
            if (playerLocomotion.health < 3)
            {
                playerLocomotion.fruitMeter += 1;
            }
            Destroy(gameObject);
        }
    }
}
