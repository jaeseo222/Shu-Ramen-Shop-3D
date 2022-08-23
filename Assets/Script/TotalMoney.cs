using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalMoney : MonoBehaviour
{
    public static int totalMoney = 0;
    public static int [] stageMoney = new int[] { 0, 0, 0 };

    public GameObject totalMoneyObj;

    // Start is called before the first frame update
    void Start()
    {
        totalMoney = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalMoney != 0)
        {
            totalMoneyObj.GetComponent<Text>().text = string.Format("{0:#,###}", totalMoney);

        }
        else
        {
            totalMoneyObj.GetComponent<Text>().text = "0";

        }
    }
}
