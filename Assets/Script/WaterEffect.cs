using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffect : MonoBehaviour
{
    public Transform pot;// 현재 냄비
    float time;// 물 담긴 시간
    // Start is called before the first frame update
    void Start()
    {
        pot = gameObject.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        // 물 안 넣었으면 리턴
        if (!pot.GetComponent<PotMoveTo>().isStart)
            return;

        time = pot.GetComponent<PotMoveTo>().time;

        // 시간에 따라 끓고 있는 애니메이션 실행
        if (time >= 0f && time <= 1f)
        {
            Debug.Log("asdf");
            transform.Find("waterFill").gameObject.SetActive(true);// 물 채워지는 애니메이션
        }
        if(!transform.Find("waterFill").gameObject.activeSelf)
        {
            transform.Find("waterStand").gameObject.SetActive(true);// 끓고 있는 중인 물
        }

        if (time > 2f)
        {
            transform.Find("waterSky000").gameObject.SetActive(true); // 기포 올라오는 애니메이션
        }
         if (time > 5f)
        {
            transform.Find("waterSky001").gameObject.SetActive(true);// 물 약하게 끓는 애니메이션
        }
         if (time > 10f)
        {
            transform.Find("waterSky002").gameObject.SetActive(true);//물 강하게 끓는 애니메이션
        }
    }
}
