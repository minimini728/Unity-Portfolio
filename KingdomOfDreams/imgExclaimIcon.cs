using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imgExclaimIcon : MonoBehaviour
{
    public void Init()
    {
        //느낌표 활성화 이벤트 등록
        EventDispatcher.instance.AddEventHandler<int>((int)LHMEventType.eEventType.EXCLAIM_ICON_BOOK_ITEM, onExclaimEvent);
    }

    private void onExclaimEvent(short type, int a) //느낌표 활성화 이벤트
    {
        if (a == 0) //비활성화
        {
            this.gameObject.SetActive(false);

        }
        else if (a == 1) //활성화
        {
            this.gameObject.SetActive(true);

        }
    }
    private void OnDestroy()
    {   
        //이벤트 중복 방지, 이벤트 제거
        EventDispatcher.instance.RemoveEventHandler<int>((int)LHMEventType.eEventType.EXCLAIM_ICON_BOOK_ITEM, onExclaimEvent);
    }
}
