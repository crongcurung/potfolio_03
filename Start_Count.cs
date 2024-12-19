using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Start_Count : MonoBehaviour
{
    AudioSource audios;
    public AudioClip[] audio_Arr;
    public TextMeshProUGUI start_Count;

    void Awake()
    {
        Time.timeScale = 0;
        audios = GetComponent<AudioSource>();
    }


    public void Start_Anim(int num)
    {


        if (num.Equals(10))   // 마지막
        {
            Time.timeScale = 1;

            transform.gameObject.SetActive(false);
        }
        else if (num.Equals(0))
        {
            start_Count.text = "시작!";

            audios.clip = audio_Arr[3];
            audios.Play();
            return;
        }
        else if (num.Equals(1))   // 
        {
            audios.clip = audio_Arr[2];
            audios.Play();
        }
        else if (num.Equals(2))   // 
        {
            audios.clip = audio_Arr[1];
            audios.Play();
        }
        else    // 3
        {
            audios.clip = audio_Arr[0];
            audios.Play();
        }

        start_Count.text = num.ToString();
    }

}
