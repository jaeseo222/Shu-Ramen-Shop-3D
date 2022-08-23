using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameObject[] stageLock;
    public Text[] stageMoney;

    // 현재 게임한 스테이지 번호
    public static int currStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        int openStage = PlayerPrefs.GetInt("stage");
        // 해금 정보 + 돈 표시
        for (int i = 0; i <= openStage; i++)
        {
            stageLock[i].SetActive(false);
            int money = PlayerPrefs.GetInt(stageMoney[i].transform.name);
            stageMoney[i].text = GetThousandCommaText(money) + " 원";
        }
    }

    public string GetThousandCommaText(int data)
    {
        if (data == 0)
        {
            return "0";
        }
        return string.Format("{0:#,###}", data);
    }

}
