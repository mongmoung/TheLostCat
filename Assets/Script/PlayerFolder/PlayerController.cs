using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour, IRunable, IWalkable
{
    Rigidbody2D rb2D;
    Animator animator;
    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip jumpClip;
    public AudioClip Runclip;

    float speed;
    float groundCheckDistance = 0.8f;
    float normalSpeed = 1.5f;
    float maxSpeed = 5f;
    float minSpeed = 0.7f;
    float jumoPower = 5;

    bool isWalk;
    bool isRun;
    bool isMove = true;
    bool isJump = true;

    public bool IsMove
    {
        get { return isMove; }
        set { isMove = value; }
    }

    public float MaxSpeed
    {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }
    public float NormalSpeed
    {
        get { return normalSpeed; }
        set { normalSpeed = value; }
    }

    public float Speed
    {
        get { return speed; }

        set
        {
            speed = value;
            if (speed >= MaxSpeed)
                speed = MaxSpeed;

            if (speed <= minSpeed)
                speed = minSpeed;
        }
    }
    public bool IsWalk
    {
        get { return isWalk; }
        set
        {
            isWalk = value;
            animator.SetBool("IsMove", isWalk);

        }
    }

    public bool IsRun
    {
        get { return isRun; }
        set
        {
            isRun = value;

            if (isRun)
            {
                Speed = MaxSpeed;
                animator.SetFloat("Move", 0.5f);
            }
            else
            {
                Speed = NormalSpeed;
                isRun = false;
                animator.SetFloat("Move", 0);
            }
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }


    public bool Run()
    {
        IsRun = Input.GetKey(KeyCode.Space);
        return IsRun;
    }
    public void Walk()
    {
        if (IsMove)
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
            if(IsWalk == false)
            {
                IsWalk = true;        
            }
            Run();

        }
       
    }

    public void WalkSound()
    {
        SoundManager.instance.SFXPlay("sound", clip);
    }

    public void RunSound()
    {
        SoundManager.instance.SFXPlay("sound", Runclip);
    }

    public void Jump()
    {
        if (isJump)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                rb2D.AddRelativeForce(Vector2.up * jumoPower, ForceMode2D.Impulse);
                StartCoroutine(JumpSound());
            }
                   
        }

        IEnumerator JumpSound()
        {
            yield return new WaitForSeconds(1);
            SoundManager.instance.SFXPlay("Jump", jumpClip);
            yield return null;
        }
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
        
            transform.rotation = Quaternion.Euler(0, 180, 0);
            Walk();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
          
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Walk();
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
           
            IsWalk = false;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance);
        if (hit.collider.IsTouchingLayers(1 << 8) || hit.collider.IsTouchingLayers(1 << 10))
            isJump = true;
        else
            isJump = false;
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.blue);
        Jump();
    }

    void Update()
    {
        Move();

    }
}
