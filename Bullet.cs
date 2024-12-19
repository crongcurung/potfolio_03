using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour   // �⺻ �Ѿ�
{
    public Spawn spawn_Script;
    Rigidbody2D rigid;
    Collider2D col;

    Vector2 vec_01;    // ��� ���Ϳ� ����� �� ƨ���� ������ ����
    Vector2 vec_02;    // ��� ���Ϳ� ����� �� ƨ���� ������ ����

    WaitForSeconds wait_Bullet = new WaitForSeconds(2.0f);    // �Ѿ��� Ȱ��ȭ�ϴ� �ð�

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        vec_01 = new Vector2(0.7f, 10f);
        vec_02 = new Vector2(0.7f, -10f);
    }

    void OnEnable()
    {
        StartCoroutine(End_Cor());    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))          // ���Ϳ� ����� ���(�⺻ ������ ��ź)
        {
            if (collision.gameObject.layer.Equals(6))   // �⺻ ���Ϳ� ����� ���
            {
                Gather_01();     
                StopAllCoroutines();
                collision.GetComponent<Mon_01>().Hitted_Player(0, false);
                spawn_Script.InsertQueue_Bullet(transform.gameObject);
                
            }
            else if (collision.gameObject.layer.Equals(7))        // ��� ���Ϳ� ����� ���
            {
                collision.GetComponent<Mon_02>().Hitted_Player(0, false);

                if (collision.GetComponent<Mon_02>().isShield)     // ���尡 ���� ���
                {
                    col.enabled = false;
                    GameObject bullet_Hit = spawn_Script.GetQueue_Bullet_Hit();
                    bullet_Hit.transform.position = transform.position;


                    if (Random.Range(0, 2).Equals(0))                         // 
                    {
                        rigid.AddForce(vec_01.normalized * 1000);
                        transform.rotation = Quaternion.Euler(0, 0, 60);
                    }
                    else
                    {
                        rigid.AddForce(vec_02.normalized * 1000);              // 
                        transform.rotation = Quaternion.Euler(0, 0, -60);
                    }
                }
                else                               // ���尡 ���� ���
                {
                    Gather_01();
                    StopAllCoroutines();
                    spawn_Script.InsertQueue_Bullet(transform.gameObject);
                }
            }
            else if(collision.gameObject.layer.Equals(8))     // ���Ÿ� ����
            {
                Gather_01();
                StopAllCoroutines();
                collision.GetComponent<Mon_03>().Hitted_Player(0);
                spawn_Script.InsertQueue_Bullet(transform.gameObject);
            }
        }
    }


    void Gather_01()
    {
        col.enabled = true;
        rigid.velocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }


    IEnumerator End_Cor()     // �ҷ��� ������ �ð��� ������ �ҷ��� �������� �Ѵ�.
    {
        yield return wait_Bullet;
        Gather_01();
        spawn_Script.InsertQueue_Bullet(transform.gameObject);
    }
}
