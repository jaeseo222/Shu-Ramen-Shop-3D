using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragMove : MonoBehaviour
{ 
    // Play Scene
    private const float PLAY_DISTANCE = 14f;
    private const float PLAY_POSITION_UP = -0.0f;
    private const float PLAY_POSITION_DOWN = -3.5f;

    // Tutorial Scene
    private const float TUTORIAL_DISTANCE = 22f;
    private const float TUTORIAL_POSITION_UP = 1.5f;
    private const float TUTORIAL_POSITION_DOWN = -1f;

    private float distance; // 화면에 보이는 거리
    private float defaultDistance; // 화면에 보이는 default 거리
    private float positionUpRange; //휠을 밀어 돌렸을 때의 범위
    private float positionDownRange; // 휠을 당겨 올렸을 때의 범위

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            distance = PLAY_DISTANCE;
            defaultDistance = PLAY_DISTANCE;
            positionUpRange = PLAY_POSITION_UP;
            positionDownRange = PLAY_POSITION_DOWN;
        }
        if (SceneManager.GetActiveScene().name == "TutorialScene")
        {
            distance = TUTORIAL_DISTANCE;
            defaultDistance = TUTORIAL_DISTANCE;
            positionUpRange = TUTORIAL_POSITION_UP;
            positionDownRange = TUTORIAL_POSITION_DOWN;
        }
    }

    //주전자 들기
    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //if (transform.position.y < -2.7f) objPosition.y = -2.7f;//바닥으로 안 꺼지게 제어
        transform.position = objPosition;

        //주전자 위치 앞뒤로 조정
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0 && transform.position.z < positionUpRange)// 휠을 밀어 돌렸을 때의 처리 ↑
            distance += 0.8f;
        else if (wheelInput < 0 && transform.position.z > positionDownRange)// 휠을 당겨 올렸을 때의 처리 ↓
            distance -= 0.8f;
    }
    private void OnMouseUp() {
        distance = defaultDistance;
    }
}