using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMemoryClear : MonoBehaviour
{
    public Button btnClear;
    void Start()
    {
        this.btnClear.onClick.AddListener(() =>
        {
            //ȿ����
            SoundManager.PlaySFX("Selection");
            //���� �������� ���ư��� �̺�Ʈ ���� -> App Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.BackMainGame);
        });
    }

}
