using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSkill : MonoBehaviour
{
    PlayerController playerController;
    PlayerLight playerLight;
    SpriteRenderer playerRenderer;
    Animator animator;
    Color playerColor;
    CapsuleCollider2D capsuleCollider;
    Rigidbody2D rb;
    MonsterMove monster;
    public AudioClip BadClip;
    public AudioClip hideClip;

    float changeColor = 0.05f;
    float setTime = 10.0f;
    bool isHide;
    bool stopDark;
    bool stopBright;
    public bool IsHide
    {
        get { return isHide; }
        set { isHide = value; }
    }

    private void Start()
    {
        playerLight = GetComponentInChildren<PlayerLight>();
        playerRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerColor = GetComponent<SpriteRenderer>().material.color;
        playerController = GetComponent<PlayerController>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Hide();
    }
    void Hide()
    {
        if (isHide)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SoundManager.instance.SFXPlay("Hide", hideClip);
                stopDark = false;
                StopCoroutine("BrightPlayer");
                stopBright = true;
                playerController.IsMove = false;
                StartCoroutine(DarkPlayer());
                gameObject.layer = 8;
                playerLight.CurrentBright = 0.1f;
                animator.SetTrigger("HideTrigger");

            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                stopBright = false;
                StopCoroutine("DarkPlayer");
                stopDark = true;
                playerController.IsMove = true;
                StartCoroutine(BrightPlayer());
                gameObject.layer = 6;
                playerLight.CurrentBright = 0.6f;
                animator.SetTrigger("UnHideTrigger");
            }
        }
    }

    private IEnumerator DarkPlayer()
    {
        while (playerColor.a >= 0.05f && stopDark == false)
        {
            yield return new WaitForSeconds(0.1f);
            playerColor.a -= changeColor;
            GetComponent<SpriteRenderer>().color = playerColor;
            if (stopDark == true)
                break;
        }
    }
    private IEnumerator BrightPlayer()
    {
        while (playerColor.a <= 1.0f && stopBright == false)
        {
            yield return new WaitForSeconds(0.1f);
            playerColor.a += changeColor;
            GetComponent<SpriteRenderer>().color = playerColor;
            yield return null;
            if(stopBright == true)
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        MonsterMove monster = collision.gameObject.GetComponent<MonsterMove>();
        bool isCheckMonster = monster != null;


        if (isCheckMonster)
        {
            
            setTime -= Time.deltaTime;
            playerController.IsMove = false;
            if (setTime > 0 && Input.GetKeyDown(KeyCode.LeftControl))
            {
                StartCoroutine(LayerCo());
                playerController.IsMove = true;
            }
            else if (setTime < 0 )
            {
                StartCoroutine(BadCo());
            }
            IEnumerator LayerCo()
            {
                gameObject.layer = 8;
                Debug.Log(gameObject.layer);
                yield return new WaitForSeconds(3);
                gameObject.layer = 6;
                Debug.Log(gameObject.layer);
                setTime = 1.5f;
            }

            IEnumerator BadCo()
            {
                yield return new WaitForSeconds(1);
                SoundManager.instance.SFXPlay("Bad", BadClip);
                Destroy(this.gameObject);
                SceneManager.LoadScene("BadEndding");
                Time.timeScale = 0.0f;

            }

        }

    }

   
}
