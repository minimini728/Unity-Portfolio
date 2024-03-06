using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidClover : MonoBehaviour
{
    void Start()
    {
        AudioListener.volume = 5;

        this.transform.rotation = Quaternion.Euler(0, 90, 0); //앞모습으로 보이게
    }

    void Update()
    {
        StartCoroutine("Move"); //아래로 떨어지는 코루틴 실행
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //토끼 표정 웃는 표정 이벤트 전송 -> AvoidPlayerController 클래스로
            EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.AvoidChangeFace, 2);

            //점수 증가 이벤트 전송 -> AvoidMain 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.AvoidUpScore);

            //효과음
            SoundManager.PlaySFX("Pop");

            Destroy(this.gameObject);
        }

    }
    IEnumerator Move()
    {
        yield return null;

        if (this.transform.position.y <= -6f)
        {
            Destroy(this.gameObject);
        }

        transform.Translate(Vector3.down * 3f * Time.deltaTime,Space.World);

        transform.Rotate(Vector3.right * 90f * Time.deltaTime);
    }
}
