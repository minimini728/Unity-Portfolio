using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBookCellNormal : MonoBehaviour
{
    public Image imgNormal1; //노멀 아이템1 이미지
    public Image imgNormal2; //노멀 아이템2 이미지
    public Image imgNormal3; //노멀 아이템3 이미지
    public Button btnClaim; //보상 획득 버튼
    public Image deem1; //노멀 아이템1 이미지 deem
    public Image deem2; //노멀 아이템2 이미지 deem
    public Image deem3; //노멀 아이템3 이미지 deem
    public Image deem4; //보상 아이템 이미지 deem
    public Image deem5; //보상 버튼 이미지 deem
    public Image imgThema; //행동 이미지
    public Image imgReward; //보상 이미지

    private bool isClaim = false; //보상 획득 유무/ 가능 유무 판별
    private BookData fieldData;
    void Start()
    {
        this.btnClaim.interactable = isClaim;

        this.btnClaim.onClick.AddListener(() => //보상 획득 버튼
        {
            Debug.LogFormat("보상 {0}개 받기", fieldData.reward_count);

            //보유 도감에서 보상 획득으로 변경
            var dataInfo = InfoManager.instance.BookInfos.Find(x => x.id == fieldData.id);
            dataInfo.claim = 1;

            //자동공급 보상 지급
            var dataTable = DataManager.instance.GetBookDatas().Find(x => x.id == fieldData.id);
            var itemInfo = InfoManager.instance.IngredientInfos.Find(x => x.id == dataTable.reward_id);
            if(itemInfo.auto == 15)
            {
                itemInfo.auto += dataTable.reward_count;
            }
            else
            {
                itemInfo.auto = dataTable.reward_count;
            }

            InfoManager.instance.SaveIngredientInfos();

            InfoManager.instance.SaveBookInfo();

            this.isClaim = false;
            this.btnClaim.interactable = isClaim;

            //인벤토리 UI Refresh 이벤트 전송 -> UIInventoryScroll 클래스로
            EventDispatcher.instance.SendEvent((int)LHMEventType.eEventType.REFRESH_UI_INVENTORY);
            //보상 아이템 이름 전달하는 이벤트 -> UIBook 클래스로
            EventDispatcher.instance.SendEvent<string>((int)LHMEventType.eEventType.CLAIM_BOOK_ITEM, "자동공급 5배 증가");
        });
    }

    public void Init(BookData data)
    {
        this.fieldData = data;

        var atlas = AtlasManager.instance.GetAtlasByName("book"); //도감 아틀라스 가져오기

        //book_data, book_item_data 데이터 테이블 매칭 작업
        BookItemData normalItem1 = DataManager.instance.dicBookItemData[data.normal1_id]; //도감 노멀 행 노멀 아이템1 id = 도감 아이템 id
        BookItemData normalItem2 = DataManager.instance.dicBookItemData[data.normal2_id]; //도감 노멀 행 노멀 아이템2 id = 도감 아이템 id
        BookItemData normalItem3 = DataManager.instance.dicBookItemData[data.normal3_id]; //도감 노멀 행 노멀 아이템3 id = 도감 아이템 id

        this.imgNormal1.sprite = atlas.GetSprite(normalItem1.sprite_name);
        this.imgNormal2.sprite = atlas.GetSprite(normalItem2.sprite_name);
        this.imgNormal3.sprite = atlas.GetSprite(normalItem3.sprite_name);

        //행동 이미지
        switch (data.action)
        {
            case 2: //벌목
                this.imgThema.sprite = atlas.GetSprite("logging"); //행동 이미지
                this.imgReward.sprite = atlas.GetSprite("equip_shield_wood"); //보상 이미지
                break;
            case 3: //낚시
                this.imgThema.sprite = atlas.GetSprite("fishing");
                this.imgReward.sprite = atlas.GetSprite("equip_fish");
                break;
            case 4: //채집
                this.imgThema.sprite = atlas.GetSprite("gathering");
                this.imgReward.sprite = atlas.GetSprite("cabbage");
                break;
            case 5: //광질
                this.imgThema.sprite = atlas.GetSprite("mining");
                this.imgReward.sprite = atlas.GetSprite("equip_stone");
                break;
            case 6: //사냥
                this.imgThema.sprite = atlas.GetSprite("hunting");
                this.imgReward.sprite = atlas.GetSprite("icon_itemicon_meat");
                break;

        }

        //아이템 UI
        for (int i = 0; i < InfoManager.instance.BookItemInfos.Count; i++)
        {
            var info = InfoManager.instance.BookItemInfos[i];

            if (info.id == data.normal1_id && info.exist == 1)
            {
                this.deem1.gameObject.SetActive(false);
            }
            else if (info.id == data.normal2_id && info.exist == 1)
            {
                this.deem2.gameObject.SetActive(false);
            }
            else if (info.id == data.normal3_id && info.exist == 1)
            {
                this.deem3.gameObject.SetActive(false);
            }
        }

        //보상, Claim버튼 UI
        var infoNormal1 = InfoManager.instance.BookItemInfos.Find(x => x.id == data.normal1_id);
        var infoNormal2 = InfoManager.instance.BookItemInfos.Find(x => x.id == data.normal2_id);
        var infoNormal3 = InfoManager.instance.BookItemInfos.Find(x => x.id == data.normal3_id);
        if (infoNormal1 != null && infoNormal2 != null && infoNormal3 != null
            && infoNormal1.exist == 1 && infoNormal2.exist == 1 && infoNormal3.exist == 1)
        {
            this.deem4.gameObject.SetActive(false);
            this.deem5.gameObject.SetActive(false);

            var foundInfo = InfoManager.instance.BookInfos.Find(x => x.id == data.id);
            if (foundInfo == null)
            {
                var TbookInfo = new BookInfo(data.id);
                InfoManager.instance.BookInfos.Add(TbookInfo);
                InfoManager.instance.SaveBookInfo();
            }

            if (InfoManager.instance.BookInfos.Find(x => x.id == data.id).claim != 1)
            {
                this.isClaim = true;
                this.btnClaim.interactable = isClaim;

            }

        }
    }
}
