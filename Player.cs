using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour           // 데빌모드는 20초, 모든 레이저는 7초
{
    public Transform Parent;           // 플레이어가 현재 빈 오브젝트 자식이기 때문에 이동을 위해 가져옴

    Animator anim;
    public GameObject bat_Pos;         // 기본모드 방망이 영역
    public GameObject bat_Pos_02;      // 데빌모드 방망이 영역
    public Transform bullet_Pos;       // 총알 발사 위치

    Vector3 posUp;          // 플레이어를 더이상 위로 못 올라가게 하는 변수
    Vector3 posDown;        // 플레이어를 더이상 아래로 못 내려가게 하는 변수

    

    public Spawn spawn_Script;
    public GameObject bim_Lazer;      // 기본 모드 레이저
    public GameObject bim_Lazer_02;   // 데빌 모드 레이저
    public bool isLazer = false;      // 현재 레이저가 발사되는지 묻는 변수
    public bool bim_Bool = false;



    bool isDevil = false;                   // 현재 데빌모드인지 묻는 변수
    public RuntimeAnimatorController anim_Ori;     // 기본 모드 애니 컨트롤러
    public RuntimeAnimatorController anim_Con;     // 데빌 모드 애니 컨트롤러

    WaitForSeconds wait_Bim = new WaitForSeconds(7f);    // 빔 코루틴은 7초
    WaitForSeconds wait_Devil = new WaitForSeconds(20f);    // 데빌 코루틴은 20초

    Vector2 vec_Case;                // 탄피가 날라가는 각도(데빌모드)

    public bool isLevelUp = false;
    public bool isPause = false;
    public bool isGameOver = false;

    
    void Awake()
    {
        anim = GetComponent<Animator>();

        posUp = new Vector3(0, 2, 0);           // 이동 조정 값
        posDown = new Vector3(0, -2, 0);

        vec_Case = new Vector2(-1f, 9f);       // 탄피가 날라가는 각도(데빌모드)
    }



    void Update()
    {
        if (isLevelUp || isPause || isGameOver)
        {
            return;
        }

        Move();                       // 이동 관련 함수
         
        if (isLazer)                  // 레이저가 발사되고 있다면 이동 관련을 제외하곤 실행 안됨
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.K))      // 빔 레이저 나오는 컨트롤
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
            Parent.transform.position += posUp;    // 한 칸씩 움직이도록 이동
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
            Parent.transform.position += posDown;    // 한 칸씩 움직이도록 이동
        }
    }

    void Move()       // 이동 관련 함수
    {
        if (isLevelUp || isPause || isGameOver)
        {

            return;
        }

        if (Input.GetKeyDown(KeyCode.W) && transform.position.y <= 3.5f)  
        {
            Parent.transform.position += posUp;    // 한 칸씩 움직이도록 이동
        }
        else if (Input.GetKeyDown(KeyCode.S) && transform.position.y >= -3.5f)
        {
            Parent.transform.position += posDown;    // 한 칸씩 움직이도록 이동
        }
    }

    public void Bim_Start()      // 레이저를 시작함
    {
        if (isLevelUp || isPause || isGameOver || isLazer.Equals(true) || GameManager.Instance.bim_Bool.Equals(false))
        {
            return;
        }

        GameManager.Instance.is_PlayerBim();
        isLazer = true;                   // 레이저가 발사되고 있다고 알림
        anim.SetBool("doBim", true);
        if (isDevil)                     // 데빌 모드라면
        {
            bim_Lazer_02.SetActive(true);
            anim.SetTrigger("triggerBim");      // 레이저 애니메이션 실행
            
            bat_Pos_02.SetActive(false);

            
        }
        else                           // 기본 모드라면...
        {
            bim_Lazer.SetActive(true);        // 레이저 활성화
            anim.SetTrigger("triggerBim");      // 레이저 애니메이션 실행
            bat_Pos.SetActive(false);
            
        }

        StopCoroutine(Bim_End_Cor());              // 레이저가 발사될때 플레이어가 또 레이저를 발사할 수 있으므로 스탑 코루틴 배치
        StartCoroutine(Bim_End_Cor());        // 레이저 실행시간 코루틴 실행
    }


    void Bim_End()        // 레이저를 끝냄
    { 
        isLazer = false;                   // 레이저가 끝났다고 알림

        if (isDevil)                     // 데빌 모드라면
        {
            bim_Lazer_02.SetActive(false);

            bat_Pos_02.SetActive(true);
        }
        else                           // 기본 모드라면...
        {
            bim_Lazer.SetActive(false);        // 레이저 비활성화

            bat_Pos.SetActive(true);
        }

        bim_Lazer_02.SetActive(false);     // 둘 레이저를 다 끝냄(레이저가 끝날떄 어떤 모드인지 파악하기 어려워서;;;)
        bim_Lazer.SetActive(false);
        anim.SetBool("doBim", false);      // 
    }


    public void Bullet_Shot()                    // 기본 총알 발사 (애니메이션에서 반복 실행)
    {

        GameObject bullet = spawn_Script.GetQueue_Bullet();     // 총알 프리팹 가져오기
        bullet.transform.position = bullet_Pos.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500);   // 총알 발사

        AudioManager.ins.PlayEffect("Gun_01");
    }

    public void Bat_Shot()     // 방망이 타격
    {
        if (isLazer)      // 레이저가 발사하고 있다면 미실행 (방망이는 영역에서 트리거 실행이기 때문에 막아줘야 함)
        {
            return;
        }
        anim.SetTrigger("doBat");     // 방망이 애니메이션 실행
        if (isDevil.Equals(false))
        {
            AudioManager.ins.PlayEffect("Bat_01");
        }
        else
        {
            AudioManager.ins.PlayEffect("Bat_02");
        }
    }


    public void Bullet_Shot_02()                    // 데빌 총알 발사 (애니메이션에서 반복 실행)
    {
        GameObject bullet = spawn_Script.GetQueue_Bullet_02();     // 총알 프리팹 가져오기
        bullet.transform.position = bullet_Pos.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 800);   // 총알 발사

        GameObject bullet_Case = spawn_Script.GetQueue_Bullet_Case();     // 탄피 프리팹 가져오기
        bullet_Case.transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z -1);
        bullet_Case.GetComponent<Rigidbody2D>().AddForce(vec_Case.normalized * 650);   // 탄피 발사

        AudioManager.ins.PlayEffect("Gun_02");
    }


    public void Devil_Transfor()    // 데빌 모드 변신
    {
        if (isLazer)       // 현재 레이저를 발사중이라면..
        {
            bim_Lazer_02.SetActive(false);    // 둘다 지워버림(혹시 몰라서...)
            bim_Lazer.SetActive(false);
        }

        isDevil = true;                   // 현재 데빌모드라고 알려줌
        GameManager.Instance.Player_Devil(isDevil);   // 현재 데빌모드라고 알려줌
        bat_Pos.SetActive(false);          
        bat_Pos_02.SetActive(true);
        anim.runtimeAnimatorController = anim_Con;    // 데빌 애니 컨트롤러로 바꿈

        AudioManager.ins.PitchUp_BG();
        StartCoroutine(Devil_End());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))    // 몬스터 발사체
        {
            GameManager.Instance.GameEnd();
        }
        else if (collision.CompareTag("Item"))         // 아이템 태그
        {
            if (collision.gameObject.layer.Equals(6))  // 데빌 아이템
            {
                collision.gameObject.SetActive(false);
                Devil_Transfor();
            }
            else if (collision.gameObject.layer.Equals(7))   // 당근 아이템
            {
                GameManager.Instance.GameEnd();
            }
        }
    }


    IEnumerator Bim_End_Cor()              //   빔 애니메이션을 끝내는 코루틴 (7초)
    {
        yield return wait_Bim;

        Bim_End();        // 7초 이후에 레이저 종료
    }

    IEnumerator Devil_End()   // 데빌모드는 20초
    {
        yield return wait_Devil;        // 20초
        isDevil = false;
        GameManager.Instance.Player_Devil(isDevil);       // 데빌 모드가 종료되었다고 알려줌
        AudioManager.ins.PitchDown_BG();
        StopCoroutine(Bim_End_Cor());
        Bim_End();                        // 혹시라도 데빌모드가 실행되는 중일수도 있어서 종료
        bat_Pos.SetActive(true);                 
        bat_Pos_02.SetActive(false);
        anim.runtimeAnimatorController = anim_Ori;   // 기본 애니 컨트롤러로 변경
    }
}
