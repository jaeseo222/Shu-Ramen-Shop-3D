using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPotMoveTo : MonoBehaviour
{
    private Transform ingredient;
    private Vector3 originPos;//원래 위치
    private bool isComp;

    private bool isStatic = true; // 냄비가 붙어 있는지 여부

    public float time = 0f; // 시간 (물 넢은 후부터 카운트)
    public bool isStart = false; // 물 채워진 걸로 시작 여부

    // 물 끓는 소리
    private AudioSource audioSource;
    public AudioClip boilingBgm;

    // 탄 시간 기준
    private const float BURN_TIME = 15f;

    private void Start()
    {
        ingredient = transform;
        originPos = GetComponent<Rigidbody>().position;//원래위치 저장해놓기
        isComp = false;

        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    private void OnMouseUp()
    {
        if (!isComp)
            MoveOriginPos();
    }

    private void OnMouseDown()
    {
        // 마우스 드래그 시작되므로 false로 변경
        isStatic = false;
    }

    private void Update()
    {
        // 물 채워졌고, 냄비가 붙어있다면 시간 세기
        if (isStart && isStatic)
        {
            if (time == 0f)
            {
                Debug.Log("초 세기 시작");
            }
            time += Time.deltaTime;
        }

        if (time == 0f)
        {
            audioSource.Stop();
        }
        if (time > 5f && !audioSource.isPlaying)
        {
            audioSource.clip = boilingBgm;
            audioSource.Play();
        }

        // 탄 냄비 보여주기
        if (time > BURN_TIME)
        {
            this.gameObject.SetActive(false);
            int childNum = transform.GetSiblingIndex();
            GameObject.Find("BlackPots").transform.GetChild(childNum).gameObject.SetActive(true);
        }
    }

    private void MoveOriginPos()
    {
        transform.position = originPos;
        isComp = false;

        // 원점으로 돌아오면서 냄비가 가만히 있으므로 true로 변경
        isStatic = true;

        // 탄 냄비라면 -> 기존 냄비로 바뀌게
        burnPotManager();
    }

    private void burnPotManager()
    {
        // 탄 냄비라면
        if (transform.parent.name == "BlackPots")
        {
            // 탄 냄비 비활성화
            this.gameObject.SetActive(false);
            // 자식 번호 알아내기
            int childNum = transform.GetSiblingIndex();
            GameObject.Find("Pots").transform.GetChild(childNum).gameObject.SetActive(true);
            GameObject.Find("Pots").transform.GetChild(childNum).gameObject.GetComponent<TutorialPotMoveTo>().formatPot();
        }
    }

    private void formatPot()
    {
        // 시간 및 판정 변수 초기화
        time = 0f;
        isStart = false;

        if (transform.parent.name == "BlackPots")
        {
            return;
        }

        // 냄비 속 초기화
        transform.Find("water").gameObject.SetActive(false);
    }
}