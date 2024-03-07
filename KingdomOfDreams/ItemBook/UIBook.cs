using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBook : MonoBehaviour
{
    public UIBookScrollView scrollview; //도감 UI 스크롤뷰

    public Button btnClose;

    public Image imgNotice; //보상 아이템 알림판
    public Text txtNotice; //보상 아이템 이름
    public Button btnNoticeClose;
    //public imgExclaimIcon imgExclaim;

    void Start()
    {
        this.btnClose.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });

        this.btnNoticeClose.onClick.AddListener(() =>
        {
            this.imgNotice.gameObject.SetActive(false);
        });
    }
    public void Init()
    {
        this.scrollview.Init(); //스크롤뷰 처음 초기화

        this.AddEvent();
        //this.imgExclaim.Init();
    }
    private void AddEvent()
    {   
        //도감 보상 아이템 획득 이벤트 등록
        EventDispatcher.instance.AddEventHandler<string>((int)LHMEventType.eEventType.CLAIM_BOOK_ITEM, Notice);
    }
    //도감 보상 아이템 알림판 보여주는 메서드
    public void Notice(short type, string item)
    {
        this.imgNotice.gameObject.SetActive(true);
        this.txtNotice.text = string.Format("{0} 획득!", item);
    }

    private void OnDestroy()
    {
        //이벤트 중복 방지, 이벤트 제거
        EventDispatcher.instance.RemoveEventHandler<string>((int)LHMEventType.eEventType.CLAIM_BOOK_ITEM, Notice);

    }
}
