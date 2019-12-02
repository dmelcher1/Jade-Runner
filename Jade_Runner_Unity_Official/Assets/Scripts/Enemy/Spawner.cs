using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool forGorilla;
    public bool forChargeRhino;
    public bool forPatrolRhino;

    public GameObject chargeRhino;
    private GameObject patrolRhino;
    private GameObject gorilla;

    // Start is called before the first frame update
    void Start()
    {
        //chargeRhino = GameObject.Find("Charge_Rhino");
        if (forGorilla)
        {
            //gorilla = GameObject.Find() Insert name of gorilla object here
            //Instantiate Gorilla at this position and rotation
        }
        else if(forChargeRhino)
        {
            //Instantiate Charging Rhino at this position and rotation
            Instantiate(chargeRhino, new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z), transform.rotation);
        }
        else if(forPatrolRhino)
        {
            //Instantiate Patrolling Rhino at this position and rotation
            //Instantiate()
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
