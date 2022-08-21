using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalMoney : MonoBehaviour
{
    public static int totalMoney = 0;
    public static int restaurantMoney = 0;
    public static int foundedMoney = 0;
    public static int partTimeMoney = 0;

    public GameObject totalMoneyObj;

    // Start is called before the first frame update
    void Start()
    {

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
