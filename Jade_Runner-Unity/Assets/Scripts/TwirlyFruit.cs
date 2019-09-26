using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwirlyFruit : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.tag == "Fruit")
        {
            this.transform.Rotate(0, 1, 0, Space.World);
        }
    }
}
