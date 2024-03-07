using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIInventory : MonoBehaviour
{
    public UIInventoryScroll scrollview; //��ũ�Ѻ�
    public Image imgLock; //�ڹ��� �̹���
    public GameObject imgItems; //����ü�� UI
    public Button btnClose;

    private int stageNum; //���� ��������

    public void Start()
    {   
        this.btnClose.onClick.AddListener(() => //�ݱ� ��ư
        {
            this.gameObject.SetActive(false);
        });

        //���� �������� ��ȣ ��������
        foreach (StageInfo stageInfo in InfoManager.instance.StageInfos)
        {
            Debug.LogFormat("<color>stageInfo.stage : {0}, stageInfo.isClear : {1}</color>", stageInfo.stage, stageInfo.isClear);
            if (stageInfo.isClear == false)
            {
                //���ϴ°��� stageInfo.num ����
                this.stageNum = stageInfo.stage;
                break;
            }
        }

        if(this.stageNum >= 7) //8���������� ��� ����ü�� �����ֱ�
        {
            this.imgLock.gameObject.SetActive(false);
            this.imgItems.SetActive(true);
        }

        //����ü�� �����ִ� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)LHMEventType.eEventType.SHOW_PRODUCTION_CHAIN, EventShowProductionChain);

    }

    public void Init() //�ʱ�ȭ
    {
        this.scrollview.Init(); //��ũ�Ѻ� �ʱ�ȭ 
    }

    public void EventShowProductionChain(short type) //����ü�� �����ִ� �̺�Ʈ
    {
        this.imgLock.gameObject.SetActive(false);
        this.imgItems.SetActive(true);

    }
    private void OnDestroy()
    {   
        //�̺�Ʈ �ߺ� ����, �̺�Ʈ ����
        EventDispatcher.instance.RemoveEventHandler((int)LHMEventType.eEventType.SHOW_PRODUCTION_CHAIN, EventShowProductionChain);
    }
}
