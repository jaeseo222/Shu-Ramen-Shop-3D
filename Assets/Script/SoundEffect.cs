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
}
