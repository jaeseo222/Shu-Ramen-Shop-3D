using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPotImg : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.name);
        //pot1 이미지랑 부딪혔을 때
        if (other.gameObject.tag== "Pot")
        {
            print(other.gameObject.name);
        }
    }
}
