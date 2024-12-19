using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour   // 당근, 데빌 아이템
{
    Rigidbody2D rigid;
    float Speed;
    WaitForSeconds wait_Item = new WaitForSeconds(10f);    // 아이템 남는 시간은 10초

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        
    }

    void OnEnable()      // 나타날 떄 마다
    {
        StartCoroutine(End_Cor());                   // 시간이 지나면 끝내는 코루틴

        Speed = Random.Range(5, 15);          // 아이템 속도는 랜덤
    }

    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + Vector2.left * Speed * Time.fixedDeltaTime);
        rigid.velocity = Vector2.zero;
    }


    IEnumerator End_Cor()      // 시간이 지나면 끝내는 코루틴
    {
        yield return wait_Item;
        transform.gameObject.SetActive(false);
    }
}
