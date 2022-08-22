using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EffectButton : MonoBehaviour
{
    public GameObject buttonNoEffect; //효과 전 버튼 이미지
    public GameObject buttonEffect; //호버, 눌렀을 때 버튼 이미지

    public void OnPointerExit()
    {
        // 효과 전
        buttonNoEffect.SetActive(true);
        buttonEffect.SetActive(false);
    }
    public void OnPointerEnter()
    {
        // 호버 시
        buttonNoEffect.SetActive(false);
        buttonEffect.SetActive(true);
    }
}
