using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartText : MonoBehaviour
{
    public Text talk;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("TwoOpen", 1.5f);    
    }

    public void TwoOpen()
    {
        talk.text = "맛있는 라면을 만들어볼까요?";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
