using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFill : MonoBehaviour
{
    //[SerializeField]
    //private float maxY; // 주전자 최대 y위치
    //[SerializeField]
    //private float minY; // 주전자 최소 y위치
    //[SerializeField]
    //private ObjectDetector objectDetector; //오브젝트 선택을 위한
    //private Movement3D movement3D;//주전자 오브젝트 이동을 위한 Movement


    private RaycastHit hit;
    private Ray ray;

    private void Awake()
    {
        //movement3D=GetComponent<>
    }


    void Update()
    {
        //마지막으로 놓은 주전자의 위치 얻어오기

        //GameObject.Find("waterPot").gameObject.transform.position
        if (Input.GetMouseButtonUp(0))
        {
            //pot_lu채우기
            //if()


            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ////주전자 주변에 냄비 있는 지 확인 후, 물 채우기
            //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            //   if (hit.collider.name == "pot_lu")
            //        transform.Find("water").gameObject.SetActive(true);
        }
    }
}
