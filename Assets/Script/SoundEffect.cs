using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource audioSource;

    // 클래스 객체를 static으로 만들어서 해당 스크립트 함수를 사용할 수 있도록
    public static SoundEffect _soundEffect;

    // 효과음 온/오프 여부
    public bool soundToggle = true;

    // 효과음 오디오 클립
    public AudioClip boilingWaterBgm;
    public AudioClip eggBgm;
    public AudioClip ramenBgm;
    public AudioClip waterBgm;
    public AudioClip soupBgm;
    public AudioClip leekBgm;
    public AudioClip moneyBgm;
    public AudioClip successPotBgm;
    public AudioClip failPotBgm;
    public AudioClip potBgm;
    public AudioClip eatRamenBgm;

    // Start is called before the first frame update
    void Awake()
    {
        _soundEffect = this;
    }

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>(); //GameManager 오브젝트
    }


    public void boilingWaterAudio()
    {
        audioSource.clip = boilingWaterBgm;
        if (soundToggle) //브금 켜져있다면
        {
            audioSource.Play();
        }
    }

    public void eggAudio()
    {
        audioSource.clip = eggBgm;
        if (soundToggle) //브금 켜져있다면
        {
            audioSource.Play();
        }
    }

    public void ramenAudio()
    {
        audioSource.clip = ramenBgm;
        if (soundToggle) //브금 켜져있다면
        {
            audioSource.Play();
        }
    }

    public void waterAudio()
    {
        audioSource.clip = waterBgm;
        if (soundToggle) //브금 켜져있다면
        {
            audioSource.Play();
        }
    }

    public void soupAudio()
    {
        audioSource.clip = soupBgm;
        if (soundToggle) //브금 켜져있다면
        {
            audioSource.Play();
        }
    }
    public void leekAudio()
    {
        audioSource.clip = leekBgm;
        if (soundToggle) //브금 켜져있다면
        {
            audioSource.Play();
        }
    }
    public void moneyAudio()
    {
        audioSource.clip = moneyBgm;
        if (soundToggle) //브금 켜져있다면
        {
            audioSource.Play();
        }
    }

    public void successPotAudio()
    {
        audioSource.clip = successPotBgm;
        if (soundToggle) //브금 켜져있다면
        {
            audioSource.Play();
        }
    }
    public void failPotAudio()
    {
        audioSource.clip = failPotBgm;
        if (soundToggle) //브금 켜져있다면
        {
            audioSource.Play();
        }
    }

    public void eatRamenAudio()
    {
        audioSource.clip = eatRamenBgm;
        if (soundToggle) //브금 켜져있다면
        {
            audioSource.Play();
        }
    }

    public void potAudio()
    {
        audioSource.clip = potBgm;
        if (soundToggle) //브금 켜져있다면
        {
            audioSource.Play();
        }
    }


}
