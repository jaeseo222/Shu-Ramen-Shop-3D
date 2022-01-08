using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotMoveTo : MonoBehaviour
{
    private RigidbodyConstraints rigidCon;
    private Vector3 originPos;//원래 위치
    private bool isComp;
    MoveTo moveTo;

    private void Start()
    {
        rigidCon = GetComponent<Rigidbody>().constraints;
        originPos = GetComponent<Rigidbody>().position;//원래위치 저장해놓기
        isComp = false;
    }
    private void Update()
    {
        moveTo = GameObject.Find("waterPot").GetComponent<MoveTo>();
    }

    private void OnTriggerStay(Collider other)
    {
        //콜라이더 범위 내에서만 작동
        if (Inclusion(transform.GetComponent<Collider>().bounds, other.bounds) && Input.GetMouseButtonUp(0))
        {
            if (other.tag == "ingredient")
            {
                moveTo.isPot = true;
                switch (other.gameObject.name)
                {
                    case "waterPot"://물 -> 타이머 시작
                        transform.Find("water").gameObject.SetActive(true);
                        break;
                    case "soup"://스프
                        transform.Find("souped").gameObject.SetActive(true);
                        break;
                    case "leek"://파
                        transform.Find("choppedLeek").gameObject.SetActive(true);
                        break;
                    case "egg"://계란
                        transform.Find("egged").gameObject.SetActive(true);
                        break;
                    case "ramen"://면
                        transform.Find("ramened").gameObject.SetActive(true);
                        break;
                }
                //스프 + 물
                if (transform.Find("water").gameObject.activeSelf && transform.Find("souped").gameObject.activeSelf)
                {
                    transform.Find("water").gameObject.SetActive(false);
                    transform.Find("souped").gameObject.SetActive(false);
                    transform.Find("soupedWater").gameObject.SetActive(true);
                }
            }
            if (other.gameObject.name == "comp")
            {
                isComp = true;
                StartCoroutine(MoveToComp());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ingredient")
            moveTo.isPot = false;
    }
    private void OnMouseUp()
    {
        if (!isComp)
            transform.position = originPos;
    }

    private IEnumerator MoveToComp()
    {
        transform.position = new Vector3(9.56f, -2.97f, -1.26f);
        rigidCon = RigidbodyConstraints.FreezePositionX;
        rigidCon = RigidbodyConstraints.FreezePositionY;
        rigidCon = RigidbodyConstraints.FreezePositionZ;

        //판정

        //애니메이션 추가

        yield return new WaitForSeconds(1.0f);
        transform.position = originPos;
    }

    Vector3[] positions = new Vector3[6];
    bool Inclusion(Bounds source, Bounds target)
    {
        positions[0] = target.center + new Vector3(target.extents.x, 0f, 0f);
        positions[1] = target.center + new Vector3(-target.extents.x, 0f, 0f);
        positions[2] = target.center + new Vector3(0f, target.extents.y, 0f);
        positions[3] = target.center + new Vector3(0f, -target.extents.y, 0f);
        positions[4] = target.center + new Vector3(0f, 0f, target.extents.z);
        positions[5] = target.center + new Vector3(0f, 0f, -target.extents.z);

        foreach (Vector3 pos in positions)
            if (!source.Contains(pos))
                return false;
        return true;
    }
}
