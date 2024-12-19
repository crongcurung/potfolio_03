using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Area : MonoBehaviour      // 플레이어 바로 앞에 영역
{
    public Player player;       // 플레이어 스크립트를 받아온다.(방망이 때문에)

    void OnTriggerEnter2D(Collider2D collision)       
    {
        if (collision.CompareTag("Monster"))        // 몬스터가 영역에 있다면
        {
            player.Bat_Shot();   // 방망이 공격

            if (collision.gameObject.layer.Equals(6))        // 기본 몬스터
            {
                collision.GetComponent<Mon_01>().Hitted_Player(1, false);     // 해당 몬스터에게 방망이에 맞았다고 알려줌
            }
            else if (collision.gameObject.layer.Equals(7))   // 방어 몬스터
            {
                collision.GetComponent<Mon_02>().Hitted_Player(1, false);     // 해당 몬스터에게 방망이에 맞았다고 알려줌
            }
        }
    }
}
