using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Back_Manager : MonoBehaviour
{
    public GameObject[] back_Array = new GameObject[4];   // ��, ����, ����, �ܿ�
    public SpriteRenderer back_Sprite;                    // ����� ���ĸ� ���

    WaitForSeconds wait_Back = new WaitForSeconds(60.0f);    // �Ѿ��� Ȱ��ȭ�ϴ� �ð�

    void Start()
    {
        StartCoroutine(Back_Cor());
    }

    List<int> Back_Int = new List<int>() { 0, 1, 2, 3 };

    IEnumerator Back_Cor()
    {

        int rand = Random.Range(0, Back_Int.Count);
        int now_Int = Back_Int[rand];
        back_Array[now_Int].SetActive(true);
        Back_Int.RemoveAt(rand);

        
        while (true)
        {
            yield return wait_Back;
            back_Array[now_Int].SetActive(false);
            rand = Random.Range(0, Back_Int.Count);
            now_Int = Back_Int[rand];
            back_Array[Back_Int[rand]].SetActive(true);
            Back_Int.RemoveAt(rand);

            if (Back_Int.Count.Equals(0))
            {
                Back_Int = new List<int>() { 0, 1, 2, 3 };
            }

        }
    }
}
