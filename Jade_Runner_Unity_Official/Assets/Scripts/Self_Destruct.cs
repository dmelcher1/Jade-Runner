using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_Destruct : MonoBehaviour
{
    SphereCollider thisCollider;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
