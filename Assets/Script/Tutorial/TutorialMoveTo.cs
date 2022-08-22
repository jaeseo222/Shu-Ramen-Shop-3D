using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMoveTo : MonoBehaviour
{
    private GameObject gameObj;
    private Vector3 originPos;//원래 위치
    public bool isPot;

    private void Start()
    {
        originPos = this.gameObject.transform.position;//원래위치 저장해놓기
        isPot = false;
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
            Invoke("MoveOriginPos", 1.0f);
        }
    }
    private void PotFill(GameObject gameObj, string name)
    {
        Transform pot = GameObject.Find(name).transform;//냄비

        // 탄 냄비라면 리턴
        if (pot.parent.name == "BlackPots")
        {
            return;
        }

        float time = pot.GetComponent<TutorialPotMoveTo>().time;

        Debug.Log("음식 넣은 시간: " + time);

        //각자 애니메이션 추가
        switch (gameObject.name)
        {
            case "waterPot"://물 -> 타이머 시작
                pot.GetComponent<TutorialPotMoveTo>().isStart = true;
                pot.Find("water").gameObject.SetActive(true);
                SoundEffect._soundEffect.waterAudio();
                break;
        }
        transform.position = new Vector3(transform.position.x, -13.8f, transform.position.z);
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
    public Vector3 centerMove(Vector3 source)
    {
        if (this.gameObject.name == "waterPot")
            return new Vector3(source.x, source.y, source.z + 0.8f);
        else
            return source;
    }
}
