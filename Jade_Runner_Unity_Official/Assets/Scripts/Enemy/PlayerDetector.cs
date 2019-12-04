using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public bool playerDetected;
    private BoxCollider thisCollider;
    public GameObject chargeRhino;
    private C_Rhino chargeRhinoScript;

    private void Start()
    {
        chargeRhinoScript = chargeRhino.GetComponent<C_Rhino>();
        thisCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerDetected = true;
            chargeRhinoScript.charge = true;
            thisCollider.enabled = false;
        }
    }
}
