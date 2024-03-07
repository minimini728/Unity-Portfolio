using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBookCell : MonoBehaviour
{
    public Image imgRare; //레어 아이템 이미지
    public Image imgNormal; //노멀 아이템 이미지
    public Button btnClaim; //보상 획득 버튼
    public Image deem1; //레어 아이템 이미지 deem
    public Image deem2; //노멀 아이템 이미지 deem
    public Image deem3; //보상 아이템 이미지 deem
    public Image deem4; //획득 버튼 이미지 deem
    public Image imgThema; //새싹 이미지

    private int stageNum; //현재 스테이지 번호
    private bool isClaim = false; //보상 획득 유무/ 가능 유무 판별

    void Start()
    {
        this.btnClaim.interactable = isClaim;

        this.btnClaim.onClick.AddListener(() => //보상 획득 버튼
        {
            Debug.LogFormat("꿈 {0}개 획득", DataManager.instance.GetBookDatas().Find(x => x.id == 5000).reward_count);
            
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@보상받기 코드@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //현재 스테이지 번호 받아오기
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

            //꿈 보상 받기
            switch (this.stageNum)
            {
                case 4:
                    var mainGo5 = GameObject.FindObjectOfType<Tutorial05Main>();
                    mainGo5.GetDream(30);
                    break;
                case 5:
                    var mainGo6 = GameObject.FindObjectOfType<Stage06Main>();
                    mainGo6.GetDream(30);
                    break;
                case 6:
                    var mainGo7 = GameObject.FindObjectOfType<Stage07Main>();
                    mainGo7.GetDream(30);
                    break;
                case 7:
                    var mainGo8 = GameObject.FindObjectOfType<Stage08Main>();
                    mainGo8.GetDream(30);
                    break;
            }
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            var claimData = InfoManager.instance.BookInfos.Find(x => x.id == 5000); //튜토리얼 보상 도감 행 정보 찾기
            claimData.claim = 1; //획득으로 변경 0:획득X, 1:획득0

            InfoManager.instance.SaveBookInfo(); //보유 도감 저장하기

            //획득하면 버튼 비활성화
            this.isClaim = false; 
            this.btnClaim.interactable = isClaim;

            //보상 아이템 이름 전달하는 이벤트 -> UIBook 클래스로
            EventDispatcher.instance.SendEvent<string>((int)LHMEventType.eEventType.CLAIM_BOOK_ITEM, "꿈 30개");
        });

    }
    public void Init(BookData data) //초기화
    {
        var atlas = AtlasManager.instance.GetAtlasByName("book"); //도감 아틀라스 가져오기

        //book_data, book_item_data 데이터 테이블 매칭 작업
        BookItemData rareItem = DataManager.instance.dicBookItemData[data.rare1_id]; //도감 튜토리얼 행 레어 아이템 id = 도감 아이템 id
        BookItemData normalItem = DataManager.instance.dicBookItemData[data.normal1_id]; //도감 튜토리얼 행 노멀 아이템 id = 도감 아이템 id

        this.imgRare.sprite = atlas.GetSprite(rareItem.sprite_name); 
        this.imgNormal.sprite = atlas.GetSprite(normalItem.sprite_name);
        this.imgThema.sprite = atlas.GetSprite("tutorial");

        for (int i = 0; i < InfoManager.instance.BookItemInfos.Count; i++) //보유 도감 순회하면서 획득 유무 deem 활성화/비활성화
        {
            var info = InfoManager.instance.BookItemInfos[i];
            if (info.id == data.normal1_id && info.exist == 1)
            {
                this.deem2.gameObject.SetActive(false);
            }

            if (info.id == data.rare1_id && info.exist == 1)
            {
                this.deem1.gameObject.SetActive(false);
                this.deem3.gameObject.SetActive(false);
                this.deem4.gameObject.SetActive(false);

                if (InfoManager.instance.BookInfos.Find(x => x.id == 5000).claim != 1) //보상 획득 안했을 시 버튼 활성화
                {
                    this.isClaim = true;
                    this.btnClaim.interactable = isClaim;
                }

            }

        }

    }

}
