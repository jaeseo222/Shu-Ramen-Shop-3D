using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StartMusic : MonoBehaviour
{
    public void Awake()
    {
        // 씬 넘어가도 배경음악 계속 재생되도록
        // 배경음악 갖고 있는 현재 object 파괴되지 않도록 함
        // DontDestroyOnLoad 할 때 마다 쌓이기 때문에, 1개 이상 중복 생성되었을 시 파괴
        var objs = FindObjectsOfType<StartMusic>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
 