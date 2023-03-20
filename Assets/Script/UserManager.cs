using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 이름 이미 있다면 -> 스타트씬 바로
        if (PlayerPrefs.HasKey("name"))
        {
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            // 로컬에 스테이지별 점수 초기화
            PlayerPrefs.SetInt("restaurant", 0);
            PlayerPrefs.SetInt("founded", 0);
            PlayerPrefs.SetInt("partTime", 0);

            // 로컬에 현재 깬 스테이지에 대한 정보 초기화
            PlayerPrefs.SetInt("stage", 0);

            // 없다면 -> 이름 등록 씬으로
            SceneManager.LoadScene("NameScene");
        }
    }
}
