using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mon_01 : MonoBehaviour
{
    public Spawn spawn_Script;
    float Speed = 1f;


    WaitForFixedUpdate wait = new WaitForFixedUpdate();
    WaitForSeconds wait_StartAndHealth = new WaitForSeconds(1f);           // 처음 시작했을 때 실행하는 코루틴

    Rigidbody2D rigid;
    Animator anim;
    Collider2D col;
    SpriteRenderer sprite;
    AudioSource audio_Mon;
    public AudioClip clip_Knock;
    public AudioClip clip_Death;


    Color color = new Color(1, 1, 1, 1);      // 원래 색깔로 되돌리는 색깔

    public GameObject healthBar;
    public Slider slider;


    bool isWall = false;        // 몬스터가 끝까지 가서 벽에 붙어있나?


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

        if (rigid.position.x < -7.9f)      // 끝까지
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


    public void Hitted_Player(int num, bool isDevil)         // 0번이면 권총, 1번이면 방망이         true면 데빌모드, false면 기본모드
    {
        if (num.Equals(0))   // 기본 권총에 피격
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
        else if (num.Equals(1))    // 기본 방망이에 피격
        {
            if (isDevil.Equals(false))   // 기본 모드라면...
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
        else if (num.Equals(2))    // 기본 레이저에 피격
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
        else if (num.Equals(10))   // 파란 벽에 부딪혔을 때
        {
            Dead_First();
        }
    }

    public void Dead_First()   // 몬스터가 죽었을 떄 처음으로 하는 함수
    {
        GameManager.Instance.Kill_Num();      // 킬 카운트를 올림
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

    public void Dead_End()   // 몬스터가 죽었을 떄 마지막으로 하는 함수
    {
        col.enabled = true;
        anim.SetBool("doAttack", false);
        anim.SetBool("doDead", false);
        sprite.color = color;
        spawn_Script.InsertQueue_Mon_01(transform.gameObject);   // 몬스터를 오브젝트 풀링에 집어넣음
    } 

    public void Attck_End()
    {
        GameManager.Instance.GameEnd();
    }

    IEnumerator KnockBack()       // 총알에 맞았을 때 순간적으로 넉백하는 코루틴
    {
        audio_Mon.clip = clip_Knock;
        audio_Mon.Play();
        yield return wait;
        
        rigid.AddForce(Vector2.right, ForceMode2D.Impulse);
    }

    IEnumerator Wait_Health_Cor()      // 체력을 보였다가 사라지게 하는 코루틴
    {
        healthBar.SetActive(true);          // 체력바 활성화
        yield return wait_StartAndHealth;
        healthBar.SetActive(false);          // 체력바 비활성화
    }

    

}
