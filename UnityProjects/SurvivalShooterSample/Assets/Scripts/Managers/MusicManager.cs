using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [FMODUnity.EventRef] public string MusicEvent;

    private FMOD.Studio.EventInstance musicEvent;
    void Awake()
    {
        musicEvent = FMODUnity.RuntimeManager.CreateInstance(MusicEvent);
        musicEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
        musicEvent.start();
    }

    void OnDestroy()
    {
        musicEvent.stop(STOP_MODE.ALLOWFADEOUT);
    }
}
