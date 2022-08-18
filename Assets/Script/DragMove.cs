using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMove : MonoBehaviour
{
    private float distance;
    private Rigidbody myRigid;

    private void Start()
    {
        distance = 14f;
        myRigid = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        myRigid.velocity = Vector3.zero;
    }

    //주전자 들기
    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // (버그) 바닥으로 안 꺼지게 하려다보니 자꾸 위치를 강제적으로 돌려놓아서 떨리는 현상 발생
        // if (transform.position.y < -4.0f) objPosition.y = -2.7f; // 바닥으로 안 꺼지게 제어
        
        // 리지드바디를 움직여야 충돌 시 떨림 현상 방지
        myRigid.MovePosition(objPosition);

        //주전자 위치 앞뒤로 조정
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0 && transform.position.z < -0.2f)// 휠을 밀어 돌렸을 때의 처리 ↑
            distance += 0.8f;
        else if (wheelInput < 0 && transform.position.z > -5.0f)// 휠을 당겨 올렸을 때의 처리 ↓
            distance -= 0.8f;
    }
    private void OnMouseUp() {
        distance = 14f;
    }
}