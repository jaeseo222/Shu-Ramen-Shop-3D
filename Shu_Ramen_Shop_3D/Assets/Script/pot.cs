using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{
    WaterPot waterPot;
    private void Update()
    {
        waterPot = GameObject.Find("waterPot").GetComponent<WaterPot>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonUp(0))
            switch (other.gameObject.name) {
                case "waterPot"://물 담기
                    waterPot.isPot = true;
                    transform.Find("water").gameObject.SetActive(true);
                    break;
            }
    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "waterPot":
                waterPot.isPot = false;
                break;
        }

    }
}
