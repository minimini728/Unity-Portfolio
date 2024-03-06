using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidEnemy : MonoBehaviour
{
    void Update()
    {
        StartCoroutine("Move"); //�Ʒ��� �������� �ڷ�ƾ ����
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //��� ǥ������ �ٲٴ� �̺�Ʈ ���� -> AvoidPlayerController Ŭ������
            EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.AvoidChangeFace, 1);
            //���� ���� �̺�Ʈ ���� -> AvoidMain Ŭ������
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
