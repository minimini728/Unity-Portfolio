using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public UIDialogue dialogue;

    public void Init()
    {   
        //���� ������ ȹ�� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler<int>((int)LHMEventType.eEventType.GET_BOOK_ITEM, this.GetBookItemEventHandler);
    }

    //���� ������ ȹ�� �̺�Ʈ a: ���� ������ id
    private void GetBookItemEventHandler(short type, int a)
    {
        this.GetBookItem(a); //���� ȹ�� �޼���

        if(a == 6001)
        {
            this.FindDialogue();

            var data = DataManager.instance.GetDialogueData(10096);
            InfoManager.instance.DialogueInfo.id = 10096;
            this.dialogue.gameObject.SetActive(true);
            this.dialogue.Init(data);
        }
    }

    //���� ������ ȹ�� �޼��� num: ���� ������ id
    public void GetBookItem(int num)
    {
        var data = DataManager.instance.GetBookItemData(num); //���� ������ �����Ϳ��� num�� ���� id�� ���� ������ ���� ��������

        //Debug.Log("���� ������ |" + data.name + "| ȹ��");

        var id = data.id; //id�� int�� ����
        var foundInfo = InfoManager.instance.BookItemInfos.Find(x => x.id == id); //���� ���� �����ۿ��� ã��

        if (foundInfo == null) //���� ���
        {
            BookItemInfo info = new BookItemInfo(id, 1); //�ش� id�� �ν��Ͻ� ����
            InfoManager.instance.BookItemInfos.Add(info); //���� ������ �ֱ�

            InfoManager.instance.SaveBookItemInfo(); //���� ���� �����ϱ�

            //BookUI refresh �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent((int)LHMEventType.eEventType.REFRESH_UI_BOOK);
            //����ǥ ǥ�� �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.EXCLAIM_ICON_BOOK_ITEM, 1);

        }
        else
        {
            return; //�̹� ȹ��
        }

    }

    public void FindDialogue()
    {
        this.dialogue = GameObject.FindObjectOfType<UITutorial05Director>()
            .transform.GetChild(0).GetChild(19).gameObject.GetComponent<UIDialogue>();

        //this.dialogue = dialogue;
    }

    private void OnDestroy()
    {   
        //�̺�Ʈ �ߺ� ����, �̺�Ʈ ����
        EventDispatcher.instance.RemoveEventHandler<int>((int)LHMEventType.eEventType.GET_BOOK_ITEM, this.GetBookItemEventHandler);
    }
}
