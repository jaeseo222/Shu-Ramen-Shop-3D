using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMove : MonoBehaviour
{
    private float distance;

    private void Start()
    {
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
    private void OnMouseUp() {
        distance = 14f;
    }
}