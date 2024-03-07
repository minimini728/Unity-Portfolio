using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imgExclaimIcon : MonoBehaviour
{
    public void Init()
    {
        //����ǥ Ȱ��ȭ �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler<int>((int)LHMEventType.eEventType.EXCLAIM_ICON_BOOK_ITEM, onExclaimEvent);
    }

    private void onExclaimEvent(short type, int a) //����ǥ Ȱ��ȭ �̺�Ʈ
    {
        if (a == 0) //��Ȱ��ȭ
        {
            this.gameObject.SetActive(false);

        }
        else if (a == 1) //Ȱ��ȭ
        {
            this.gameObject.SetActive(true);

        }
    }
    private void OnDestroy()
    {   
        //�̺�Ʈ �ߺ� ����, �̺�Ʈ ����
        EventDispatcher.instance.RemoveEventHandler<int>((int)LHMEventType.eEventType.EXCLAIM_ICON_BOOK_ITEM, onExclaimEvent);
    }
}
