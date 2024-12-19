using System.Collections;
using UnityEngine;

public class Bim : MonoBehaviour   // �⺻ ������
{
    Vector2 pos;  
    Rigidbody2D rigid;
    public Player player;
    WaitForSeconds wait_Bim = new WaitForSeconds(1.0f);    // �⺻ �� �ڷ�ƾ�� 1��

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        pos = transform.localPosition;        // ���� ��ġ�� ����Ѵ�.
    }

    void OnEnable()
    {
        rigid.AddForce(Vector2.right * 3000);    // ������ �߻�
        StartCoroutine(End_Bim());
    }

    void OnDisable()
    {
        player.isLazer = false;          // �������� �����ٰ� �˷���
        transform.localPosition = pos;   // ���� ��ġ�� ����
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))          // ���Ϳ� ����� ���(�⺻ ������ ��ź)
        {
            if (collision.gameObject.layer.Equals(6))   // �⺻ ����
            {
                collision.GetComponent<Mon_01>().Hitted_Player(2, false);
            }
            else if (collision.gameObject.layer.Equals(7))   // ��� ����
            {
                collision.GetComponent<Mon_02>().Hitted_Player(2, false);
            }
            else if (collision.gameObject.layer.Equals(8))   // ���Ÿ� ����
            {
                collision.GetComponent<Mon_03>().Hitted_Player(2);
            }
        }
    }

    IEnumerator End_Bim()
    {
        AudioManager.ins.PlayEffect("Lazer_01");
        yield return wait_Bim; 
        rigid.velocity = Vector2.zero;
        transform.localPosition = pos;
        rigid.AddForce(Vector2.right * 3000);   // �ٽ� �߻�
        StartCoroutine(End_Bim());    // �� �ڷ�ƾ ���� ���� �ݺ�
    }
}