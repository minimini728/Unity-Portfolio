using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIInventory : MonoBehaviour
{
    public UIInventoryScroll scrollview; //스크롤뷰
    public Image imgLock; //자물쇠 이미지
    public GameObject imgItems; //생산체인 UI
    public Button btnClose;

    private int stageNum; //현재 스테이지

    public void Start()
    {   
        this.btnClose.onClick.AddListener(() => //닫기 버튼
        {
            this.gameObject.SetActive(false);
        });

        //현재 스테이지 번호 가져오기
        foreach (StageInfo stageInfo in InfoManager.instance.StageInfos)
        {
            Debug.LogFormat("<color>stageInfo.stage : {0}, stageInfo.isClear : {1}</color>", stageInfo.stage, stageInfo.isClear);
            if (stageInfo.isClear == false)
            {
                //원하는곳에 stageInfo.num 저장
                this.stageNum = stageInfo.stage;
                break;
            }
        }

        if(this.stageNum >= 7) //8스테이지일 경우 생산체인 보여주기
        {
            this.imgLock.gameObject.SetActive(false);
            this.imgItems.SetActive(true);
        }

        //생산체인 보여주는 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)LHMEventType.eEventType.SHOW_PRODUCTION_CHAIN, EventShowProductionChain);

    }

    public void Init() //초기화
    {
        this.scrollview.Init(); //스크롤뷰 초기화 
    }

    public void EventShowProductionChain(short type) //생산체인 보여주는 이벤트
    {
        this.imgLock.gameObject.SetActive(false);
        this.imgItems.SetActive(true);

    }
    private void OnDestroy()
    {   
        //이벤트 중복 방지, 이벤트 제거
        EventDispatcher.instance.RemoveEventHandler((int)LHMEventType.eEventType.SHOW_PRODUCTION_CHAIN, EventShowProductionChain);
    }
}
