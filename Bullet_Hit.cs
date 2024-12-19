using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Hit : MonoBehaviour   // �Ѿ��� ���п� ������ ����� ��
{
    public Spawn spawn_Script;

    WaitForSeconds wait_BulletHit = new WaitForSeconds(0.2f);    // �Ѿ��� Ȱ��ȭ�ϴ� �ð�

    void OnEnable()
    {
        StartCoroutine(End_Bullet_Hit());     // Ȱ��ȭ�Ǹ� ��Ȱ��ȭ���� �Ǵ� �ð�
    }

    IEnumerator End_Bullet_Hit()
    {
        yield return wait_BulletHit;
        spawn_Script.InsertQueue_Bullet_Hit(transform.gameObject);
    }
}
