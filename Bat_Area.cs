using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Area : MonoBehaviour      // �÷��̾� �ٷ� �տ� ����
{
    public Player player;       // �÷��̾� ��ũ��Ʈ�� �޾ƿ´�.(����� ������)

    void OnTriggerEnter2D(Collider2D collision)       
    {
        if (collision.CompareTag("Monster"))        // ���Ͱ� ������ �ִٸ�
        {
            player.Bat_Shot();   // ����� ����

            if (collision.gameObject.layer.Equals(6))        // �⺻ ����
            {
                collision.GetComponent<Mon_01>().Hitted_Player(1, false);     // �ش� ���Ϳ��� ����̿� �¾Ҵٰ� �˷���
            }
            else if (collision.gameObject.layer.Equals(7))   // ��� ����
            {
                collision.GetComponent<Mon_02>().Hitted_Player(1, false);     // �ش� ���Ϳ��� ����̿� �¾Ҵٰ� �˷���
            }
        }
    }
}
