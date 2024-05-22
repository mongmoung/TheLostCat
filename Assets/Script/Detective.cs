using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Detective : MonoBehaviour
{
    public LayerMask targetLayerMask;
    float radius = 4.5f;
    bool checkMask;

    public Vector2 TargetLastPos
    {
        get;
        private set;
    }

    public bool CheckMask
    {
        get { return checkMask; }
        private set { checkMask = value; }
    }

    private void FixedUpdate()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, radius, targetLayerMask);
        CheckMask = target != null;

        if (CheckMask)
        {
            TargetLastPos = target.transform.position;
        }
        else if (target == null)
        {
            CheckMask = false;
        }
    }

}