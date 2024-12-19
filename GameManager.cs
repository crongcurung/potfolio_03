using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{
    static GameManager instance = null;

    public Player player;
    public Spawn spawn_Script;
    public Back_Manager back_Script;

    public TextMeshProUGUI text1;

    public Animator text_Anim;

    public TextMeshProUGUI level_Count;

    int kill_Int;
    public bool isPlayer_Devil = false;   // true면 데빌, false면 일반

    public GameObject UI_Panel;
    public ParticleSystem particle;
    public Transform[] UI_Item = new Transform[3];
    

    public int level_Int;
    public float demage_Bullet_01;
    public float demage_Bullet_02;
    public float demage_Bat_01;
    public float demage_Bat_02;
    public float demage_Bim_01;
    public float demage_Bim_02;

    public float speed_Mon1;
    public float speed_Mon2;
    public float speed_Mon3;


    public float anim_Speed;

    public int life_Mon3;     // 원거리 몬스터는 생명이 카운트로 들어가서 int임


    Color color_Devil = new Color(105, 0, 255);  // 데빌모드 테두리(킬카운트)

    int level = 1;

    int bim_Count = 0;
    public bool bim_Bool = false;
    public Image bim_Button;

    public GameObject GameOver_Panel;
    public GameObject Re_Button;
    public GameObject End_Button;

    public GameObject Pause_Panel;

    public GameObject Start_Panel;
    

    void Awake()
    {

        if (null == instance)
        {
            instance = this;

            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //Destroy(this.gameObject);
        }

        //Application.targetFrameRate = 60;

        level_Int = 1;

        demage_Bullet_01 = 2.0f;      
        demage_Bullet_02 = demage_Bullet_01 + 0.2f;
        demage_Bat_01 = 4.0f;
        demage_Bat_02 = demage_Bat_01 + 1.0f;
        demage_Bim_01 = 4.0f;
        demage_Bim_02 = demage_Bim_01 * 2;

        speed_Mon1 = 2.0f;      // 몬스터 스피드 관련
        speed_Mon2 = 1.7f;
        speed_Mon3 = 1.5f;

        life_Mon3 = 3;


        anim_Speed = 1.0f;
    }

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void Player_Devil(bool isDevil)
    {
        isPlayer_Devil = isDevil;
    }

    public void is_PlayerBim()   // 플레이어가 빔을 쐇나?
    {
        bim_Count = 0;
        bim_Button.fillAmount = 0;
        bim_Button.color = new Color(bim_Button.color.r, bim_Button.color.g, bim_Button.color.b, 0.25f);
        bim_Bool = false;
    }

    public void Kill_Num()      // 킬 카운트를 보여주는 함수
    {
        kill_Int++;
        text1.text = kill_Int.ToString();   // 


        if (bim_Count < 100)
        {
            bim_Count++;
            bim_Button.fillAmount += 0.01f;

            if (bim_Count.Equals(50))
            {
                bim_Bool = true;
                bim_Button.color = new Color(bim_Button.color.r, bim_Button.color.g, bim_Button.color.b, 0.4f);
            }

            if (bim_Count.Equals(100))
            {
                bim_Bool = true;
                bim_Button.color = new Color(bim_Button.color.r, bim_Button.color.g, bim_Button.color.b, 0.6f);
            }
        }
        

        if (isPlayer_Devil.Equals(false))    // 기본 모드라면
        {
            text1.color = Color.red;            // 흰색 테두리에 빨간색 글자
            text1.outlineColor = Color.white;
        }
        else         // 데빌 모드라면
        {
            text1.color = Color.black;            // 보라색 테두리에 검정색 글자
            text1.outlineColor = color_Devil;
        }

        text_Anim.gameObject.SetActive(true);    // 킬 텍스트 활성화
        text_Anim.SetTrigger("triggerKill");     // 킬 텍스트 애니메이션 실행

        
        int step = level / 5;           // 5레벨씩 한 단계
        switch (step)
        {
            case 0:         // 1 ~ 4
                if (kill_Int.Equals(10))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 1:         // 5 ~ 9
                if (kill_Int.Equals(20))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 2:         // 10 ~ 14
                if (kill_Int.Equals(30))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 3:         // 15 ~ 19
                if (kill_Int.Equals(40))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 4:         // 20 ~ 24
                if (kill_Int.Equals(50))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 5:         // 25 ~ 29
                if (kill_Int.Equals(60))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 6:         // 30 ~ 34
                if (kill_Int.Equals(70))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 7:         // 35 ~ 39
                if (kill_Int.Equals(80))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 8:         // 40 ~ 44
                if (kill_Int.Equals(90))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 9:         // 45 ~ 49
                if (kill_Int.Equals(100))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 10:         // 50 ~ 54
                if (kill_Int.Equals(110))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 11:         // 55 ~ 59
                if (kill_Int.Equals(120))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 12:         // 60 ~ 64
                if (kill_Int.Equals(130))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 13:         // 65 ~ 69
                if (kill_Int.Equals(140))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 14:         // 70 ~ 74
                if (kill_Int.Equals(150))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 15:         // 75 ~ 79
                if (kill_Int.Equals(160))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 16:         // 80 ~ 84
                if (kill_Int.Equals(170))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 17:         // 85 ~ 89
                if (kill_Int.Equals(180))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 18:         // 90 ~ 94
                if (kill_Int.Equals(190))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            case 19:         // 95 ~ 99
                if (kill_Int.Equals(200))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
            default:         // 100 ~ 
                if (kill_Int.Equals(999))
                {
                    kill_Int = 0;
                    Level_Up();
                }
                break;
        }
    }

    Vector3 move_Ui = new Vector3(10, -10, 0);             // ui를 조금 움직이게 하는 위치
    Vector3 ori_Ui;                                        // ui의 원래 위치
    Color move_Color = new Color(67/255f, 160/255f, 52/255f);   // ui를 누를 떄 화살표 색깔
    Color ori_Color = new Color(1, 1, 1);                       // 화살표 원래 색깔(흰색)
    // 67 160 52

    public void ClickDown_Item(int num)      // ui를 누를때 처리 함수(0:총, 1:방망이, 2:레이저)
    {
        ori_Ui = UI_Item[num].localPosition;
        UI_Item[num].localPosition += move_Ui;
        UI_Item[num].GetComponent<Shadow>().enabled = false;
        UI_Item[num].GetChild(0).GetComponent<Image>().color = move_Color;

        AudioManager.ins.PlayEffect("Click_01");
    }

    public void ClickUp_Item(int num)
    {
        UI_Item[num].localPosition = ori_Ui;
        UI_Item[num].GetComponent<Shadow>().enabled = true;
        UI_Item[num].GetChild(0).GetComponent<Image>().color = ori_Color;
        count_Item[num]++;
        DamageUp(num);
        UI_Panel.SetActive(false);
        particle.Stop();
        player.isLevelUp = false;
        Time.timeScale = 1f;
    }
    

    int[] count_Item = new int[3] { 0, 0, 0 };  // 업그레이할 떄 마다 카운팅 되고 최대를 막기위한 인트

    public void Upgrade_Func()   // 레벨업 할 때마다 업그레이드를 묻는 함수
    {
        if (count_Item[0].Equals(100))
        {
            UI_Item[0].gameObject.SetActive(false);
        }

        if (count_Item[1].Equals(50))
        {
            UI_Item[1].gameObject.SetActive(false);
        }

        if (count_Item[2].Equals(30))
        {
            UI_Item[2].gameObject.SetActive(false);
        }


    }

    public void DamageUp(int num)
    {
        if (num.Equals(0))
        {
            demage_Bullet_01 += 0.02f;
            demage_Bullet_02 = demage_Bullet_01 + 0.2f;
        }
        else if (num.Equals(1))
        {
            demage_Bat_01 += 0.04f;
            demage_Bat_02 = demage_Bat_01 + 2.0f;
        }
        else
        {
            demage_Bim_01 += 0.2f;
            demage_Bim_02 = demage_Bim_01 * 2;
        }
    }

  

    public void SpeedUp_Mon1()
    {
        speed_Mon1 += 0.1f;
    }

    public void SpeedUp_Mon2()
    {
        speed_Mon1 += 0.3f;
    }

    public void SpeedUp_Mon3()
    {
        speed_Mon1 += 0.5f;
    }

    public void Level_Up()
    {
        level++;
        level_Count.text = level.ToString();
        player.isLevelUp = true;
        UI_Panel.SetActive(true);
        particle.Play();
        AudioManager.ins.PlayEffect("LevelUp");
        Time.timeScale = 0f;
        Upgrade_Func();            // 업그레이드를 묻는 함수
        spawn_Script.LevelUp();
    }

    public void GameEnd()
    {
        AudioManager.ins.PlayEffect("GameOver");
        AudioManager.ins.Stop_BG();
        Time.timeScale = 0f;
        GameOver_Panel.SetActive(true);
    }

    public void Restart_Down()
    {
        ori_Ui = Re_Button.transform.localPosition;
        Re_Button.transform.localPosition += move_Ui;
        Re_Button.GetComponent<Shadow>().enabled = false;

        AudioManager.ins.PlayEffect("Click_01");
    }
    public void Restart_Up()
    {
        Re_Button.transform.localPosition = ori_Ui;
        Re_Button.GetComponent<Shadow>().enabled = true;


        GameOver_Panel.SetActive(false);

        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    public void End_Game_Down()
    {
        ori_Ui = End_Button.transform.localPosition;
        End_Button.transform.localPosition += move_Ui;
        End_Button.GetComponent<Shadow>().enabled = false;

        AudioManager.ins.PlayEffect("Click_01");
        
    }
    public void End_Game_Up()
    {
        End_Button.transform.localPosition = ori_Ui;
        End_Button.GetComponent<Shadow>().enabled = true;
        
        
        GameOver_Panel.SetActive(false);
        
        Time.timeScale = 1f;
        Application.Quit();
    }

    
    public void Pause_First()
    {
        if (player.isLevelUp.Equals(true) || player.isGameOver.Equals(true))
        {
            return;
        }


        Pause_Panel.SetActive(true);
        AudioManager.ins.PlayEffect("Click_02");
        player.isPause = true;
        Time.timeScale = 0f;
    }


    public void Pause_Restart_Down()
    {
        ori_Ui = Pause_Panel.transform.GetChild(0).localPosition;
        Pause_Panel.transform.GetChild(0).localPosition += move_Ui;
        Re_Button.GetComponent<Shadow>().enabled = false;

        AudioManager.ins.PlayEffect("Click_01");
    }

    public void Pause_Restart_Up()
    {
        Pause_Panel.transform.GetChild(0).localPosition = ori_Ui;
        Pause_Panel.transform.GetChild(0).GetComponent<Shadow>().enabled = true;


        Pause_Panel.SetActive(false);

        player.isPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Pause_End_Game_Down()
    {
        ori_Ui = Pause_Panel.transform.GetChild(1).localPosition;
        Pause_Panel.transform.GetChild(1).localPosition += move_Ui;
        Re_Button.GetComponent<Shadow>().enabled = false;

        AudioManager.ins.PlayEffect("Click_01");
    }

    public void Pause_End_Game_Up()
    {
        Pause_Panel.transform.GetChild(1).localPosition = ori_Ui;
        Pause_Panel.transform.GetChild(1).GetComponent<Shadow>().enabled = true;


        Pause_Panel.SetActive(false);

        player.isPause = false;
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void Pause_Play_Down()
    {
        ori_Ui = Pause_Panel.transform.GetChild(2).localPosition;
        Pause_Panel.transform.GetChild(2).localPosition += move_Ui;
        Re_Button.GetComponent<Shadow>().enabled = false;

        AudioManager.ins.PlayEffect("Click_01");
    }

    public void Pause_Play_Up()
    {
        Pause_Panel.transform.GetChild(2).localPosition = ori_Ui;
        Pause_Panel.transform.GetChild(2).GetComponent<Shadow>().enabled = true;

        player.isPause = false;
        Pause_Panel.SetActive(false);

        Time.timeScale = 1f;
    }


    
}
