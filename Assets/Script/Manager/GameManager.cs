using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject playerObj;
    public PlayerController playerController;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        playerObj = GameObject.Find("Player").gameObject;
        playerController = playerObj.GetComponent<PlayerController>();
    }
}
