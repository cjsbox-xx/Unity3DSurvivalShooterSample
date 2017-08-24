using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEngineType
{
    None,
    Fmod
}
public abstract class SoundEngine
{
    public abstract void Init();
    public abstract BaseSound CreateSound();
    public abstract void DestroySound(BaseSound sound);
    public abstract SoundEngineType GetEngineType();
    public abstract void SetParamValue(string paramName, float value);
    public abstract float GetParamValue(string paramName);
}
