using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbOfLife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("���� ���� ����");

            //PlayerProperty hp Ǯ�� �ʱ�ȭ �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ResetPlayerHpValue);

            Destroy(this.gameObject);
        }
    }
}
