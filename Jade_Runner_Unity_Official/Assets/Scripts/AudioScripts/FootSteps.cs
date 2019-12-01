using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    uint bankID;

    public void LoadBank()
    {
        AkSoundEngine.LoadBank("JadeRunner_Soundbank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
    }
    // Update is called once per frame
    private void Step()
    {
        AkSoundEngine.PostEvent("UI_hover", gameObject);

    }
}
