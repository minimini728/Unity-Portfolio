using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBlock : MonoBehaviour
{
    private bool isMovingUp = true; //현재 이동 방향 추적
    private int moveCount = 0; //이동 횟수
    private float speed = 3f; //이동 속도
    public float waitSecond;

    public void Init(float a)
    {
        this.waitSecond = a;
    }

    void Update()
    {   
        if(this.waitSecond != 0)
        {
            StartCoroutine(Fall(this.waitSecond));
        }

        if(this.transform.position.y <= -20f)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Fall(float a)
    {
        yield return new WaitForSeconds(a);
        //이동 횟수가 4번보다 작을 때만 이동
        if (moveCount < 4)
        {
            //위로 이동 중인 경우
            if (isMovingUp)
            {
                //y 위치를 위로 이동
                transform.Translate(Vector3.up * Time.deltaTime * this.speed);

                //오브젝트의 Y 위치가 0.5 이상인 경우 아래로 이동 방향으로 변경하고 이동 횟수 증가
                if (transform.position.y >= 0.5f)
                {
                    isMovingUp = false;
                    moveCount++;
                }
            }
            //아래로 이동 중인 경우
            else
            {
                //y 위치를 아래로 이동
                transform.Translate(Vector3.down * Time.deltaTime * this.speed);

                //오브젝트의 Y 위치가 -0.5 이하인 경우 위로 이동 방향으로 변경하고 이동 횟수 증가
                if (transform.position.y <= -0.5f)
                {
                    isMovingUp = true;
                    moveCount++;
                }
            }
        }

        //moveCount가 4회 이상일 경우 오브젝트를 떨어트림
        if (moveCount >= 4)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 5f);
        }
    }

}
