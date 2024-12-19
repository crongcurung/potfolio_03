using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour           // �������� 20��, ��� �������� 7��
{
    public Transform Parent;           // �÷��̾ ���� �� ������Ʈ �ڽ��̱� ������ �̵��� ���� ������

    Animator anim;
    public GameObject bat_Pos;         // �⺻��� ����� ����
    public GameObject bat_Pos_02;      // ������� ����� ����
    public Transform bullet_Pos;       // �Ѿ� �߻� ��ġ

    Vector3 posUp;          // �÷��̾ ���̻� ���� �� �ö󰡰� �ϴ� ����
    Vector3 posDown;        // �÷��̾ ���̻� �Ʒ��� �� �������� �ϴ� ����

    

    public Spawn spawn_Script;
    public GameObject bim_Lazer;      // �⺻ ��� ������
    public GameObject bim_Lazer_02;   // ���� ��� ������
    public bool isLazer = false;      // ���� �������� �߻�Ǵ��� ���� ����
    public bool bim_Bool = false;



    bool isDevil = false;                   // ���� ����������� ���� ����
    public RuntimeAnimatorController anim_Ori;     // �⺻ ��� �ִ� ��Ʈ�ѷ�
    public RuntimeAnimatorController anim_Con;     // ���� ��� �ִ� ��Ʈ�ѷ�

    WaitForSeconds wait_Bim = new WaitForSeconds(7f);    // �� �ڷ�ƾ�� 7��
    WaitForSeconds wait_Devil = new WaitForSeconds(20f);    // ���� �ڷ�ƾ�� 20��

    Vector2 vec_Case;                // ź�ǰ� ���󰡴� ����(�������)

    public bool isLevelUp = false;
    public bool isPause = false;
    public bool isGameOver = false;

    
    void Awake()
    {
        anim = GetComponent<Animator>();

        posUp = new Vector3(0, 2, 0);           // �̵� ���� ��
        posDown = new Vector3(0, -2, 0);

        vec_Case = new Vector2(-1f, 9f);       // ź�ǰ� ���󰡴� ����(�������)
    }



    void Update()
    {
        if (isLevelUp || isPause || isGameOver)
        {
            return;
        }

        Move();                       // �̵� ���� �Լ�
         
        if (isLazer)                  // �������� �߻�ǰ� �ִٸ� �̵� ������ �����ϰ� ���� �ȵ�
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.K))      // �� ������ ������ ��Ʈ��
        {
            Bim_Start();
        }
    }

    public void Up_Button()
    {
        if (isLevelUp || isPause || isGameOver)
        {
            
            return;
        }

        if (transform.position.y <= 3.5f)
        {
            Parent.transform.position += posUp;    // �� ĭ�� �����̵��� �̵�
        }
    }

    public void Down_Button()
    {
        if (isLevelUp || isPause || isGameOver)
        {
            
            return;
        }

        if (transform.position.y >= -3.5f)
        {
            Parent.transform.position += posDown;    // �� ĭ�� �����̵��� �̵�
        }
    }

    void Move()       // �̵� ���� �Լ�
    {
        if (isLevelUp || isPause || isGameOver)
        {

            return;
        }

        if (Input.GetKeyDown(KeyCode.W) && transform.position.y <= 3.5f)  
        {
            Parent.transform.position += posUp;    // �� ĭ�� �����̵��� �̵�
        }
        else if (Input.GetKeyDown(KeyCode.S) && transform.position.y >= -3.5f)
        {
            Parent.transform.position += posDown;    // �� ĭ�� �����̵��� �̵�
        }
    }

    public void Bim_Start()      // �������� ������
    {
        if (isLevelUp || isPause || isGameOver || isLazer.Equals(true) || GameManager.Instance.bim_Bool.Equals(false))
        {
            return;
        }

        GameManager.Instance.is_PlayerBim();
        isLazer = true;                   // �������� �߻�ǰ� �ִٰ� �˸�
        anim.SetBool("doBim", true);
        if (isDevil)                     // ���� �����
        {
            bim_Lazer_02.SetActive(true);
            anim.SetTrigger("triggerBim");      // ������ �ִϸ��̼� ����
            
            bat_Pos_02.SetActive(false);

            
        }
        else                           // �⺻ �����...
        {
            bim_Lazer.SetActive(true);        // ������ Ȱ��ȭ
            anim.SetTrigger("triggerBim");      // ������ �ִϸ��̼� ����
            bat_Pos.SetActive(false);
            
        }

        StopCoroutine(Bim_End_Cor());              // �������� �߻�ɶ� �÷��̾ �� �������� �߻��� �� �����Ƿ� ��ž �ڷ�ƾ ��ġ
        StartCoroutine(Bim_End_Cor());        // ������ ����ð� �ڷ�ƾ ����
    }


    void Bim_End()        // �������� ����
    { 
        isLazer = false;                   // �������� �����ٰ� �˸�

        if (isDevil)                     // ���� �����
        {
            bim_Lazer_02.SetActive(false);

            bat_Pos_02.SetActive(true);
        }
        else                           // �⺻ �����...
        {
            bim_Lazer.SetActive(false);        // ������ ��Ȱ��ȭ

            bat_Pos.SetActive(true);
        }

        bim_Lazer_02.SetActive(false);     // �� �������� �� ����(�������� ������ � ������� �ľ��ϱ� �������;;;)
        bim_Lazer.SetActive(false);
        anim.SetBool("doBim", false);      // 
    }


    public void Bullet_Shot()                    // �⺻ �Ѿ� �߻� (�ִϸ��̼ǿ��� �ݺ� ����)
    {

        GameObject bullet = spawn_Script.GetQueue_Bullet();     // �Ѿ� ������ ��������
        bullet.transform.position = bullet_Pos.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500);   // �Ѿ� �߻�

        AudioManager.ins.PlayEffect("Gun_01");
    }

    public void Bat_Shot()     // ����� Ÿ��
    {
        if (isLazer)      // �������� �߻��ϰ� �ִٸ� �̽��� (����̴� �������� Ʈ���� �����̱� ������ ������� ��)
        {
            return;
        }
        anim.SetTrigger("doBat");     // ����� �ִϸ��̼� ����
        if (isDevil.Equals(false))
        {
            AudioManager.ins.PlayEffect("Bat_01");
        }
        else
        {
            AudioManager.ins.PlayEffect("Bat_02");
        }
    }


    public void Bullet_Shot_02()                    // ���� �Ѿ� �߻� (�ִϸ��̼ǿ��� �ݺ� ����)
    {
        GameObject bullet = spawn_Script.GetQueue_Bullet_02();     // �Ѿ� ������ ��������
        bullet.transform.position = bullet_Pos.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 800);   // �Ѿ� �߻�

        GameObject bullet_Case = spawn_Script.GetQueue_Bullet_Case();     // ź�� ������ ��������
        bullet_Case.transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z -1);
        bullet_Case.GetComponent<Rigidbody2D>().AddForce(vec_Case.normalized * 650);   // ź�� �߻�

        AudioManager.ins.PlayEffect("Gun_02");
    }


    public void Devil_Transfor()    // ���� ��� ����
    {
        if (isLazer)       // ���� �������� �߻����̶��..
        {
            bim_Lazer_02.SetActive(false);    // �Ѵ� ��������(Ȥ�� ����...)
            bim_Lazer.SetActive(false);
        }

        isDevil = true;                   // ���� ��������� �˷���
        GameManager.Instance.Player_Devil(isDevil);   // ���� ��������� �˷���
        bat_Pos.SetActive(false);          
        bat_Pos_02.SetActive(true);
        anim.runtimeAnimatorController = anim_Con;    // ���� �ִ� ��Ʈ�ѷ��� �ٲ�

        AudioManager.ins.PitchUp_BG();
        StartCoroutine(Devil_End());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))    // ���� �߻�ü
        {
            GameManager.Instance.GameEnd();
        }
        else if (collision.CompareTag("Item"))         // ������ �±�
        {
            if (collision.gameObject.layer.Equals(6))  // ���� ������
            {
                collision.gameObject.SetActive(false);
                Devil_Transfor();
            }
            else if (collision.gameObject.layer.Equals(7))   // ��� ������
            {
                GameManager.Instance.GameEnd();
            }
        }
    }


    IEnumerator Bim_End_Cor()              //   �� �ִϸ��̼��� ������ �ڷ�ƾ (7��)
    {
        yield return wait_Bim;

        Bim_End();        // 7�� ���Ŀ� ������ ����
    }

    IEnumerator Devil_End()   // �������� 20��
    {
        yield return wait_Devil;        // 20��
        isDevil = false;
        GameManager.Instance.Player_Devil(isDevil);       // ���� ��尡 ����Ǿ��ٰ� �˷���
        AudioManager.ins.PitchDown_BG();
        StopCoroutine(Bim_End_Cor());
        Bim_End();                        // Ȥ�ö� ������尡 ����Ǵ� ���ϼ��� �־ ����
        bat_Pos.SetActive(true);                 
        bat_Pos_02.SetActive(false);
        anim.runtimeAnimatorController = anim_Ori;   // �⺻ �ִ� ��Ʈ�ѷ��� ����
    }
}
