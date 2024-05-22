using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item 
{
    float speedBuff = 0.5f;
    public override void Use(Player target)
    {
        target.capsule.size = new Vector2(0.6f, 0.6f);
        target.playerController.NormalSpeed += speedBuff;
    }

}
