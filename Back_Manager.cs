using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Back_Manager : MonoBehaviour
{
    public GameObject[] back_Array = new GameObject[4];   // 봄, 여름, 가을, 겨울
    public SpriteRenderer back_Sprite;                    // 배경의 알파를 담당

    WaitForSeconds wait_Back = new WaitForSeconds(60.0f);    // 총알이 활성화하는 시간

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
