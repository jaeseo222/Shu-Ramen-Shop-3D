using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class EndManager : MonoBehaviour
{
    // 유저 스코어 정보 객체
    [Serializable]
    class ScoreInfo
    {
        public string name;
        public int restaurant;
        public int founded;
        public int partTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 스테이지에 번 돈 저장
        stageMoneyManage();
    }
    public void stageMoneyManage()
    {
        int stage = StageManager.currStage;
        int money = TotalMoney.totalMoney;
        TotalMoney.stageMoney[stage] = money;

        // 현재 로컬에 저장된 돈보다 많이 벌었다면 업데이트
        switch (stage)
        {
            case 0:
                if (PlayerPrefs.GetInt("partTime") < money)
                {
                    PlayerPrefs.SetInt("partTime", money);
                }
                break;
            case 1:
                if (PlayerPrefs.GetInt("founded") < money)
                {
                    PlayerPrefs.SetInt("founded", money);
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("restaurant") < money)
                {
                    PlayerPrefs.SetInt("restaurant", money);
                }
                break;
            default:
                break;
        }


        // 유저 post 요청
        StartCoroutine(UserScorePostApi());
    }

    // 유저 점수 업데이트 post api
    IEnumerator UserScorePostApi()
    {
        UnityWebRequest request;

        ScoreInfo user = new ScoreInfo
        {
            name = PlayerPrefs.GetString("name"),
            restaurant = TotalMoney.stageMoney[2],
            founded = TotalMoney.stageMoney[1],
            partTime = TotalMoney.stageMoney[0]
        };
        string json = JsonUtility.ToJson(user);

        Debug.Log(json);

        using (request = UnityWebRequest.Post("https://shu-ramen-3d.herokuapp.com/users/rank", json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                if (request.responseCode == 200)
                {
                    Debug.Log(request.downloadHandler.text);
                }
                else
                {
                    Debug.Log("요청 실패");
                }
            }
        }
    }
}
