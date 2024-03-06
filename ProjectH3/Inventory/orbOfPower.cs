using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbOfPower : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("스킬 구슬 닿음");

            //PlayerProperty skill 풀로 초기화 이벤트 호출
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.FullPlayerSkillValue);

            Destroy(this.gameObject);
        }
    }

}
