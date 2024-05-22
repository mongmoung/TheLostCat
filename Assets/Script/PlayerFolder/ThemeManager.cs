using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public GameObject target;
    public GameObject monster;
    Player player;
    
    private void Start()
    {
        player = target.GetComponent<Player>();
    }

    private void Update()
    {
        if (player.transform.position.x >= 16 && player.transform.position.x <= 16.5f && player.gameObject.layer == 6)
        {
            player.transform.position = new Vector2(21.5f, player.transform.position.y);
            StartCoroutine(ChangeMonsterPos());
            CameraManager.instance.cameraXYSetting(2,1);
            StartCoroutine(ChangeLayer());
        }
        if(player.transform.position.x >= 20 && player.transform.position.x <= 21 && player.gameObject.layer == 6)
        {
            player.transform.position = new Vector2(15.8f, player.transform.position.y);
            CameraManager.instance.cameraXYSetting(1,1);
            StartCoroutine(ChangeLayer());
        }
        if (player.transform.position.x >= 84.3f && player.transform.position.x <= 85.5f && player.gameObject.layer == 6)
        {
            player.transform.position = new Vector2(91.5f, player.transform.position.y);
            CameraManager.instance.cameraXYSetting(3,1);
            StartCoroutine(ChangeLayer());
        }
        if (player.transform.position.x >= 90 && player.transform.position.x <= 91.4f && player.gameObject.layer == 6)
        {
            player.transform.position = new Vector2(84.2f, player.transform.position.y);
            CameraManager.instance.cameraXYSetting(2, 1);
            StartCoroutine(ChangeLayer());
        }
    }
    IEnumerator ChangeLayer()
    {
        player.gameObject.layer = 8;
        yield return new WaitForSeconds(1.5f);
        player.gameObject.layer = 6;
    }
    IEnumerator ChangeMonsterPos()
    {
        yield return new WaitForSeconds(5f);
    }    
}
