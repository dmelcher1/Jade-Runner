using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementAudio : MonoBehaviour
{
    private void Step()
    {
        AkSoundEngine.PostEvent("soldierStep", gameObject);
    }

    private void Swat()
    {
        AkSoundEngine.PostEvent("soldierSpear", gameObject);
    }

    private void Charge()
    {
        AkSoundEngine.PostEvent("soldierCharge", gameObject);
    }

}
