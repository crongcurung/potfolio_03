using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour   // ���, ���� ������
{
    Rigidbody2D rigid;
    float Speed;
    WaitForSeconds wait_Item = new WaitForSeconds(10f);    // ������ ���� �ð��� 10��

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        
    }

    void OnEnable()      // ��Ÿ�� �� ����
    {
        StartCoroutine(End_Cor());                   // �ð��� ������ ������ �ڷ�ƾ

        Speed = Random.Range(5, 15);          // ������ �ӵ��� ����
    }

    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + Vector2.left * Speed * Time.fixedDeltaTime);
        rigid.velocity = Vector2.zero;
    }


    IEnumerator End_Cor()      // �ð��� ������ ������ �ڷ�ƾ
    {
        yield return wait_Item;
        transform.gameObject.SetActive(false);
    }
}
