using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotMoveTo : MonoBehaviour
{
    private Transform ingredient;
    private Vector3 originPos;//원래 위치
    private bool isComp;

    private bool isStatic = true; // 냄비가 붙어 있는지 여부

    public float time = 0f; // 시간 (물 넢은 후부터 카운트)
    public bool isStart = false; // 물 채워진 걸로 시작 여부
    public bool eggAfterFive = false; // 물 끓고 계란 넣었는지 여부
    public bool leekAfterFive = false; // 물 끓고 파 넣었는지 여부

    private int money = 0; // 라면 1개에 번 돈
    private string customerTalking = ""; // 손님 말

    // 손님 말 종류
    private const string BURN = "탄 걸 어떻게 먹어? 너나 먹어!";
    private const string FAIL = "지금 장난해?";
    private const string EMPTY_RAW = "먼가가 허전하고 안 익었짜나!";
    private const string SALTY_EMPTY = "켁, 넘 짜! 그리고 먼가 허전해!";
    private const string SIMPLE = "라면이 너무 심플한거 아니야~?";
    private const string RAW = "안 익었짜나!";
    private const string NO_SOUL_EMPTY = "정성도 없고 먼가 허전하군...";
    private const string SALTY = "켁, 넘 짜!";
    private const string NO_SOUL = "맛에 정성이 없어!";
    private const string VERY_NICE = "아주 좋아~";

    // 돈
    private readonly int[] MONEY = { 300, 400, 500, 600, 800, 1000 };

    // 손님 말풍선
    private GameObject customerObject; //텍스트
    private GameObject customerChatObj; //배경 말풍선

    // 물 끓는 소리
    private AudioSource audioSource;
    public AudioClip boilingBgm;

    // 탄 시간 기준
    private const float BURN_TIME = 15f;

    // 오브젝트 자식
    private Transform[] allChildren;

    private void Start()
    {
        ingredient = transform;
        originPos = GetComponent<Rigidbody>().position;//원래위치 저장해놓기
        isComp = false;

        customerChatObj = GameObject.Find("Canvas").transform.Find("CustomerChat").gameObject;
        customerObject = customerChatObj.transform.Find("Text").gameObject;

        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {    // other.bounds : 콜라이더의 중심 좌표와 콜라이더의 사이즈의 반
         // other.center : 콜라이더의 중심 좌표
         // other.extents : 콜라이더 사이즈의 반

        // 마우스 뗐을 때
        // 내가 드래그 하고 있는 오브젝트의 콜라이더의 센터가 충돌 오브젝트의 콜라이더 내에 있는지 체크
        if (Input.GetMouseButtonUp(0))
        {
            if (other.gameObject.name != "comp")
            {
                return;
            }
            if ((other.bounds.Contains(transform.GetComponent<Collider>().bounds.center)))
            {
                Debug.Log("부딪힘!");
                isComp = true;
                MoveToComp();
                Invoke("MoveOriginPos", 1.0f);
                Invoke("burnPotManager", 1.0f);
                Invoke("formatPot", 1.0f);
            }
        }
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

    private void MoveToComp()
    {
        transform.position = new Vector3(9.56f, -2.67f, -1.26f);

        // 판정
        money = getMoney(time, eggAfterFive, leekAfterFive);

        if (money > 0)
        {
            // 돈 올라가는 효과음
            SoundEffect._soundEffect.moneyAudio();
        }

        // 판정 결과 출력
        // Debug.Log("번 돈: " + money + ", 멘트: " + customerTalking);

        // 전체 돈에 추가
        TotalMoney.totalMoney += money;

        // 판정 결과 말풍선에 띄우기
        customerChatObj.SetActive(true);
        customerObject.GetComponent<Text>().text = customerTalking;

        Invoke("deleteChat", 0.7f);

        //애니메이션 추가
    }

    private void deleteChat()
    {
        customerChatObj.SetActive(false);
    }

    private string mergeCustomerTalking(string ment, int money)
    {
        return ment + " " + money.ToString() + "원 주지~";
    }

    private int getMoney(float endTime, bool eggAfterFive, bool leekAfterFive)
    {
        // 탄 냄비라면
        if (transform.parent.name == "BlackPots")
        {
            customerTalking = BURN;
            return 0;
        }

        // 냄비에 재료 담긴 여부 파악
        bool isSoupedWater = ingredient.Find("soupedWater").gameObject.activeSelf;
        bool isRamened = ingredient.Find("ramened").gameObject.activeSelf;
        bool isChoppedLeek = ingredient.Find("choppedLeek").gameObject.activeSelf;
        bool isEgged = ingredient.Find("egged").gameObject.activeSelf;

        Debug.Log("끝난 시간: " + endTime + ", 담긴 재료 정보: " + isSoupedWater + isRamened + isChoppedLeek + isEgged);

        int money = 0;
        if (!isSoupedWater || !isRamened || endTime > 10f)
        {
            customerTalking = FAIL;
        }
        else if (isEgged && isChoppedLeek && endTime > 8f)
        {
            money = MONEY[2];
            customerTalking = mergeCustomerTalking(SALTY, money);
        }
        else if (isEgged && isChoppedLeek && endTime > 5f && eggAfterFive && leekAfterFive)
        {
            money = MONEY[5];
            customerTalking = VERY_NICE + " " + money.ToString() + "원 이야!";
        }
        else if (isEgged && isChoppedLeek && endTime > 5f && (eggAfterFive || leekAfterFive))
        {
            money = MONEY[4];
            customerTalking = mergeCustomerTalking(NO_SOUL, money);
        }
        else if (isEgged && isChoppedLeek && endTime > 5f)
        {
            money = MONEY[3];
            customerTalking = mergeCustomerTalking(NO_SOUL, money);
        }
        else if (isEgged && isChoppedLeek)
        {
            money = MONEY[2];
            customerTalking = mergeCustomerTalking(RAW, money);
        }
        else if (isEgged && endTime > 8f)
        {
            money = MONEY[2];
            customerTalking = mergeCustomerTalking(SALTY, money);
        }
        else if (isEgged && endTime > 5f && eggAfterFive)
        {
            money = MONEY[3];
            customerTalking = mergeCustomerTalking(SIMPLE, money);
        }
        else if (isEgged && endTime > 5f)
        {
            money = MONEY[2];
            customerTalking = mergeCustomerTalking(NO_SOUL_EMPTY, money);
        }
        else if (isEgged)
        {
            money = MONEY[2];
            customerTalking = mergeCustomerTalking(RAW, money);
        }
        else if (isChoppedLeek && endTime > 8f)
        {
            money = MONEY[0];
            customerTalking = mergeCustomerTalking(SALTY_EMPTY, money);
        }
        else if (isChoppedLeek && endTime > 5f && leekAfterFive)
        {
            money = MONEY[3];
            customerTalking = mergeCustomerTalking(SIMPLE, money);
        }
        else if (isChoppedLeek && endTime > 5f)
        {
            money = MONEY[2];
            customerTalking = mergeCustomerTalking(NO_SOUL_EMPTY, money);
        }
        else if (isChoppedLeek)
        {
            money = MONEY[0];
            customerTalking = mergeCustomerTalking(EMPTY_RAW, money);
        }
        else if (endTime > 8f)
        {
            money = MONEY[0];
            customerTalking = mergeCustomerTalking(SALTY_EMPTY, money);
        }
        else if (endTime > 5f)
        {
            money = MONEY[1];
            customerTalking = mergeCustomerTalking(SIMPLE, money);
        }
        else
        {
            money = MONEY[0];
            customerTalking = mergeCustomerTalking(EMPTY_RAW, money);
        }
        return money;
    }

    private void MoveOriginPos()
    {
        transform.position = originPos;
        isComp = false;

        // 원점으로 돌아오면서 냄비가 가만히 있으므로 true로 변경
        isStatic = true;

        // 탄 냄비라면 -> 기존 냄비로 바뀌게
        if (transform.parent.name == "BlackPots")
        {
            // 탄 냄비 비활성화
            this.gameObject.SetActive(false);
            // 자식 번호 알아내기
            int childNum = transform.GetSiblingIndex();
            GameObject.Find("Pots").transform.GetChild(childNum).gameObject.SetActive(true);
            GameObject.Find("Pots").transform.GetChild(childNum).gameObject.GetComponent<PotMoveTo>().formatPot();
        }
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

            // 남은 냄비 오브젝트 관리하는 스크립트
            ExtraPot extraPotScript = GameObject.Find("GameManager").GetComponent<ExtraPot>();
            int extraPotIndex = extraPotScript.extraPotIndex;

            // 남은 냄비 있을 경우만 원상복구
            if (extraPotIndex >= 0)
            {
                GameObject.Find("Pots").transform.GetChild(childNum).gameObject.SetActive(true);
                GameObject.Find("Pots").transform.GetChild(childNum).gameObject.GetComponent<PotMoveTo>().formatPot();

                // 남은 냄비 없애기
                extraPotScript.extraPotObj.transform.GetChild(extraPotIndex).gameObject.SetActive(false);
                extraPotScript.extraPotIndex--;
            }
        }

        // 남은 냄비가 없다면 -> 원상 복구 안되고 탄거 기존꺼 다 사라짐
        if (GameObject.Find("GameManager").GetComponent<ExtraPot>().extraPotIndex < 0)
        {
            int childNum = transform.GetSiblingIndex();
            this.gameObject.SetActive(false);
            GameObject.Find("Pots").transform.GetChild(childNum).gameObject.SetActive(false);
            GameObject.Find("BlackPots").transform.GetChild(childNum).gameObject.SetActive(false);
        }
    }

    private void formatPot()
    {
        // 시간 및 판정 변수 초기화
        time = 0f;
        isStart = false;
        eggAfterFive = false;
        leekAfterFive = false;

        if (transform.parent.name == "BlackPots")
        {
            return;
        }
        // 냄비 속 초기화
        transform.Find("water").gameObject.SetActive(false);
        Transform water = transform.Find("water");
        water.Find("waterStand").gameObject.SetActive(false);
        water.Find("waterFill").gameObject.SetActive(false);
        water.Find("waterSky000").gameObject.SetActive(false);
        water.Find("waterSky001").gameObject.SetActive(false);
        water.Find("waterSky002").gameObject.SetActive(false);

        transform.Find("soupedWater").gameObject.SetActive(false);
        water = transform.Find("soupedWater");
        water.Find("waterStand").gameObject.SetActive(false);
        water.Find("waterFill").gameObject.SetActive(false);
        water.Find("waterSky000").gameObject.SetActive(false);
        water.Find("waterSky001").gameObject.SetActive(false);
        water.Find("waterSky002").gameObject.SetActive(false);



        transform.Find("souped").gameObject.SetActive(false);
        transform.Find("ramened").gameObject.SetActive(false);
        transform.Find("choppedLeek").gameObject.SetActive(false);
        transform.Find("egged").gameObject.SetActive(false);
    }
}