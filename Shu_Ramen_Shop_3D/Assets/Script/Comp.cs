using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp : MonoBehaviour
{
    //MoveTo moveTo;
    //private void Update()
    //{
    //    moveTo = GameObject.Find("waterPot").GetComponent<MoveTo>();
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    if (Inclusion(transform.GetComponent<Collider>().bounds, other.bounds)) //콜라이더 범위 내에서만 작동
    //        if (Input.GetMouseButtonUp(0)&&other.tag=="pot")
    //        {
    //            moveTo.isComp = true;
    //            //쟁반 가져가는 애니메이션
    //        }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "pot")
    //        moveTo.isComp = false;
    //}

    //Vector3[] positions = new Vector3[6];
    //bool Inclusion(Bounds source, Bounds target)
    //{
    //    positions[0] = target.center + new Vector3(target.extents.x, 0f, 0f);
    //    positions[1] = target.center + new Vector3(-target.extents.x, 0f, 0f);
    //    positions[2] = target.center + new Vector3(0f, target.extents.y, 0f);
    //    positions[3] = target.center + new Vector3(0f, -target.extents.y, 0f);
    //    positions[4] = target.center + new Vector3(0f, 0f, target.extents.z);
    //    positions[5] = target.center + new Vector3(0f, 0f, -target.extents.z);

    //    foreach (Vector3 pos in positions)
    //        if (!source.Contains(pos))
    //            return false;
    //    return true;
    //}
}
