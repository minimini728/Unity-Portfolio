using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public UIDialogue dialogue;

    public void Init()
    {   
        //도감 아이템 획득 이벤트 등록
        EventDispatcher.instance.AddEventHandler<int>((int)LHMEventType.eEventType.GET_BOOK_ITEM, this.GetBookItemEventHandler);
    }

    //도감 아이템 획득 이벤트 a: 도감 아이템 id
    private void GetBookItemEventHandler(short type, int a)
    {
        this.GetBookItem(a); //도감 획득 메서드

        if(a == 6001)
        {
            this.FindDialogue();

            var data = DataManager.instance.GetDialogueData(10096);
            InfoManager.instance.DialogueInfo.id = 10096;
            this.dialogue.gameObject.SetActive(true);
            this.dialogue.Init(data);
        }
    }

    //도감 아이템 획득 메서드 num: 도감 아이템 id
    public void GetBookItem(int num)
    {
        var data = DataManager.instance.GetBookItemData(num); //도감 아이템 데이터에서 num과 같은 id인 도감 아이템 정보 가져오기

        //Debug.Log("도감 아이템 |" + data.name + "| 획득");

        var id = data.id; //id를 int로 저장
        var foundInfo = InfoManager.instance.BookItemInfos.Find(x => x.id == id); //보유 도감 아이템에서 찾기

        if (foundInfo == null) //없는 경우
        {
            BookItemInfo info = new BookItemInfo(id, 1); //해당 id로 인스턴스 생성
            InfoManager.instance.BookItemInfos.Add(info); //보유 도감에 넣기

            InfoManager.instance.SaveBookItemInfo(); //보유 도감 저장하기

            //BookUI refresh 이벤트 호출
            EventDispatcher.instance.SendEvent((int)LHMEventType.eEventType.REFRESH_UI_BOOK);
            //느낌표 표시 이벤트 호출
            EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.EXCLAIM_ICON_BOOK_ITEM, 1);

        }
        else
        {
            return; //이미 획득
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
        //이벤트 중복 방지, 이벤트 제거
        EventDispatcher.instance.RemoveEventHandler<int>((int)LHMEventType.eEventType.GET_BOOK_ITEM, this.GetBookItemEventHandler);
    }
}
