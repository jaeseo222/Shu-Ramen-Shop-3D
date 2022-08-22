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
            //SceneManager.LoadScene("StartScene");
            SceneManager.LoadScene("RankingScene");
        }
        else
        {
            // 없다면 -> 이름 등록 씬으로
            SceneManager.LoadScene("NameScene");
        }
    }
}
