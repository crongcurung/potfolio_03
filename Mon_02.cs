using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mon_02 : MonoBehaviour
{
    public Spawn spawn_Script;
    float Speed = 1f;

    WaitForFixedUpdate wait = new WaitForFixedUpdate();
    WaitForSeconds wait_StartAndHealth = new WaitForSeconds(1f);           // ó�� �������� �� �����ϴ� �ڷ�ƾ
    WaitForSeconds wait_ShieldNot = new WaitForSeconds(0.3f);           // ó�� �������� �� �����ϴ� �ڷ�ƾ

    Rigidbody2D rigid;
    Animator anim;
    Collider2D col;
    AudioSource audio_Mon;
    public AudioClip clip_Knock;
    public AudioClip clip_Death;
    public AudioClip clip_Sheild;


    public GameObject healthBar;
    public Slider slider;
    public Image slider_Back;
    public Image slider_Fill;
    public bool isShield;            // ���� ���� �������� ���� ����

    public RuntimeAnimatorController anim_Ori;        // 2�� ��Ʈ�ѷ�
    public RuntimeAnimatorController anim_Con;        // 1�� ��Ʈ�ѷ�

    Color fill_Color;      // �Ѹ� �����̴� ����

    bool isWall = false;        // ���Ͱ� ������ ���� ���� �پ��ֳ�?

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        audio_Mon = GetComponent<AudioSource>();

        fill_Color = slider_Fill.color;       // ù���� ��� �����̵� �Ѹ� ���� ����

        isShield = true;
    }

    void OnEnable()
    {
        isShield = true;

        slider.value = 1f;
        slider_Back.color = Color.red;        // ù���� ��� �����̵� ��� ������ ����������
        slider_Fill.color = fill_Color;       // ù���� ��� �����̵� �Ѹ� ���� ���� ����

        StartCoroutine(Start_Cor());
        Speed = GameManager.Instance.speed_Mon2;
    }

    IEnumerator Start_Cor()
    {
        yield return wait_StartAndHealth;
        isWall = false;

        anim.SetBool("doAttack", false);
    }

    void FixedUpdate()
    {
        if (rigid.position.x > 20.0f)
        {
            spawn_Script.InsertQueue_Mon_02(transform.gameObject);
        }

        if (slider.value <= 0 || isWall || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }

        if (rigid.position.x < -7.9f)      // ������
        {
            isWall = true;

            anim.SetBool("doAttack", true);
            
            return;
        }

        rigid.MovePosition(rigid.position + Vector2.left * Speed * Time.fixedDeltaTime);
        rigid.velocity = Vector2.zero;

        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (slider.value <= 0)
        {
            return;
        }
    }


    public void Hitted_Player(int num, bool isDevil)         // 0���̸� ����, 1���̸� �����
    {
        if (num.Equals(0))   // ���ѿ� �ǰ�
        {
            if (isShield == true && slider.value > 0)   // ���� ���Ͱ� ���尡 �ִ� ���
            {
                audio_Mon.clip = clip_Sheild;
                audio_Mon.Play();

                if (isDevil.Equals(false))
                {
                    slider.value -= 1 * GameManager.Instance.demage_Bullet_01 * 0.1f;
                }
                else
                {
                    slider.value -= 1 * GameManager.Instance.demage_Bullet_02 * 0.1f;
                }

                StopAllCoroutines();
                StartCoroutine(Wait_Health_Cor());

                if (slider.value <= 0)
                {
                    slider.value = 1f;
                    slider_Back.color = Color.white;
                    slider_Fill.color = Color.red;

                    anim.runtimeAnimatorController = anim_Con;   // 2�� ��Ʈ�ѷ����� 1�� ��Ʈ�ѷ���
                    isShield = false;
                }

                return;
            }
            else if (isShield == true && slider.value <= 0)
            {
                slider.value = 1f;
                slider_Back.color = Color.white;
                slider_Fill.color = Color.red;

                anim.runtimeAnimatorController = anim_Con;       // 2�� ��Ʈ�ѷ����� 1�� ��Ʈ�ѷ���

                isShield = false;
            }


            if (isDevil.Equals(false))
            {
                slider.value -= 1 * GameManager.Instance.demage_Bullet_01 * 0.1f;
            }
            else
            {
                slider.value -= 1 * GameManager.Instance.demage_Bullet_02 * 0.1f;
            }

            StopAllCoroutines();
            StartCoroutine(Wait_Health_Cor());

            if (slider.value <= 0)
            {
                Dead_First();
                return;
            }

            anim.SetTrigger("triggerHit");
            StartCoroutine(KnockBack());
        }
        else if (num.Equals(1))    // ����̿� �ǰ�....................///////////////////////////////////////////////////////////////////////
        {
            if (isShield == true && slider.value > 0)
            {

                rigid.AddForce(Vector2.right * 1000);
                audio_Mon.clip = clip_Knock;
                audio_Mon.Play();

                isWall = false;

                StopAllCoroutines();
                StartCoroutine(Wait_Health_Cor());
                anim.SetBool("doAttack", false);

                isShield = false;
                slider.value = 0;
                StartCoroutine(Shield_Not());
                anim.runtimeAnimatorController = anim_Con;       // 2�� ��Ʈ�ѷ����� 1�� ��Ʈ�ѷ���
                
                return;
            }
            else if(isShield == false && slider.value > 0)
            {
                if (isDevil.Equals(false))
                {
                    slider.value -= 1 * GameManager.Instance.demage_Bat_01 * 0.1f;
                }
                else
                {
                    slider.value -= 1 * GameManager.Instance.demage_Bat_02 * 0.1f;
                }

                if (slider.value > 0)
                {
                    rigid.AddForce(Vector2.right * 1000);

                    isWall = false;

                    anim.SetBool("doAttack", false);
                    anim.SetTrigger("triggerHit");
                    StopAllCoroutines();
                    StartCoroutine(Wait_Health_Cor());
                }
                else
                {
                    slider.value = 0;
                    Dead_First();
                }
                return;
            }

            if (isDevil.Equals(false))
            {
                slider.value -= 1 * GameManager.Instance.demage_Bat_01 * 0.1f;
            }
            else
            {
                slider.value -= 1 * GameManager.Instance.demage_Bat_02 * 0.1f;
            }
            Dead_First();
        }
        else if (num.Equals(2))    // �⺻ �������� �ǰ�
        {
            if (isShield == true && slider.value > 0)
            {
                StopAllCoroutines();
                StartCoroutine(Wait_Health_Cor());
                StartCoroutine(Shield_Not());
                anim.SetBool("doAttack", false);
                anim.runtimeAnimatorController = anim_Con;       // 2�� ��Ʈ�ѷ����� 1�� ��Ʈ�ѷ���

                isShield = false;
                isWall = false;

                return;
            }

            slider.value = 0;
            Dead_First();

            if (isDevil.Equals(false))
            {
                slider.value -= 1 * GameManager.Instance.demage_Bim_01;
            }
            else
            {
                slider.value -= 1 * GameManager.Instance.demage_Bim_02;
            }

            if (slider.value > 0)
            {
                rigid.AddForce(Vector2.right * 30);
                audio_Mon.clip = clip_Knock;
                audio_Mon.Play();

                anim.SetBool("doAttack", false);
                anim.SetTrigger("triggerHit");

                isWall = false;
                StopAllCoroutines();
                StartCoroutine(Wait_Health_Cor());
            }
            else
            {
                Dead_First();
            }
        }
    }

    

    public void Dead_First()   // ���Ͱ� �׾��� �� ó������ �ϴ� �Լ�
    {
        GameManager.Instance.Kill_Num();      // ų ī��Ʈ�� �ø�
        audio_Mon.clip = clip_Death;
        audio_Mon.Play();
        col.enabled = false;
        anim.SetBool("doDead", true);
        anim.SetTrigger("triggerDead");
        rigid.velocity = Vector2.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        healthBar.SetActive(false);
    }

    public void Dead_End()   // ���Ͱ� �׾��� �� ���������� �ϴ� �Լ�
    {
        col.enabled = true;
        anim.SetBool("doDead", false);

        anim.SetBool("doAttack", false);
        anim.runtimeAnimatorController = anim_Ori;                     // 1�� ��Ʈ�ѷ����� 2�� ��Ʈ�ѷ���
        spawn_Script.InsertQueue_Mon_02(transform.gameObject);   // ���͸� ������Ʈ Ǯ���� �������
    }

    public void Attck_End()
    {
        GameManager.Instance.GameEnd();
    }

    IEnumerator Shield_Not()     // ���尡 �� ������ �� �ڷ�ƾ
    {
        slider.value = 0;
        yield return wait_ShieldNot;  // ������ �� �ڷ�ƾ���� �ؾ��ϴ����� ������, �׳� �Լ��� ���� �� ������ ������

        slider.value = 1f;
        slider_Back.color = Color.white;
        slider_Fill.color = Color.red;
    }

    IEnumerator KnockBack()       // �Ѿ˿� �¾��� �� ���������� �˹��ϴ� �ڷ�ƾ\
    {
        audio_Mon.clip = clip_Knock;
        audio_Mon.Play();
        yield return wait;
        rigid.AddForce(Vector2.right, ForceMode2D.Impulse);
    }

    IEnumerator Wait_Health_Cor()      // ü���� �����ٰ� ������� �ϴ� �ڷ�ƾ
    {
        healthBar.SetActive(true);          // ü�¹� Ȱ��ȭ
        yield return wait_StartAndHealth;
        healthBar.SetActive(false);          // ü�¹� ��Ȱ��ȭ
    }
}
