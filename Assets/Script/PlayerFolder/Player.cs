using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Player : SingleTon<Player>
{
    [SerializeField]
    private float stamina;

    private float maxStamina = 100;
    private float minStamina = 0;
    private Item curItem;

    public Image Image;
    public Inventory Inventory;
    public GameObject invenObj;

    public CapsuleCollider2D capsule;
    public PlayerController playerController;
    PlayerLight playerLight;
    private void Start()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        playerController = GetComponent<PlayerController>();
        playerLight = GetComponentInChildren<PlayerLight>();
    }


    public float Stamina
    {
        get { return stamina; }
        set
        {
            stamina = value;

            if (stamina >= maxStamina)
                stamina = maxStamina;
            if (stamina <= minStamina)
                stamina = minStamina;

            if (playerController.IsMove)
            {
                if (stamina <= 30)
                {
                    playerController.MaxSpeed = 3f;
                    playerLight.CurrentBright = 0.3f;
                }
                if (stamina > 30)
                {
                    playerController.MaxSpeed = 5f;
                    playerLight.CurrentBright = 0.9f;
                }
            }
        }
    }



    private void Update()
    {
        if (playerController.IsRun)
        {
            Stamina -= 10f * Time.deltaTime;
        }
        if (playerController.IsWalk)
        {
            Stamina += 3.5f * Time.deltaTime;
        }
        else if (!playerController.IsWalk)
        {
            Stamina += 10f * Time.deltaTime;
        }

        Image.fillAmount = Stamina / 100;

        if (curItem != null)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                Inventory.setItem(curItem.GetComponent<Item>());
                curItem.gameObject.SetActive(false);
                curItem.transform.SetParent(invenObj.transform);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>() != null)
        {
            curItem = collision.GetComponent<Item>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>() != null)
        {
            curItem = null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

}