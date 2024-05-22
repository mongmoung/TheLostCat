using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PlayerLight : LIghtManager
{
    public override void Start()
    {
        base.Start();
        CurrentBright = 0.8f;
    }

    private void Update()
    {
        lights.intensity = CurrentBright;
        
    }

}

