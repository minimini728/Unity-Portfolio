using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbOfPower : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("��ų ���� ����");

            //PlayerProperty skill Ǯ�� �ʱ�ȭ �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.FullPlayerSkillValue);

            Destroy(this.gameObject);
        }
    }

}
