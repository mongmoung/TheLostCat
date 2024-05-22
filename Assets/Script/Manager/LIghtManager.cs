using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LIghtManager : SingleTon<LIghtManager>
{

    protected Light2D lights;

    protected float maxBright = 1.0f;
    protected float currentBright;
    protected float minBright = 0.0f;
    protected float maxDark = 0.1f;

    public virtual void Start()
    {
        lights = GetComponent<Light2D>();
    }
    public float CurrentBright
    {
        get { return currentBright; }

        set
        {
            currentBright = value;
            if (currentBright > maxBright)
                lights.intensity = maxBright;
            if (currentBright < minBright)
                lights.intensity = minBright;
        }

    }

    public float MaxDark
    {
        get { return maxDark; }
        set
        {
            maxDark = value;
            if (minBright < maxDark)
                lights.intensity = maxDark;
        }
    }

}
