using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidClover : MonoBehaviour
{
    void Start()
    {
        AudioListener.volume = 5;

        this.transform.rotation = Quaternion.Euler(0, 90, 0); //�ո������ ���̰�
    }

    void Update()
    {
        StartCoroutine("Move"); //�Ʒ��� �������� �ڷ�ƾ ����
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //�䳢 ǥ�� ���� ǥ�� �̺�Ʈ ���� -> AvoidPlayerController Ŭ������
            EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.AvoidChangeFace, 2);

            //���� ���� �̺�Ʈ ���� -> AvoidMain Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.AvoidUpScore);

            //ȿ����
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
