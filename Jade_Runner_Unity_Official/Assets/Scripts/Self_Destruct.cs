using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_Destruct : MonoBehaviour
{
    SphereCollider thisCollider;
    private PlayerLocomotion playerLocomotion;

    // Start is called before the first frame update
    void Start()
    {
        playerLocomotion = GameObject.FindObjectOfType<PlayerLocomotion>();
        thisCollider = GetComponent<SphereCollider>();
        StartCoroutine("DisableCollider");
        StartCoroutine("DestroySelf");
    }

    IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(1.0f);
        thisCollider.enabled = false;
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3.0f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerLocomotion.dead && !playerLocomotion.hit)
            {
                playerLocomotion.health -= 1;
                playerLocomotion.hit = true;
                Debug.Log("Boom!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
