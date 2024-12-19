using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject bullet_prefab;
    public GameObject bullet_prefab_02;
    public GameObject mon_01_prefab;
    public GameObject mon_02_prefab;
    public GameObject mon_03_prefab;
    public GameObject mon_03_Shot_prefab;
    public GameObject bullet_Hit_prefab;
    public GameObject bullet_Hit_prefab_02;
    public GameObject bullet_Case;
    Queue<GameObject> queue_Bullet = new Queue<GameObject>();     // �⺻ �Ѿ� ������Ʈ Ǯ��
    Queue<GameObject> queue_Bullet_02 = new Queue<GameObject>();     // ���� �Ѿ� ������Ʈ Ǯ��
    Queue<GameObject> queue_Mon_01 = new Queue<GameObject>();     // �⺻ ���� ������Ʈ Ǯ��
    Queue<GameObject> queue_Mon_02 = new Queue<GameObject>();     // ��� ���� ������Ʈ Ǯ��
    Queue<GameObject> queue_Mon_03 = new Queue<GameObject>();     // ���Ÿ� ���� ������Ʈ Ǯ��
    Queue<GameObject> queue_Mon_03_Shot = new Queue<GameObject>();     // ���Ÿ� ���� ������Ʈ Ǯ��
    Queue<GameObject> queue_Bullet_Hit = new Queue<GameObject>();     //  ���� �Ѿ� ��Ʈ ������Ʈ Ǯ��
    Queue<GameObject> queue_Bullet_Hit_02 = new Queue<GameObject>();     //  ���� ���� �Ѿ� ��Ʈ ������Ʈ Ǯ��
    Queue<GameObject> queue_Bullet_Case = new Queue<GameObject>();     //  ���� ���� �Ѿ� ��Ʈ ������Ʈ Ǯ��

    public GameObject Item_Devil;
    public GameObject Item_Carrot;


    void Awake()
    {
        GameObject p_object;


        for (int i = 0; i < 50; i++)       // �⺻ �Ѿ� 50���� ���� �� ����
        {

            p_object = Instantiate(bullet_prefab);  // �Ѿ� ����
            p_object.GetComponent<Bullet>().spawn_Script = this;
            queue_Bullet.Enqueue(p_object);
        }

        for (int i = 0; i < 100; i++)       // ���� �Ѿ� 50���� ���� �� ����
        {

            p_object = Instantiate(bullet_prefab_02);  // �Ѿ� ����
            p_object.GetComponent<Bullet_02>().spawn_Script = this;
            queue_Bullet_02.Enqueue(p_object);
        }

        for (int i = 0; i < 50; i++)
        {
            p_object = Instantiate(bullet_Hit_prefab);  // �Ѿ� ��Ʈ ����
            p_object.GetComponent<Bullet_Hit>().spawn_Script = this;
            queue_Bullet_Hit.Enqueue(p_object);
        }

        for (int i = 0; i < 50; i++)
        {
            p_object = Instantiate(bullet_Hit_prefab_02);  // ���� �Ѿ� ��Ʈ ����
            p_object.GetComponent<Bullet_Hit_02>().spawn_Script = this;
            queue_Bullet_Hit_02.Enqueue(p_object);
        }

        for (int i = 0; i < 50; i++)
        {
            p_object = Instantiate(bullet_Case);  // ź�� ����
            p_object.GetComponent<Bullet_Case>().spawn_Script = this;
            queue_Bullet_Case.Enqueue(p_object);
        }

        for (int i = 0; i < 30; i++)       // �⺻ ���� 100���� ���� �� ����
        {
            p_object = Instantiate(mon_01_prefab);  // �⺻ ���� ����
            p_object.GetComponent<Mon_01>().spawn_Script = this;
            queue_Mon_01.Enqueue(p_object);       // ������Ʈ Ǯ��
        }

        for (int i = 0; i < 30; i++)       // ��� ���� 100���� ���� �� ����
        {
            p_object = Instantiate(mon_02_prefab);  // ��� ���� ����
            p_object.GetComponent<Mon_02>().spawn_Script = this;
            queue_Mon_02.Enqueue(p_object);       // ������Ʈ Ǯ��
        }

        for (int i = 0; i < 30; i++)       // ���Ÿ� ���� 100���� ���� �� ����
        {
            p_object = Instantiate(mon_03_prefab);  // ��� ���� ����
            p_object.GetComponent<Mon_03>().spawn_Script = this;
            queue_Mon_03.Enqueue(p_object);       // ������Ʈ Ǯ��
        }

        for (int i = 0; i < 30; i++)       // ���Ÿ� ���� 100���� ���� �� ����
        {
            p_object = Instantiate(mon_03_Shot_prefab);  // ��� ���� ����
            p_object.GetComponent<Mon_03_Shot>().spawn_Script = this;
            queue_Mon_03_Shot.Enqueue(p_object);       // ������Ʈ Ǯ��
        }

    }


    public void InsertQueue_Bullet(GameObject p_object)     // ����� ��ü�� ť�� �ٽ� �ݳ���Ű�� �Լ�(�⺻ �Ѿ�)
    {
        queue_Bullet.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue_Bullet()           // ť���� ��ü�� �������� �Լ�(�⺻ �Ѿ�)
    {
        GameObject t_object = queue_Bullet.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }

    ///////////////////////////////////////////////////////////


    public void InsertQueue_Bullet_02(GameObject p_object)     // ����� ��ü�� ť�� �ٽ� �ݳ���Ű�� �Լ�(���� �Ѿ�)
    {
        queue_Bullet_02.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue_Bullet_02()           // ť���� ��ü�� �������� �Լ�(���� �Ѿ�)
    {
        GameObject t_object = queue_Bullet_02.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }

    ///////////////////////////////////////////////////////////


    public void InsertQueue_Bullet_Hit(GameObject p_object)     // ����� ��ü�� ť�� �ٽ� �ݳ���Ű�� �Լ�(�Ѿ�)
    {
        queue_Bullet_Hit.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue_Bullet_Hit()           // ť���� ��ü�� �������� �Լ�(�Ѿ�)
    {
        GameObject t_object = queue_Bullet_Hit.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }

    ///////////////////////////////////////////////////////////

    public void InsertQueue_Bullet_Hit_02(GameObject p_object)     // ����� ��ü�� ť�� �ٽ� �ݳ���Ű�� �Լ�(�Ѿ�)
    {
        queue_Bullet_Hit_02.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue_Bullet_Hit_02()           // ť���� ��ü�� �������� �Լ�(�Ѿ�)
    {
        GameObject t_object = queue_Bullet_Hit_02.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }

    ///////////////////////////////////////////////////////////


    public void InsertQueue_Bullet_Case(GameObject p_object)     // ����� ��ü�� ť�� �ٽ� �ݳ���Ű�� �Լ�(ź��)
    {
        queue_Bullet_Case.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue_Bullet_Case()           // ť���� ��ü�� �������� �Լ�(ź��)
    {
        GameObject t_object = queue_Bullet_Case.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }

    ///////////////////////////////////////////////////////////


    public void InsertQueue_Mon_01(GameObject p_object)     // ����� ��ü�� ť�� �ٽ� �ݳ���Ű�� �Լ�(�⺻ ����)
    {
        queue_Mon_01.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue_Mon_01()           // ť���� ��ü�� �������� �Լ�(�⺻ ����)
    {
        GameObject t_object = queue_Mon_01.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }

    ///////////////////////////////////////////////////////////

    public void InsertQueue_Mon_02(GameObject p_object)     // ����� ��ü�� ť�� �ٽ� �ݳ���Ű�� �Լ�(��� ����)
    {
        queue_Mon_02.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue_Mon_02()           // ť���� ��ü�� �������� �Լ�(��� ����)
    {
        GameObject t_object = queue_Mon_02.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }


    ///////////////////////////////////////////////////////////

    public void InsertQueue_Mon_03(GameObject p_object)     // ����� ��ü�� ť�� �ٽ� �ݳ���Ű�� �Լ�(���Ÿ� ����)
    {
        queue_Mon_03.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue_Mon_03()           // ť���� ��ü�� �������� �Լ�(���Ÿ� ����)
    {
        GameObject t_object = queue_Mon_03.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }


    ///////////////////////////////////////////////////////////

    public void InsertQueue_Mon_03_Shot(GameObject p_object)     // ����� ��ü�� ť�� �ٽ� �ݳ���Ű�� �Լ�(���Ÿ� ���� �߻�)
    {
        queue_Mon_03_Shot.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue_Mon_03_Shot()           // ť���� ��ü�� �������� �Լ�(���Ÿ� ���� �߻�)
    {
        GameObject t_object = queue_Mon_03_Shot.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }


    // 11.5, 4
    // 11.5, 2
    // 11.5, 0
    // 11.5, -2
    // 11.5, -4


    void Spawn_Mon_01()
    {
        int rand = Random.Range(0, 5);

        GameObject mon_01 = GetQueue_Mon_01();
        mon_01.transform.position = new Vector3(11.5f, 2 * rand - 4, -2); 
    }

    void Spawn_Mon_02()
    {
        int rand = Random.Range(0, 5);

        GameObject mon_02 = GetQueue_Mon_02();
        mon_02.transform.position = new Vector3(11.5f, 2 * rand - 4, -2);
    }

    void Spawn_Mon_03()
    {
        int rand = Random.Range(0, 5);

        GameObject mon_03 = GetQueue_Mon_03();
        mon_03.transform.position = new Vector3(11.5f, 2 * rand - 4, -2);
    }

    void Spawn_Devil()
    {
        int rand = Random.Range(0, 5);

        Item_Devil.transform.position = new Vector3(11.5f, 2 * rand - 4, -2);
        Item_Devil.SetActive(true);
    }

    void Spawn_Carrot()
    {
        int rand = Random.Range(0, 5);

        Item_Carrot.transform.position = new Vector3(11.5f, 2 * rand - 4, -2);
        Item_Carrot.SetActive(true);
    }

    int level = 1;
    float timer = 0;
    float time = 1.5f;
    // ti


    void Update()
    {

        timer += Time.deltaTime;
        if (timer > time)
        {
            Spawn_Func();   // ���� ����
            timer = 0;

            //Spawn_Mon_01();
            //Spawn_Mon_02();
            //Spawn_Mon_03();

            //Spawn_Devil();
            //Spawn_Carrot();
        }
    }


    public void LevelUp()  // �������� ���� ���� �ð� ���� (10����)
    {
        level++;
        int int_Level = level / 10;
        bool bool_Item = level % 5 == 0;

        switch (int_Level)
        {
            case 0:
                if (bool_Item)  // 15 ����
                {
                    Spawn_Item();
                }
                break;
            case 1:
                time = 1.4f;
                
                if (bool_Item)  // 15 ����
                {
                    Spawn_Item();
                    GameManager.Instance.SpeedUp_Mon1();
                }
                break;
            case 2:
                time = 1.3f;

                if (bool_Item)
                {
                    Spawn_Item();
                    GameManager.Instance.SpeedUp_Mon1();
                }
                break;
            case 3:
                time = 1.2f;

                if (bool_Item)
                {
                    Spawn_Item();
                    GameManager.Instance.SpeedUp_Mon1();
                }
                break;
            case 4:
                time = 1.1f;

                if (bool_Item)
                {
                    Spawn_Item();
                    GameManager.Instance.SpeedUp_Mon1();
                }
                break;
            case 5:
                time = 1.0f;

                if (bool_Item)
                {
                    Spawn_Item();
                    GameManager.Instance.SpeedUp_Mon1();
                    GameManager.Instance.SpeedUp_Mon2();
                }
                break;
            case 6:
                time = 0.9f;

                if (bool_Item)
                {
                    Spawn_Item();
                    GameManager.Instance.SpeedUp_Mon1();
                    GameManager.Instance.SpeedUp_Mon2();
                }
                break;
            case 7:
                time = 0.8f;

                if (bool_Item)
                {
                    Spawn_Item();
                    GameManager.Instance.SpeedUp_Mon1();
                    GameManager.Instance.SpeedUp_Mon2();
                }
                break;
            case 8:
                time = 0.7f;

                if (bool_Item)
                {
                    Spawn_Item();
                    GameManager.Instance.SpeedUp_Mon1();
                    GameManager.Instance.SpeedUp_Mon2();
                    GameManager.Instance.SpeedUp_Mon3();
                }
                break;
            case 9:
                time = 0.6f;

                if (bool_Item)
                {
                    Spawn_Item();
                    GameManager.Instance.SpeedUp_Mon1();
                    GameManager.Instance.SpeedUp_Mon2();
                    GameManager.Instance.SpeedUp_Mon3();
                }
                break;
            default:
                time = 0.5f;

                if (bool_Item)
                {
                    Spawn_Item();
                }
                break;
        }
    }

    void Spawn_Item()
    {
        int rand_Item = Random.Range(0, 3);

        if (rand_Item > 1)
        {
            Spawn_Devil();
        }
        else
        {
            Spawn_Carrot();

        }
    }

    // ������ 99����?
    void Spawn_Func()
    {
        int step = level / 5;// 5������ �� �ܰ�
        int rand = Random.Range(0, 100);
        
        switch (step)
        {
            case 0:         // 1 ~ 4
                if (rand > 10)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 1)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }

                break;
            case 1:         // 5 ~ 9
                if (rand > 15)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 5)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 2:         // 10 ~ 14
                if (rand > 20)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 5)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 3:         // 15 ~ 19
                if (rand > 25)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 10)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 4:         // 20 ~ 24
                if (rand > 30)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 10)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 5:         // 25 ~ 29
                if (rand > 35)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 15)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 6:         // 30 ~ 34
                if (rand > 40)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 15)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 7:         // 35 ~ 39
                if (rand > 45)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 20)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 8:         // 40 ~ 44
                if (rand > 50)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 20)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 9:         // 45 ~ 49
                if (rand > 55)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 25)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 10:         // 50 ~ 54
                if (rand > 60)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 25)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 11:         // 55 ~ 59
                if (rand > 65)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 30)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 12:         // 60 ~ 64
                if (rand > 70)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 30)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 13:         // 65 ~ 69
                if (rand > 75)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 35)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 14:         // 70 ~ 74
                if (rand > 80)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 35)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 15:         // 75 ~ 79
                if (rand > 85)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 36)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 16:         // 80 ~ 84
                if (rand > 90)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 37)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 17:         // 85 ~ 89
                if (rand > 91)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 38)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 18:         // 90 ~ 94
                if (rand > 92)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 39)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            case 19:         // 95 ~ 99
                if (rand > 93)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 40)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
            default:         // 100 ~ 
                if (rand > 95)     // �⺻ ����
                {
                    Spawn_Mon_01();
                }
                else if (rand > 40)          // ��� ����
                {
                    Spawn_Mon_02();
                }
                else               // ���Ÿ� ����
                {
                    Spawn_Mon_03();
                }
                break;
        }
    }

    void Start()
    {
        //Spawn_Carrot();
        //Spawn_Devil();
        //Spawn_Mon_03();
    }

}
