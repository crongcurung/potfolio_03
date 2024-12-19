using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mon_02 : MonoBehaviour
{
    public Spawn spawn_Script;
    float Speed = 1f;

    WaitForFixedUpdate wait = new WaitForFixedUpdate();
    WaitForSeconds wait_StartAndHealth = new WaitForSeconds(1f);           // 처음 시작했을 때 실행하는 코루틴
    WaitForSeconds wait_ShieldNot = new WaitForSeconds(0.3f);           // 처음 시작했을 때 실행하는 코루틴

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
    public bool isShield;            // 현재 쉴드 상태인지 묻는 변수

    public RuntimeAnimatorController anim_Ori;        // 2번 컨트롤러
    public RuntimeAnimatorController anim_Con;        // 1번 컨트롤러

    Color fill_Color;      // 겉면 슬라이더 색깔

    bool isWall = false;        // 몬스터가 끝까지 가서 벽에 붙어있나?

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        audio_Mon = GetComponent<AudioSource>();

        fill_Color = slider_Fill.color;       // 첫번쨰 배경 슬라이드 겉면 색깔 저장

        isShield = true;
    }

    void OnEnable()
    {
        isShield = true;

        slider.value = 1f;
        slider_Back.color = Color.red;        // 첫번쨰 배경 슬라이드 배경 색깔은 빨간색으로
        slider_Fill.color = fill_Color;       // 첫번쨰 배경 슬라이드 겉면 색깔 원상 복귀

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

        if (rigid.position.x < -7.9f)      // 끝까지
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


    public void Hitted_Player(int num, bool isDevil)         // 0번이면 권총, 1번이면 방망이
    {
        if (num.Equals(0))   // 권총에 피격
        {
            if (isShield == true && slider.value > 0)   // 현재 몬스터가 쉴드가 있는 경우
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

                    anim.runtimeAnimatorController = anim_Con;   // 2번 컨트롤러에서 1번 컨트롤러로
                    isShield = false;
                }

                return;
            }
            else if (isShield == true && slider.value <= 0)
            {
                slider.value = 1f;
                slider_Back.color = Color.white;
                slider_Fill.color = Color.red;

                anim.runtimeAnimatorController = anim_Con;       // 2번 컨트롤러에서 1번 컨트롤러로

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
        else if (num.Equals(1))    // 방망이에 피격....................///////////////////////////////////////////////////////////////////////
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
                anim.runtimeAnimatorController = anim_Con;       // 2번 컨트롤러에서 1번 컨트롤러로
                
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
        else if (num.Equals(2))    // 기본 레이저에 피격
        {
            if (isShield == true && slider.value > 0)
            {
                StopAllCoroutines();
                StartCoroutine(Wait_Health_Cor());
                StartCoroutine(Shield_Not());
                anim.SetBool("doAttack", false);
                anim.runtimeAnimatorController = anim_Con;       // 2번 컨트롤러에서 1번 컨트롤러로

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

    

    public void Dead_First()   // 몬스터가 죽었을 떄 처음으로 하는 함수
    {
        GameManager.Instance.Kill_Num();      // 킬 카운트를 올림
        audio_Mon.clip = clip_Death;
        audio_Mon.Play();
        col.enabled = false;
        anim.SetBool("doDead", true);
        anim.SetTrigger("triggerDead");
        rigid.velocity = Vector2.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        healthBar.SetActive(false);
    }

    public void Dead_End()   // 몬스터가 죽었을 떄 마지막으로 하는 함수
    {
        col.enabled = true;
        anim.SetBool("doDead", false);

        anim.SetBool("doAttack", false);
        anim.runtimeAnimatorController = anim_Ori;                     // 1번 컨트롤러에서 2번 컨트롤러로
        spawn_Script.InsertQueue_Mon_02(transform.gameObject);   // 몬스터를 오브젝트 풀링에 집어넣음
    }

    public void Attck_End()
    {
        GameManager.Instance.GameEnd();
    }

    IEnumerator Shield_Not()     // 쉴드가 다 까졌을 때 코루틴
    {
        slider.value = 0;
        yield return wait_ShieldNot;  // 솔직히 왜 코루틴으로 해야하는지는 모르지만, 그냥 함수로 했을 때 오류가 나긴함

        slider.value = 1f;
        slider_Back.color = Color.white;
        slider_Fill.color = Color.red;
    }

    IEnumerator KnockBack()       // 총알에 맞았을 때 순간적으로 넉백하는 코루틴\
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
