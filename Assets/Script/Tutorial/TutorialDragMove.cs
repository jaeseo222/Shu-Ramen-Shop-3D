using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDragMove : MonoBehaviour
{
    private float distance;

    private void Start()
    {
        distance = 22f;
    }

    //주전자 들기
    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;

        //주전자 위치 앞뒤로 조정
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0 && transform.position.z < 1.5f)// 휠을 밀어 돌렸을 때의 처리 ↑
            distance += 0.8f;
        else if (wheelInput < 0 && transform.position.z > -1f)// 휠을 당겨 올렸을 때의 처리 ↓
            distance -= 0.8f;
    }
    private void OnMouseUp()
    {
        distance = 22f;
    }
}