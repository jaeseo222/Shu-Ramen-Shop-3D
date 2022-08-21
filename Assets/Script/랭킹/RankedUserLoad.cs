using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class RankedUserLoad : MonoBehaviour
{
    
    // 랭킹에 몇 등까지 보여줄지
    private const int LAST = 30;

    // 랭킹 api 에서 가져온 유저 정보 객체 배열
    private User users;

    // 자기 자신
    private ScoreInfo me;

    // 랭킹 보드에 채워지는 유저
    public GameObject userObj;
    // 랭킹 보드 판
    public GameObject RankingBoard;

    // 임시 이름
    private string userName = "test5";
    
    [Serializable]
    class User
    {
        public ScoreInfo[] data;
    }


    // 유저 스코어 정보 객체
    [Serializable]
    class ScoreInfo
    {
        public int rank;
        public string name;
        public int restaurant;
        public int founded;
        public int part_time;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("인터넷 연결 에러");
        }
        else
        {
            StartCoroutine(RankingGetApi());
        }
    }

    // 랭킹 매겨진 유저 정보 get 요청 
    IEnumerator RankingGetApi()
    {
        UnityWebRequest request;
        using (request = UnityWebRequest.Get($"https://shu-ramen-3d.herokuapp.com/users/rank/{LAST}"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            // 네트워크 오류
            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                if (request.responseCode == 200)
                {
                    // 객체 형태로만 파싱할 수 있으므로 배열을 객체로 감싸서 파싱
                    users = JsonUtility.FromJson<User>("{\"data\":" + request.downloadHandler.text + "}");
                    ScoreInfo isMe = Array.Find(users.data, element => element.name == userName);
                    if (isMe != null) // 랭킹에서 자신이 존재한다면
                    {
                        me = isMe;
                        StartCoroutine(LoadRankingBoard());
                    }
                    else // 랭킹에 없다면
                    {
                        StartCoroutine(MyScoreGetApi());
                    }
                }
                else
                {
                    Debug.Log("요청 실패");
                }
            }
        }
    }

    // 자기 자신 점수 얻는 get 요청
    IEnumerator MyScoreGetApi()
    {
        UnityWebRequest request;
        using (request = UnityWebRequest.Get($"https://shu-ramen-3d.herokuapp.com/users/{userName}"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            // 네트워크 오류
            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                if (request.responseCode == 200)
                {
                    me = JsonUtility.FromJson<ScoreInfo>(request.downloadHandler.text);
                    StartCoroutine(LoadRankingBoard());
                }
                else
                {
                    Debug.Log("요청 실패");
                }
            }
        }
    }

    // 자기 자신 점수 칸 랭킹보드에 표시
    IEnumerator LoadMyRankingBoard()
    {
        userObj.GetComponent<Image>().color = new Color32(255, 229, 114, 100); // 색깔 표시
        if(me.rank > 0) // 랭킹에 있다면
        {
            userObj.transform.GetChild(0).GetComponent<Text>().text = me.rank.ToString();
        }
        else
        {
            userObj.transform.GetChild(0).GetComponent<Text>().text = "-"; // 등수 표시 x
        }
        userObj.transform.GetChild(1).GetComponent<Text>().text = me.name; // 이름
        userObj.transform.GetChild(2).GetComponent<Text>().text = GetThousandCommaText(me.restaurant); // 맛집 돈
        userObj.transform.GetChild(3).GetComponent<Text>().text = GetThousandCommaText(me.founded); // 창업 돈
        userObj.transform.GetChild(4).GetComponent<Text>().text = GetThousandCommaText(me.part_time); // 알바 돈
        yield return null;
    }

    // 랭킹 보드에 유저 칸 채우는 함수
    IEnumerator LoadRankingBoard()
    {
        ScoreInfo[] rankedUsers = users.data;
        for (int i = 0; i < rankedUsers.Length; i++)
        {
            GameObject newUser = GameObject.Instantiate(userObj) as GameObject;
            newUser.transform.SetParent(userObj.transform.parent);
            newUser.transform.localScale = Vector3.one;
            newUser.transform.localRotation = Quaternion.identity;

            ScoreInfo data = rankedUsers[i];
            newUser.transform.GetChild(0).GetComponent<Text>().text = data.rank.ToString(); // 등수
            newUser.transform.GetChild(1).GetComponent<Text>().text = data.name; // 이름
            newUser.transform.GetChild(2).GetComponent<Text>().text = GetThousandCommaText(data.restaurant); // 맛집 돈
            newUser.transform.GetChild(3).GetComponent<Text>().text = GetThousandCommaText(data.founded); // 창업 돈
            newUser.transform.GetChild(4).GetComponent<Text>().text = GetThousandCommaText(data.part_time); // 알바 돈

        }
        StartCoroutine(LoadMyRankingBoard());
        yield return null;
    }

    public string GetThousandCommaText(int data) 
    { 
        if(data == 0)
        {
            return "0";
        }
        return string.Format("{0:#,###}", data); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
