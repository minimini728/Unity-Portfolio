using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAvoidClear : MonoBehaviour
{
    public Button btnGameClear;
    void Start()
    {
        this.btnGameClear.onClick.AddListener(() =>
        {
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.BackMainGame);
        });
    }

}
