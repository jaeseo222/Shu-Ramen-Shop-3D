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
}
