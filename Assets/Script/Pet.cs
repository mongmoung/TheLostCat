using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    PlayerController playercon;
    public Animator petanimator;
    Vector2 playerPos;

    bool isChekPlayer;
    int maxDistance = 200;
    Queue< Vector2> prevPosQueue = new Queue<Vector2>();


    void Update()
    {

        PetMove();
        if(isChekPlayer)
        {
            playerPos = new Vector2(playercon.transform.position.x, playercon.transform.position.y - 0.2f);
            prevPosQueue.Enqueue(playerPos);
            if (prevPosQueue.Count > maxDistance)
            {                
                gameObject.transform.position = prevPosQueue.Dequeue();
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);                  
                }
        
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

        }
  
    }

    void PetMove()
    {
        if (!isChekPlayer)
            return;

        if(playercon.IsWalk) 
        {
            petanimator.SetBool("PetIsMove", true);
            petanimator.SetFloat("PetMove", 0f);
            if (playercon.IsRun)
                petanimator.SetFloat("PetMove", 0.5f);
        }
        else
            petanimator.SetBool("PetIsMove", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playercon = collision.gameObject.GetComponent<PlayerController>();
        isChekPlayer = playercon != null;
        if (isChekPlayer)
        {
            gameObject.layer = 10;
        }
    }
}
