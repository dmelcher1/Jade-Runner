﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeDestroy : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DestroySelf");
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
