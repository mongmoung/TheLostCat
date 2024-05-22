using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D circle;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
    }
    public void Active()
    {
        //�˶� ���� �����
        Debug.Log("�������������Ѹ�����������");
        gameObject.layer = 6;
        circle.radius = 10;
        StartCoroutine(ConfusionCo());
    }
    public IEnumerator ConfusionCo()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
