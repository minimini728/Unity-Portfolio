using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBook : MonoBehaviour
{
    public UIBookScrollView scrollview; //���� UI ��ũ�Ѻ�

    public Button btnClose;

    public Image imgNotice; //���� ������ �˸���
    public Text txtNotice; //���� ������ �̸�
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
        this.scrollview.Init(); //��ũ�Ѻ� ó�� �ʱ�ȭ

        this.AddEvent();
        //this.imgExclaim.Init();
    }
    private void AddEvent()
    {   
        //���� ���� ������ ȹ�� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler<string>((int)LHMEventType.eEventType.CLAIM_BOOK_ITEM, Notice);
    }
    //���� ���� ������ �˸��� �����ִ� �޼���
    public void Notice(short type, string item)
    {
        this.imgNotice.gameObject.SetActive(true);
        this.txtNotice.text = string.Format("{0} ȹ��!", item);
    }

    private void OnDestroy()
    {
        //�̺�Ʈ �ߺ� ����, �̺�Ʈ ����
        EventDispatcher.instance.RemoveEventHandler<string>((int)LHMEventType.eEventType.CLAIM_BOOK_ITEM, Notice);

    }
}
