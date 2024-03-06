using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidEnemy : MonoBehaviour
{
    void Update()
    {
        StartCoroutine("Move"); //아래로 떨어지는 코루틴 실행
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //우는 표정으로 바꾸는 이벤트 전송 -> AvoidPlayerController 클래스로
            EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.AvoidChangeFace, 1);
            //게임 오버 이벤트 전송 -> AvoidMain 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.AvoidGameOver);

            Destroy(this.gameObject);
        }

    }
    IEnumerator Move()
    {
        yield return null;

        if(this.transform.position.y <= -6f)
        {
            Destroy(this.gameObject);
        }

        transform.Translate(-this.transform.up * 3f * Time.deltaTime);
    }
}
