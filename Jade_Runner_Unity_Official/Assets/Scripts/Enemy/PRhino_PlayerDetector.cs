using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRhino_PlayerDetector : MonoBehaviour
{
    public bool playerDetected;
    private SphereCollider thisCollider;
    public GameObject patrolRhino;
    private P_Rhino patrolRhinoScript;

    private void Start()
    {
        patrolRhinoScript = patrolRhino.GetComponent<P_Rhino>();
        thisCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerDetected = true;
            //patrolRhinoScript.charge = true;
            //thisCollider.enabled = false;
        }
    }
}
