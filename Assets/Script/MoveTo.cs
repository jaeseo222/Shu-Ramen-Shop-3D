﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
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
        if (Input.GetMouseButtonUp(0))
            if (Inclusion(other.bounds, transform.GetComponent<Collider>().bounds))
                if (other.tag == "pot")
                {
                    isPot = true;
                    PotFill(gameObj, other.gameObject.name);
                    Invoke("MoveOriginPos", 1.0f);
                }
    }

    private void PotFill(GameObject gameObj, string name)
    {
        Transform pot = GameObject.Find(name).transform;//냄비

        float time = pot.GetComponent<PotMoveTo>().time;

        Debug.Log("음식 넣은 시간: " + time);

        //각자 애니메이션 추가
        switch (gameObject.name)
        {
            case "waterPot"://물 -> 타이머 시작
                pot.GetComponent<PotMoveTo>().isStart = true;
                pot.Find("water").gameObject.SetActive(true);
                break;
            case "soup"://스프
                pot.Find("souped").gameObject.SetActive(true);
                break;
            case "leek"://파
                pot.Find("choppedLeek").gameObject.SetActive(true);
                if (time >= 5f)
                {
                    pot.GetComponent<PotMoveTo>().leekAfterFive = true;

                }
                break;
            case "egg"://계란
                pot.Find("egged").gameObject.SetActive(true);
                if(time >= 5f)
                {
                    pot.GetComponent<PotMoveTo>().eggAfterFive = true;

                }
                break;
            case "ramen"://면
                pot.Find("ramened").gameObject.SetActive(true);
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