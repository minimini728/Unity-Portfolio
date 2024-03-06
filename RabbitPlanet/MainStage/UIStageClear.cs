using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageClear : MonoBehaviour
{
    public Button btnNextStage;

    void Start()
    {
        this.btnNextStage.onClick.AddListener(() =>
        {
            //ȿ����
            SoundManager.PlaySFX("Selection");

            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ClearStage);
        });

    }
}
