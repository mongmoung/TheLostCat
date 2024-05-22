using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cover : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerSkill player = collision.gameObject.GetComponent<PlayerSkill>();
        if (player != null)
        {
            player.IsHide = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerSkill player = collision.gameObject.GetComponent<PlayerSkill>();
        if(player != null) 
        {
           player.IsHide = false;
        }
    }
}
