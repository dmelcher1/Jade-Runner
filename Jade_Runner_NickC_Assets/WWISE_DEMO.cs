using System.Collections;
using UnityEngine;
using System;

public class WWISE_DEMO : MonoBehaviour
{
    uint bankID;
    //System.IntPtr pointer;
    public void LoadBank()
    {
        AkSoundEngine.LoadBank(“mx_main”, AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
    }
    //public void UnloadBank()
    //{
    //    AkSoundEngine.UnloadBank(“mx_main”, pointer);
    //}


    public void PostEvent()
    {
        AkSoundEngine.PostEvent(“mx_gp_synthwave”, gameObject);
    }

    public void AudioRendering()
    {
        AkSoundEngine.RenderAudio();
    }

    public void SetRTPCValue(float value)
    {
        AkSoundEngine.SetRTPCValue(“NameofRTPC”, value);
    }

    public void ChangeSwitch()
    {
        AkSoundEngine.SetSwitch(“Footsteps”, “Footsteps_Grass”, gameObject);
    }

    public void ChangeState()
    {
        AkSoundEngine.SetState(“NameOfStateContainer”, “NameOfState”);
    }

    public void PauseMX(string eventName, float fadeOut)
    {
        uint eventID;
        eventID = AkSoundEngine.GetIDFromString(eventName);

        AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Pause, gameObject, (int)(fadeOut * 1000), AkCurveInterpolation.AkCurveInterpolation_Sine);
    }
    public void StopMX(string eventName, float fadeOut)
    {
        uint eventID;
        eventID = AkSoundEngine.GetIDFromString(eventName);
        AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Stop, gameObject, (int)(fadeOut * 1000), AkCurveInterpolation.AkCurveInterpolation_Sine);
    }
    public void Resume(string eventName, float fadeIn)
    {
        uint eventID = AkSoundEngine.GetIDFromString(eventName);
        AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Resume, gameObject, (int)(fadeIn * 1000), AkCurveInterpolation.AkCurveInterpolation_Sine);
    }

    //https://www.audiokinetic.com/library/edge/?source=Unity&id=class_ak_event.html
}