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
            //효과음
            SoundManager.PlaySFX("Selection");
            //메인 게임으로 돌아가는 이벤트 전송 -> App 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.BackMainGame);
        });
    }

}
