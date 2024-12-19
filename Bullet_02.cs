using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_02 : MonoBehaviour
{
    public Spawn spawn_Script;
    //public GameObject bullet_Hit_prefab;
    Rigidbody2D rigid;
    Collider2D col;

    Vector2 vec_01;
    Vector2 vec_02;

    WaitForSeconds wait_Bullet = new WaitForSeconds(1.5f);    // 총알이 활성화하는 시간

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
        if (collision.CompareTag("Monster"))          // 몬스터에 닿았을 경우(기본 사이즈 폭탄)
        {
            if (collision.gameObject.layer.Equals(6))
            {
                Gather_01();
                collision.GetComponent<Mon_01>().Hitted_Player(0, true);
                spawn_Script.InsertQueue_Bullet_02(transform.gameObject);
            }
            else if (collision.gameObject.layer.Equals(7))        // 방어 몬스터에 닿았을 경우
            {
                collision.GetComponent<Mon_02>().Hitted_Player(0, true);

                if (collision.GetComponent<Mon_02>().isShield)     // 쉴드가 있을 경우
                {
                    col.enabled = false;
                    GameObject bullet_Hit = spawn_Script.GetQueue_Bullet_Hit_02();
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
                else                               // 쉴드가 없을 경우
                {
                    Gather_01();
                    StopAllCoroutines();
                    spawn_Script.InsertQueue_Bullet_02(transform.gameObject);
                }
            }
            else if (collision.gameObject.layer.Equals(8))     // 원거리 몬스터
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


    IEnumerator End_Cor()     // 불렛이 나오고 시간이 지나면 불렛이 끝나도록 한다.
    {
        yield return wait_Bullet;
        Gather_01();
        spawn_Script.InsertQueue_Bullet_02(transform.gameObject);
    }
}
