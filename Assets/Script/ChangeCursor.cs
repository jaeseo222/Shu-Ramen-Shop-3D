using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D cursorImgUp, cursorImgDown;
    Vector2 hospot;

    private void Start()
    {
        hospot.x = cursorImgUp.width / 2;
        hospot.y = cursorImgUp.height / 2;

        Cursor.SetCursor(cursorImgUp, hospot, CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorImgDown, hospot, CursorMode.Auto);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorImgUp, hospot, CursorMode.Auto);

        }
    }

}
