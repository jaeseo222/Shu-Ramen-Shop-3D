using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPot : MonoBehaviour
{
    Ray ray;
    private RaycastHit hit;
    private Vector3 originPos, pos;//주전자 원래 위치, 고정시키려는 위치
    public GameObject potImg;//주전자 위 흰색 캔버스

    private void Start()
    {
        originPos = GetComponent<Rigidbody>().position;//주전자 원래위치 저장해놓기
        potImg = GameObject.Find("Canvas/waterpot");
    }
    private void Update()
    {
        //주전자 따라다닐 흰색 캔버스
        potImg.transform.position=Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0, 0));
    }
    private void OnMouseDrag()
    {
        //주전자 들기
        float distance = 10;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }
    private void OnMouseUp()
    {
        //주전자 내려놓기

        //내려놓은 마지막 위치
        pos = transform.position;
        //냄비있는 곳에서 마우스 떼면(냄비이미지&&주전자이미지 겹칠 때)
        //1초동안 물 따르고 제자리로
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //1초 동안 물 따르기
            StartCoroutine("TimeCo");
            switch (hit.collider.gameObject.name)
            {
                //각 주전자에 따라
                case "pot1":
                    transform.Find("pot1").transform.Find("water").gameObject.SetActive(true);
                    //처리 내용

                    break;

            }
        }
        //없는 곳에서 떼면, 바로 제자리로
        transform.position = originPos;
    }

    private IEnumerator TimeCo()
    {
        //물따르는 애니메이션 추가

        yield return new WaitForSeconds(0.1f);
        transform.position = pos;
    }
}