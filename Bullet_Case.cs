using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Case : MonoBehaviour     // źƼ�� Ȱ��ȭ�Ǹ� ť�� ����ִ� �͸� ��
{
    public Spawn spawn_Script;
    WaitForSeconds wait_Case = new WaitForSeconds(1f);    // ź�� ���� �ð��� 1��

    void OnEnable()
    {
        StartCoroutine(End_Cor());   // Ȱ��ȭ ���ڸ��� 1�� �� ��Ȱ��ȭ�ϴ� �ڷ�ƾ ����
    }


    IEnumerator End_Cor()
    {
        yield return wait_Case;
        spawn_Script.InsertQueue_Bullet_Case(transform.gameObject);   // ź�Ǹ� ������Ʈ Ǯ���� �������
    }
}
