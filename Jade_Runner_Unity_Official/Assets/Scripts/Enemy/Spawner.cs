using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool forGorilla;
    public bool forChargeRhino;
    public bool forPatrolRhino;

    // Start is called before the first frame update
    void Start()
    {
        if(forGorilla)
        {
            //Instantiate Gorilla at this position and rotation
        }
        else if(forChargeRhino)
        {
            //Instantiate Charging Rhino at this position and rotation
        }
        else if(forPatrolRhino)
        {
            //Instantiate Patrolling Rhino at this position and rotation
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
