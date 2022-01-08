using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    private RigidbodyConstraints rigidCon;
    private Vector3 originPos;//원래 위치
    public bool isPot;

    private void Start()
    {
        rigidCon = GetComponent<Rigidbody>().constraints;
        originPos = GetComponent<Rigidbody>().position;//원래위치 저장해놓기
        isPot = false;
    }

    private void OnMouseUp()
    {
        if (GameObject.FindWithTag("ingredient"))//물, 계란, 스프, 라면, 파
        {
            //냄비 있는 곳에서 마우스 떼면(냄비 트리거 pot.cs 작동)
            //공중에서 1초동안 물 따르고 제자리로
            if (isPot)
                StartCoroutine(PotFill());

            //냄비 없는 곳에서 떼면 바로 제자리로
            else
                transform.position = originPos;
        }
    }

    private IEnumerator PotFill()
    {
        //애니메이션 추가
        transform.position = new Vector3(transform.position.x, -1.53f, transform.position.z);
        rigidCon = RigidbodyConstraints.FreezePositionX;
        rigidCon = RigidbodyConstraints.FreezePositionY;
        rigidCon = RigidbodyConstraints.FreezePositionZ;
        yield return new WaitForSeconds(1.0f);
        transform.position = originPos;
    }
}
