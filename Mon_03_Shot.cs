using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mon_03_Shot : MonoBehaviour
{
    public Spawn spawn_Script;

    float z;

    WaitForSeconds wait_End = new WaitForSeconds(5f);   // 5�� �Ŀ� ���Ÿ� ���� ȸ��

    void OnEnable()
    {
        StartCoroutine(End_Mon_03_Shot());   
    }

    void Update()
    {
        z += Time.deltaTime * 100;
        transform.rotation = Quaternion.Euler(0, 0, z);   // ���ۺ��� ������
    }

    IEnumerator End_Mon_03_Shot()
    {
        yield return wait_End;

        spawn_Script.InsertQueue_Mon_03_Shot(transform.gameObject);   // 5�� �Ŀ� ���Ÿ� ���� ȸ��
    }
}
