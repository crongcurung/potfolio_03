using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Mon_03 : MonoBehaviour
{
    public Spawn spawn_Script;
    float Speed = 1f;

    WaitForFixedUpdate wait;
    WaitForSeconds wait_Health;
    Rigidbody2D rigid;
    Animator anim;
    Collider2D col;
    SpriteRenderer sprite;
    AudioSource audio_Mon;
    public AudioClip clip_Portal;
    public AudioClip clip_Death;
    Color color;


    int life_Int;

    bool isFinal = false;

    public Material mat_Shader;
    Material mat_Ori;
    public Sprite sprite_Shader;
    Sprite sprite_Ori;

    void Awake()
    {
        wait = new WaitForFixedUpdate();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        wait_Health = new WaitForSeconds(2f);

        sprite = GetComponent<SpriteRenderer>();
        audio_Mon = GetComponent<AudioSource>();
        mat_Ori = sprite.material;
        sprite_Ori = sprite.sprite;
    }

    void OnEnable()
    {
        life_Int = GameManager.Instance.life_Mon3;
        anim.SetBool("doDead", false);

        sprite.color = new Color(1, 1, 1, 1);
        Speed = GameManager.Instance.speed_Mon3;
    }

    float dissolveAmount;
    bool isDissolving;
    bool isHit= false;

    void Update()                  // ��Ż �̵� ������ ������Ʈ�� ������
    {
        if (isHit)
        {
            if (isDissolving.Equals(false))    // 1���� 0����
            {
                dissolveAmount = Mathf.Clamp01(dissolveAmount + Time.deltaTime);
                sprite.material.SetFloat("_DissolveAmount", dissolveAmount);

                if (dissolveAmount.Equals(1))
                {
                    Hit_Middle();
                }
            }
            else                               // 0���� 1����
            {
                dissolveAmount = Mathf.Clamp01(dissolveAmount - Time.deltaTime);
                sprite.material.SetFloat("_DissolveAmount", dissolveAmount);

                if (dissolveAmount.Equals(0))
                {
                    Hit_End();
                }
            }
        }
    }


    void FixedUpdate()
    {
        if (life_Int <= 0 || isHit || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }

        if (rigid.position.x <= 0)   // �߰�����
        {
            if (isFinal == false)
            {
                anim.SetBool("doRun", false);
                anim.SetTrigger("triggerShot");
            }
            isFinal = true;
            return;
        }
        else
        {
            isFinal = false;
            rigid.MovePosition(rigid.position + Vector2.left * Speed * Time.fixedDeltaTime);
            rigid.velocity = Vector2.zero;
        }

        if (rigid.position.x > 20.0f)
        {
            spawn_Script.InsertQueue_Mon_03(transform.gameObject);
        }
    }


    public void Hitted_Player(int num)         // 0���̸� ����, 1���̸� �����
    {
        if (num.Equals(0))   // ���ѿ� �ǰ�
        {
            life_Int--;

            StopAllCoroutines();

            if (life_Int <= 0)
            {
                Dead_First();
                return;
            }

            Hit_First();
        }
        else if (num.Equals(2))    // �⺻ �������� �ǰ�
        {
            life_Int--;

            StopAllCoroutines();

            if (life_Int <= 0)
            {
                Dead_First();
                return;
            }

            Hit_First();
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
    }

    public void Dead_End()   // ���Ͱ� �׾��� �� ���������� �ϴ� �Լ�
    {
        col.enabled = true;
        sprite.color = new Color(1, 1, 1, 1);
        spawn_Script.InsertQueue_Mon_03(transform.gameObject);
    }

    public void Mon_03_Shot()
    {
        GameObject mon_03_Shot = spawn_Script.GetQueue_Mon_03_Shot();     // �Ѿ� ������ ��������
        mon_03_Shot.transform.position = transform.position;
        mon_03_Shot.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 200);   // �Ѿ� �߻�
    }


    void Hit_First()   // �Ѿ��̳� �������� ó�� �¾��� ��
    {
        anim.SetTrigger("triggerHit");
        col.enabled = false;

        sprite.sprite = sprite_Shader;
        sprite.material = mat_Shader;

        audio_Mon.clip = clip_Portal;
        audio_Mon.Play();

        isHit = true;
    }

    void Hit_Middle()   // �°� �Ŀ� �����̵�
    {
        int rand = Random.Range(0, 5);

        transform.position = new Vector3(transform.position.x, rand * 2 - 4, transform.position.z);

        isDissolving = true;
    }

    void Hit_End()    // ���� �̵� �� �ٽ� ����
    {
        if (isFinal)
        {
            anim.SetBool("doRun", false);
            anim.SetTrigger("triggerShot");
        }
        else
        {
            anim.SetBool("doRun", true);
        }

        isHit = false;
        col.enabled = true;
        isDissolving = false;

        sprite.sprite = sprite_Ori;
        sprite.material = mat_Ori;
    }
}
