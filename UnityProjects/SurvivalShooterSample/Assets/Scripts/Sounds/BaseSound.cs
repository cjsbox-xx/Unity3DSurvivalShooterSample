using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSound
{
    public string Identifier;
    public abstract void Play();
    public abstract void Stop();
    public abstract void Pause();
    public abstract void Update(float time);
    public abstract void SetParamValue(string paramName, float value);
    public abstract float GetParamValue(string paramName);
}
