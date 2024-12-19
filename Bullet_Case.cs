using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Case : MonoBehaviour     // 탄티는 활성화되면 큐에 집어넣는 것만 함
{
    public Spawn spawn_Script;
    WaitForSeconds wait_Case = new WaitForSeconds(1f);    // 탄피 남는 시간은 1초

    void OnEnable()
    {
        StartCoroutine(End_Cor());   // 활성화 되자마자 1초 후 비활성화하는 코루틴 실행
    }


    IEnumerator End_Cor()
    {
        yield return wait_Case;
        spawn_Script.InsertQueue_Bullet_Case(transform.gameObject);   // 탄피를 오브젝트 풀링에 집어넣음
    }
}
