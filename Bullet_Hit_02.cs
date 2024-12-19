using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Hit_02 : MonoBehaviour    // 데빌 총알 히트
{
    public Spawn spawn_Script;
    WaitForSeconds wait_BulletHit = new WaitForSeconds(0.2f);    // 총알이 활성화하는 시간

    void OnEnable()
    {
        StartCoroutine(End_Bullet_Hit());
    }

    IEnumerator End_Bullet_Hit()
    {
        yield return wait_BulletHit;
        spawn_Script.InsertQueue_Bullet_Hit_02(transform.gameObject);
    }
}
