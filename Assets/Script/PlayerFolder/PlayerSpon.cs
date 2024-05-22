using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpon : MonoBehaviour
{
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            player.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
        }
    }
}
