using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Image timerBar; // 타이머 색 채움
    public float maxTime; // 주어진 시간
    public float currTime; // 현재 시간

    // Start is called before the first frame update
    void Start()
    {
        currTime = maxTime;
        timerBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currTime > 0)
        {
            currTime -= Time.deltaTime; // 시간 없애기
            timerBar.fillAmount = currTime / maxTime;
        }        // 시간 초과되면
        else
        {
            Time.timeScale = 0;
        }
    }
}
