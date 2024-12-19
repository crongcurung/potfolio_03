using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Hit : MonoBehaviour   // 총알이 방패에 닿으면 생기는 것
{
    public Spawn spawn_Script;

    WaitForSeconds wait_BulletHit = new WaitForSeconds(0.2f);    // 총알이 활성화하는 시간

    void OnEnable()
    {
        StartCoroutine(End_Bullet_Hit());     // 활성화되면 비활성화까지 되는 시간
    }

    IEnumerator End_Bullet_Hit()
    {
        yield return wait_BulletHit;
        spawn_Script.InsertQueue_Bullet_Hit(transform.gameObject);
    }
}
