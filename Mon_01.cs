using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mon_01 : MonoBehaviour
{
    public Spawn spawn_Script;
    float Speed = 1f;


    WaitForFixedUpdate wait = new WaitForFixedUpdate();
    WaitForSeconds wait_StartAndHealth = new WaitForSeconds(1f);           // ó�� �������� �� �����ϴ� �ڷ�ƾ

    Rigidbody2D rigid;
    Animator anim;
    Collider2D col;
    SpriteRenderer sprite;
    AudioSource audio_Mon;
    public AudioClip clip_Knock;
    public AudioClip clip_Death;


    Color color = new Color(1, 1, 1, 1);      // ���� ����� �ǵ����� ����

    public GameObject healthBar;
    public Slider slider;


    bool isWall = false;        // ���Ͱ� ������ ���� ���� �پ��ֳ�?


    void Awake()
    {
         
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        audio_Mon = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        slider.value = 1.0f;
        anim.SetBool("doAttack", false);
        anim.SetBool("doDead", false);
        StartCoroutine(Start_Cor());
        Speed = GameManager.Instance.speed_Mon1;
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

        if (rigid.position.x > 20.0f)
        {
            spawn_Script.InsertQueue_Mon_01(transform.gameObject);
        }
    }


    public void Hitted_Player(int num, bool isDevil)         // 0���̸� ����, 1���̸� �����         true�� �������, false�� �⺻���
    {
        if (num.Equals(0))   // �⺻ ���ѿ� �ǰ�
        {
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

            anim.SetBool("doAttack", false);
            anim.SetTrigger("triggerHit");
            StartCoroutine(KnockBack());
        }
        else if (num.Equals(1))    // �⺻ ����̿� �ǰ�
        {
            if (isDevil.Equals(false))   // �⺻ �����...
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
                slider.value = 0;
                Dead_First();
            }

        }
        else if (num.Equals(2))    // �⺻ �������� �ǰ�
        {
            if (isDevil.Equals(false))
            {
                slider.value -= 1 * GameManager.Instance.demage_Bim_01 * 0.1f;
            }
            else
            {
                slider.value -= 1 * GameManager.Instance.demage_Bim_02 * 0.1f;
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
        else if (num.Equals(10))   // �Ķ� ���� �ε����� ��
        {
            Dead_First();
        }
    }

    public void Dead_First()   // ���Ͱ� �׾��� �� ó������ �ϴ� �Լ�
    {
        GameManager.Instance.Kill_Num();      // ų ī��Ʈ�� �ø�
        audio_Mon.clip = clip_Death;
        audio_Mon.Play();
        col.enabled = false;
        anim.SetBool("doAttack", false);
        anim.SetBool("doDead", true);
        anim.SetTrigger("triggerDead");
        rigid.velocity = Vector2.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        healthBar.SetActive(false);
    }

    public void Dead_End()   // ���Ͱ� �׾��� �� ���������� �ϴ� �Լ�
    {
        col.enabled = true;
        anim.SetBool("doAttack", false);
        anim.SetBool("doDead", false);
        sprite.color = color;
        spawn_Script.InsertQueue_Mon_01(transform.gameObject);   // ���͸� ������Ʈ Ǯ���� �������
    } 

    public void Attck_End()
    {
        GameManager.Instance.GameEnd();
    }

    IEnumerator KnockBack()       // �Ѿ˿� �¾��� �� ���������� �˹��ϴ� �ڷ�ƾ
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
