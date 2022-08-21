using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class PostUser : MonoBehaviour
{
    public Text userName;
    public GameObject WarningText;
    public GameObject DonePopup;

    [Serializable]
    class Response
    {
        public bool success;
        public string message;
    }

    [Serializable]
    class User
    {
        public string name;
    }

    public void onClickEnterName()
    {
        StartCoroutine(UserPostApi());
    }

    public void onChangeText()
    {
        WarningText.SetActive(false);
    }

    IEnumerator UserPostApi()
    {
        UnityWebRequest request;

        User user = new User
        {
            name = userName.text
        };
        string json = JsonUtility.ToJson(user);


        using (request = UnityWebRequest.Post("https://shu-ramen-3d.herokuapp.com/users", json))
        {
            Debug.Log(json);
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
                if(request.responseCode == 200)
                {
                    Response res = JsonUtility.FromJson<Response>(request.downloadHandler.text);
                    if(res.success == true)
                    {
                        // 등록 완료 팝업 1초 정도 뜨고
                        // 메인화면으로 넘어가기
                        DonePopup.SetActive(true);
                        Invoke("ChangeSceneMain", 1f);
                    }
                    else
                    {
                        WarningText.SetActive(true);
                    }
                }
                else
                {
                    Debug.Log("요청 실패");
                }
            }
        }
    }

    public void ChangeSceneMain()
    {
        SceneManager.LoadScene("StartScene");
    }
}
