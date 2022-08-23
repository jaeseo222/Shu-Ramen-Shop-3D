using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadTutorialScene() {
        SceneManager.LoadScene("TutorialScene");
    }
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void LoadNameScene()
    {
        SceneManager.LoadScene("NameScene");
    }

    public void LoadEndScene_Suc()
    {
        SceneManager.LoadScene("EndScene_Suc");
    }
    public void LoadEndScene_Fail()
    {
        SceneManager.LoadScene("EndScene_Fail");
    }
    public void LoadStageScene()
    {
        SceneManager.LoadScene("StageScene");
    }
}
