using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbOfLife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("생명 구슬 닿음");

            //PlayerProperty hp 풀로 초기화 이벤트 호출
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ResetPlayerHpValue);

            Destroy(this.gameObject);
        }
    }
}
