using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBookScrollView : MonoBehaviour
{
    public Transform contentTrans; //행 생성 위치
    public GameObject bookCellGo; //튜토리얼 도감 행
    public GameObject bookNormalCellGo; //normal 도감 행
    public GameObject bookRareCellGo; //rare 도감 행

    public void Init() //초기화
    {
        List<BookData> list = DataManager.instance.GetBookDatas(); //도감 데이터 가져오기

        foreach (BookData data in list) 
        {
            switch (data.reward_count) //보상 아이템에 따라 도감 행 구별
            {
                case 30:
                    this.AddBookCell(data); //튜토리얼 행
                    break;

                case 5:
                    this.AddBookNormalCell(data); //노멀 행
                    break;

                case 15:
                    this.AddBookRareCell(data); //레어1 행
                    break;

                case 600:
                    this.AddBookRareCell(data); //레어2 행
                    break;

            }
        }

        //도감 UI 스크롤뷰 refresh 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)LHMEventType.eEventType.REFRESH_UI_BOOK, new EventHandler((type) =>
        {
            this.Refresh();
        }));
    }

    public void AddBookCell(BookData data) //튜토리얼 도감 동적 생성 메서드
    {
        var go = Instantiate(this.bookCellGo, this.contentTrans);
        UIBookCell bookCell = go.GetComponent<UIBookCell>();
        bookCell.Init(data);
    }

    public void AddBookNormalCell(BookData data) //노멀 도감 동적 생성 메서드
    {
        var go = Instantiate(this.bookNormalCellGo, this.contentTrans);
        UIBookCellNormal bookNormalCell = go.GetComponent<UIBookCellNormal>();
        bookNormalCell.Init(data);
    }

    public void AddBookRareCell(BookData data) //레어 도감 동적 생성 메서드
    {
        var go = Instantiate(this.bookRareCellGo, this.contentTrans);
        UIBookCellRare bookRareCell = go.GetComponent<UIBookCellRare>();
        bookRareCell.Init(data);
    }

    public void Refresh() //스크롤뷰 refresh 메서드
    {   
        if(this.contentTrans != null)
        {
            foreach (Transform child in this.contentTrans) //제거했다가
            {
                Destroy(child.gameObject);
            }

            List<BookData> list = DataManager.instance.GetBookDatas();

            foreach (BookData data in list) //다시 붙이기
            {
                switch (data.reward_count)
                {
                    case 30:
                        this.AddBookCell(data);
                        break;

                    case 5:
                        this.AddBookNormalCell(data);
                        break;

                    case 15:
                        this.AddBookRareCell(data);
                        break;

                    case 600:
                        this.AddBookRareCell(data);
                        break;

                }
            }

        }

    }
}
