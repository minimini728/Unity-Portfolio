using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ShowUIBuildNotice);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.HideUIBuildNotice);
        }
    }
}
