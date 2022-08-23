using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    public void onClickStage()
    {
        Debug.Log(this.gameObject.transform);
        StageManager.currStage = this.gameObject.transform.parent.transform.GetSiblingIndex();
        Debug.Log(StageManager.currStage);
    }

}
