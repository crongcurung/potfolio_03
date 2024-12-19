using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour       // ��ŸƮ ������ ����
{
    [SerializeField] AudioSource effectAudio;       // �� ��ũ��Ʈ�� �پ��ִ� ������Ʈ�� �پ��ִ� ����� �ҽ�(ȿ������)
    [SerializeField] AudioSource BG_Audio;       // �� ��ũ��Ʈ�� �پ��ִ� ������Ʈ�� �پ��ִ� ����� �ҽ�(������ǿ�)

    Dictionary<string, AudioClip> effects;    // ��ųʸ��� Ŭ�� �Ǵ�
    Dictionary<string, AudioClip> BGs;

    WaitForSeconds wait_Audio = new WaitForSeconds(1f);    // ź�� ���� �ð��� 1��

    private static AudioManager instance;       // �̱��� �۾�
    public static AudioManager ins
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


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }


        LoadFile(ref effects, "Sound/Effect/");       // ��ųʸ��� ȿ������ �����Ѵ�.
        LoadFile(ref BGs, "Sound/BackGround/");       // ��ųʸ��� ��������� �����Ѵ�.
    }


    void Start()
    {


        StartCoroutine(Playlist());
    }


    public void LoadFile<T>(ref Dictionary<string, T> a, string path) where T : Object       // ��ųʸ��� ������(����) ����
    {
        string log = path + "���� �ҷ��� ���� \n";
        a = new Dictionary<string, T>();
        T[] particleSystems = Resources.LoadAll<T>(path);
        foreach (var particle in particleSystems)
        {
            log += particle.name + "\n";
            a.Add(particle.name, particle);
        }

    }



    //////////////////////// ȿ����
    public void PlayEffect(string name)         // ȿ���� �Ҹ� ����
    {
        effectAudio.clip = effects[name];       // ��ųʸ��� ����� ȿ������ Ŭ������ �ѱ��W
        effectAudio.Play();                     // �Ҹ� ����
    }
    public void StopEffect() { effectAudio.Stop(); }       // ȿ���� �Ҹ� ���߱�
    public void LoopEffect(bool loopBool) { effectAudio.loop = loopBool; }       // ȿ���� ���ѷ���
    


    //0~1
    public void SetEffectVolume(float scale) { effectAudio.volume = scale; }     // ȿ���� �Ҹ� ����
    public float GetEffectVolume() { return effectAudio.volume; }                // ȿ���� �Ҹ� ��������






    //////////////////////// ��� ����
    public void Play_BG(string name)         // ������� �Ҹ�����
    {
        BG_Audio.clip = BGs[name];       // ��ųʸ��� ����� ��������� Ŭ������ �ѱ��
        BG_Audio.Play();                     // �Ҹ� ����
    }
    public void Stop_BG() { BG_Audio.Stop(); }       // ȿ���� �Ҹ� ���߱�
    public void Pause_BG() { BG_Audio.Pause(); }         // ������� ���߱�
    public void UnPause_BG() { BG_Audio.UnPause(); }     // ���� ������� Ǯ��

    public void PitchUp_BG() { BG_Audio.pitch = 1.3f; }
    public void PitchDown_BG() { BG_Audio.pitch = 1.0f; }

    //0~1
    //public void SetBackGroundVolume(float scale) { BG_Audio.volume = scale; }    // ������� �Ҹ� ����
    //public float GetBackGroundVolume() { return BG_Audio.volume; }                // ������� �Ҹ� ��������

    List<int> BG_Int = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

    IEnumerator Playlist()        // ������� ���� �ڷ�ƾ(���� ����)
    {
        int rand = Random.Range(0, BG_Int.Count);
        

        Play_BG(BG_Int[rand].ToString());
        BG_Int.RemoveAt(rand);


        while (true) 
        { 
            yield return wait_Audio;      // 1�ʸ��� ��������� ����Ǵ��� Ȯ��
            if (!BG_Audio.isPlaying)                    // ���� ������ ���� �ʴ´ٸ� �ٸ� ����������� ����
            {
                rand = Random.Range(0, BG_Int.Count);

                Play_BG(BG_Int[rand].ToString());
                BG_Int.RemoveAt(rand);

                if (BG_Int.Count.Equals(0))
                {
                    BG_Int = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                }
                //soundSource.loop = true; 
            } 
        } 
    }
    
}
