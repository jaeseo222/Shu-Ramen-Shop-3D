using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPot : MonoBehaviour
{
    Ray ray;
    private RigidbodyConstraints rigidCon;
    private RaycastHit hit;
    private Vector3 originPos;//주전자 원래 위치
    public bool isPot;
    private float distance;

    private void Start()
    {
        rigidCon = GetComponent<Rigidbody>().constraints;
        originPos = GetComponent<Rigidbody>().position;//주전자 원래위치 저장해놓기
        isPot = false;
        distance = 14f;
    }

    //주전자 들기
    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if (transform.position.y < -2.7f) objPosition.y = -2.7f;//바닥으로 안 꺼지게 제어
        transform.position = objPosition;

        //주전자 위치 앞뒤로 조정
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0 && transform.position.z < -0.2f)// 휠을 밀어 돌렸을 때의 처리 ↑
            distance += 0.8f;
        else if (wheelInput < 0 && transform.position.z > -5.0f)// 휠을 당겨 올렸을 때의 처리 ↓
            distance -= 0.8f;
    }

    //주전자 내려놓기
    private void OnMouseUp()
    {
        //냄비 있는 곳에서 마우스 떼면(냄비 트리거 pot.cs 작동)
        //공중에서 1초동안 물 따르고 제자리로
        if (isPot)
            StartCoroutine(PotWaterFill());
        
        //냄비 없는 곳에서 떼면 바로 제자리로
        else
            transform.position = originPos;

        distance = 14f;
    }

    private IEnumerator PotWaterFill()
    {
        //애니메이션 추가
        transform.position = new Vector3(transform.position.x, -1.53f, transform.position.z);
        rigidCon = RigidbodyConstraints.FreezePositionX;
        rigidCon = RigidbodyConstraints.FreezePositionY;
        rigidCon = RigidbodyConstraints.FreezePositionZ;
        yield return new WaitForSeconds(1.0f);
        transform.position = originPos;
    }
}