using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Hit_02 : MonoBehaviour    // ���� �Ѿ� ��Ʈ
{
    public Spawn spawn_Script;
    WaitForSeconds wait_BulletHit = new WaitForSeconds(0.2f);    // �Ѿ��� Ȱ��ȭ�ϴ� �ð�

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
