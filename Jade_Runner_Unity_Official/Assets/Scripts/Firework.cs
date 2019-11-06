using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public GameObject firework;

    public GameObject fuseEffect;

    public GameObject explosionEffect;

    public Transform explosionTarget;

    private PlayerLocomotion playerLocomotion;

    //public EnemyLocomotion enemyLocomotion;

    public float explosionRate;

    public float explosionMaxSize;

    //public float explosionSpeed;

    public float currentRadius;

    public bool detonated = false;

    public SphereCollider explosionRadius;

    // Start is called before the first frame update
    void Start()
    {
        fuseEffect.SetActive(false);
        playerLocomotion = GameObject.FindObjectOfType<PlayerLocomotion>();
    }

    private void FixedUpdate()
    {
        if (detonated == true)
        {
            if (currentRadius < explosionMaxSize)
            {
                currentRadius += explosionRate;
            }
            explosionRadius.radius = currentRadius;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine("Countdown");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fireworks"))
        {
            StartCoroutine("Combust");
        }
        //if (detonated)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        if(!playerLocomotion.dead)
        //        {
        //            playerLocomotion.hit = true;
        //            playerLocomotion.health -= 1;
        //            Debug.Log("Boom!");
        //        }
        //    }
        //}
    }

    IEnumerator Countdown()
    {
        fuseEffect.SetActive(true);

        yield return new WaitForSeconds(3f);

        detonated = true;
        Instantiate(explosionEffect, explosionTarget.position, explosionTarget.rotation);
        Destroy(firework);
    }

    IEnumerator Combust()
    {
        yield return new WaitForSeconds(0.5f);

        detonated = true;
        Instantiate(explosionEffect, explosionTarget.position, explosionTarget.rotation);
        Destroy(firework);
    }


    // Update is called once per frame
    //void Update()
    //{

    //}
}
