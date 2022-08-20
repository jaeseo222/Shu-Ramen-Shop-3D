using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTutorial : MonoBehaviour
{
    public GameObject firstPage;// 첫 페이지

    public void PageNext() {
        // 두 번째 페이지 보임
        firstPage.SetActive(false);
    }
    public void PagePre()
    {
        // 첫 번째 페이지 보임
        firstPage.SetActive(true);
    }
}
