using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    private GameObject gameObj;
    private Vector3 originPos;//원래 위치
    public bool isPot;
    private Animator animator;
    public Transform pot; // 재료를 갖다 댄 냄비 위치
    private Transform[] allChildren;

    private void Start()
    {
        originPos = this.gameObject.transform.position;//원래위치 저장해놓기
        isPot = false;
        if(gameObject.GetComponent<Animator>())
        animator = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerStay(Collider other)
    {
        // other.bounds : 콜라이더의 중심 좌표와 콜라이더의 사이즈의 반
        // other.center : 콜라이더의 중심 좌표
        // other.extents : 콜라이더 사이즈의 반

        // 마우스 뗐을 때
        // 내가 드래그 하고 있는 오브젝트의 콜라이더의 센터가 충돌 오브젝트의 콜라이더 내에 있는지 체크
        // 주전자는 주둥이로 판단하기 위해 콜라이더의 센터 z+0.5f가 충돌 오브젝트의 콜라이더 내에 있는지 체크
        if (Input.GetMouseButtonUp(0))
        {
            if (other.tag != "pot")
            {
                return;
            }
            if (!other.bounds.Contains(centerMove(transform.GetComponent<Collider>().bounds.center)))
            {
                return;
            }
            isPot = true;
            PotFill(gameObj, other.gameObject.name);
            Invoke("MoveOriginPos", 0.35f);
        }
    }
    private void PotFill(GameObject gameObj, string name)
    {
        pot=GameObject.Find(name).transform;//냄비

        // 탄 냄비라면 리턴
        if (pot.parent.name == "BlackPots")
        {
            return;
        }

        float time = pot.GetComponent<PotMoveTo>().time;

        Debug.Log("음식 넣은 시간: " + time);

        //각자 애니메이션 추가
        switch (gameObject.name)
        {
            case "waterPot"://물 -> 타이머 시작
                pot.Find("water").gameObject.SetActive(true);
                pot.GetComponent<PotMoveTo>().isStart = true;
                SoundEffect._soundEffect.waterAudio();
                // 애니메이션 시작
                OnHandlerAnimation();
                break;
            case "soup"://스프
                pot.Find("souped").gameObject.SetActive(true);
                SoundEffect._soundEffect.soupAudio();
                // 애니메이션 시작
                OnHandlerAnimation();
                break;
            case "leek"://파
                Invoke("OnHandlerSetActiveLeek", 0.4f);
                if (time >= 5f)
                {
                    pot.GetComponent<PotMoveTo>().leekAfterFive = true;

                }
                SoundEffect._soundEffect.leekAudio();
                // 애니메이션
                OnHandlerAnimation();
                break;
            case "egg"://계란
                Invoke("OnHandlerSetActiveEgg", 0.4f);
                if (time >= 5f)
                {
                    pot.GetComponent<PotMoveTo>().eggAfterFive = true;

                }
                SoundEffect._soundEffect.eggAudio();
                // 애니메이션 시작
                OnHandlerAnimation();
                break;
            case "ramen"://면
                Invoke("OnHandlerSetActiveRamened", 0.4f);
                SoundEffect._soundEffect.ramenAudio();
                // 애니메이션
                OnHandlerAnimation();
                break;
        }
        //스프 + 물
        if (pot.Find("water").gameObject.activeSelf && pot.Find("souped").gameObject.activeSelf)
        {
            pot.Find("water").gameObject.SetActive(false);
            pot.Find("souped").gameObject.SetActive(false);
            pot.Find("soupedWater").gameObject.SetActive(true);
        }

        transform.position = new Vector3(transform.position.x, -1.53f, transform.position.z);
    }
    private void OnMouseUp()
    {
        if (!isPot)
            MoveOriginPos();
    }
    private void MoveOriginPos()
    {
        transform.position = originPos;
        isPot = false;
    }
    Vector3 centerMove(Vector3 source)
    {
        if (this.gameObject.name == "waterPot")
            return new Vector3(source.x, source.y, source.z + 0.8f);
        else
            return source;
    }
    private void OnHandlerSetActiveRamened() {
        pot.Find("ramened").gameObject.SetActive(true);
    }
    private void OnHandlerSetActiveLeek()
    {
        pot.Find("choppedLeek").gameObject.SetActive(true);
    }
    private void OnHandlerSetActiveEgg()
    {
        pot.Find("egged").gameObject.SetActive(true);
    }
    private void OnHandlerAnimation() {
        allChildren = gameObject.GetComponentsInChildren<Transform>();
        // 애니메이션
        foreach (Transform child in allChildren)
        {
            // 자기 자신도 반환하기 때문에 패스
            if (child.name == gameObject.transform.name)
                continue;
            animator = child.gameObject.GetComponent<Animator>();
            animator.SetTrigger("IsAct");
        }
    }
}
