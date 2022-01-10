using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeRamen : MonoBehaviour
{
    private Collider collider;
    private Transform thisPot;
    private bool isFail;

    private float[] waterTime;
    private int totalMoney;
    private int[] money;

    private void Start()
    {
        collider = GetComponent<Collider>();
        thisPot = transform;//현재 냄비
        isFail = false;//true : 지금 장난해?
        waterTime = new float[] {0f, 5f,10f };
        totalMoney = 0;
        money =new int[]{0,300,400,500,600,800,1000 };
    }
    private void Update()
    {
        if (!(thisPot.Find("soupedWater").gameObject.activeSelf && thisPot.Find("ramened").gameObject.activeSelf))
            isFail = true;
        else
            isFail = false;
    }
    private void timeUpdate() {
        //물 넣고부터 시작


        if (Time.deltaTime >= 5f) { 
        //끓는 애니메이션 시작

        }

        if (Time.deltaTime >= 10f)
        {
            //타는 애니메이션 시작
            //더 이상 재료 활성화 못시킴
            collider.isTrigger = false;
        }
    }
    public void judgeRamen() {
        if (isFail)
        {
            totalMoney += money[0];
            return;
        }
        //if()


    
    
    }


}
