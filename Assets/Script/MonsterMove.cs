using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MonsterMove : MonoBehaviour
{
    Rigidbody2D rb;
    Detective detcetive;
    Animator animator;

    Player player;
    float monsterSpeed;
    int randomMove;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        detcetive = GetComponent<Detective>();
        animator = GetComponent<Animator>();
        Invoke("Move", 3); //Invoke �����ð� �� �Լ��� ȣ���ϴ� Ű����
    }

    void Update()
    {

        if (rb.velocity.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (rb.velocity.x > 0)
            GetComponent<SpriteRenderer>().flipX = false;


        if (detcetive.CheckMask == false)
        {
            monsterSpeed = 1.9f;
            rb.velocity = new Vector2(randomMove * monsterSpeed, rb.velocity.y); //���� ��ȸ
            if (randomMove == 0)
                animator.SetBool("IsMove", false);
            else
                animator.SetBool("IsMove", true);
        }

        else if (detcetive.CheckMask)
        {
            monsterSpeed = 4.0f;
            transform.right = detcetive.TargetLastPos;
            transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y,0);   
            transform.position = Vector2.MoveTowards(transform.position, detcetive.TargetLastPos, monsterSpeed * Time.deltaTime);
            animator.SetBool("IsMove", true);

            if(player != null)
            {
                animator.SetFloat("Monster", 0.5f);
            }
            else
                animator.SetFloat("Monster", 0);

        }

        Vector2 front = new Vector2(transform.position.x + randomMove, transform.position.y); //�ٴ�üũ
        RaycastHit2D rayHit = Physics2D.Raycast(front, Vector2.down * 3);
        Debug.DrawRay(front, Vector2.down * 3, new Color(0, 1, 0));

        if (rayHit.collider == null)
        {
            randomMove *= -1;
            Debug.Log("�ٴھ���");
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        if (player != null)
            animator.SetFloat("Monster", 0.5f);
    }

    void Move()
    {
        randomMove = Random.Range(-1, 2);
        Invoke("Move", 3);
    }
}



