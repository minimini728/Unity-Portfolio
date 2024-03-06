using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ChangeToBattleField);

    }
}
