using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    WaterPot waterPot;
    Renderer waterColor;
    private void Update()
    {
        waterPot = GameObject.Find("waterPot").GetComponent<WaterPot>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (Inclusion(transform.GetComponent<Collider>().bounds, other.bounds)) //콜라이더 범위 내에서만 작동
            if (Input.GetMouseButtonUp(0))
            {
                waterPot.isPot = true;
                switch (other.gameObject.name)
                {
                    case "waterPot"://물 담기
                        transform.Find("water").gameObject.SetActive(true);
                        break;
                    case "soup"://스프 넣기
                        transform.Find("souped").gameObject.SetActive(true);
                        break;
                }
                //스프 물
                if (transform.Find("water").gameObject.activeSelf && transform.Find("souped").gameObject.activeSelf)
                {
                    transform.Find("water").gameObject.SetActive(false);
                    transform.Find("souped").gameObject.SetActive(false);
                    transform.Find("soupedWater").gameObject.SetActive(true);
                }
            }
    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "waterPot":
            case "soup"://스프 넣기
                waterPot.isPot = false;
                break;
        }

    }

    Vector3[] positions = new Vector3[6];
    bool Inclusion(Bounds source, Bounds target)
    {
        positions[0] = target.center + new Vector3(target.extents.x, 0f, 0f);
        positions[1] = target.center + new Vector3(-target.extents.x, 0f, 0f);
        positions[2] = target.center + new Vector3(0f, target.extents.y, 0f);
        positions[3] = target.center + new Vector3(0f, -target.extents.y, 0f);
        positions[4] = target.center + new Vector3(0f, 0f, target.extents.z);
        positions[5] = target.center + new Vector3(0f, 0f, -target.extents.z);

        foreach (Vector3 pos in positions)
            if (!source.Contains(pos))
                return false;
        return true;
    }
}
