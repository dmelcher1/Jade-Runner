using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMove : MonoBehaviour
{
    private Vector3 startPos;
    Collider m_collider;
    public float moveUp;
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        m_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit(Collider other)
    {
        
            Debug.Log("Move and Collider Off");
            transform.Translate(0f, moveUp, 0f);
            m_collider.enabled = !m_collider.enabled;
    }
}
