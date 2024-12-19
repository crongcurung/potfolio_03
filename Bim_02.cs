using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bim_02 : MonoBehaviour
{
    Vector2 pos;
    Rigidbody2D rigid;
    public Player player;
    WaitForSeconds wait_Bim = new WaitForSeconds(1.5f);    // 데빌 빔 코루틴은 1.5초

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        pos = transform.localPosition;        // 원래 위치를 기억한다.
    }

    void OnEnable()
    {
        rigid.AddForce(Vector2.right * 6000);    // 레이저 발사
        StartCoroutine(End_Bim());
    }

    void OnDisable()
    {
        player.isLazer = false;          // 레이저가 끝났다고 알려줌
        transform.localPosition = pos;   // 원래 위치로 복귀
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))          // 몬스터에 닿았을 경우(기본 사이즈 폭탄)
        {
            if (collision.gameObject.layer.Equals(6))   // 기본 몬스터
            {
                collision.GetComponent<Mon_01>().Hitted_Player(2, true);
            }
            else if (collision.gameObject.layer.Equals(7))   // 방어 몬스터
            {
                collision.GetComponent<Mon_02>().Hitted_Player(2, true);
            }
            else if (collision.gameObject.layer.Equals(8))   // 원거리 몬스터
            {
                collision.GetComponent<Mon_03>().Hitted_Player(2);
            }
        }
    }

    IEnumerator End_Bim()
    {
        AudioManager.ins.PlayEffect("Lazer_02");
        yield return wait_Bim;
        rigid.velocity = Vector2.zero;
        transform.localPosition = pos;
        rigid.AddForce(Vector2.right * 6000);   // 다시 발사
        StartCoroutine(End_Bim());    // 빔 코루틴 동안 무한 반복
    }
}
