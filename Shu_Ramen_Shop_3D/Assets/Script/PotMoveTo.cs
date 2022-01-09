using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotMoveTo : MonoBehaviour
{
    private Transform ingredient;
    private Vector3 originPos;//원래 위치
    private bool isComp;

    private void Start()
    {
        ingredient = transform;
        originPos = GetComponent<Rigidbody>().position;//원래위치 저장해놓기
        isComp = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonUp(0))
            if (Inclusion(other.bounds, transform.GetComponent<Collider>().bounds))
                if (other.gameObject.name == "comp")
                {
                    isComp = true;
                    MoveToComp();
                    Invoke("MoveOriginPos", 1.0f);
                    Invoke("formatPot", 1.0f);
                }
    }

    private void OnMouseUp()
    {
        if (!isComp)
            MoveOriginPos();
    }
    private void MoveToComp()
    {
        transform.position = new Vector3(9.56f, -2.97f, -1.26f);
        //판정

        //애니메이션 추가
    }
    private void MoveOriginPos()
    {
        transform.position = originPos;
        isComp = false;
    }

    private void formatPot() {
        ingredient.Find("water").gameObject.SetActive(false);
        ingredient.Find("soupedWater").gameObject.SetActive(false);
        ingredient.Find("souped").gameObject.SetActive(false);
        ingredient.Find("ramened").gameObject.SetActive(false);
        ingredient.Find("choppedLeek").gameObject.SetActive(false);
        ingredient.Find("egged").gameObject.SetActive(false);
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
