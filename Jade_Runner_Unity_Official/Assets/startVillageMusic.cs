using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startVillageMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("villageStart", gameObject);
    }
}
