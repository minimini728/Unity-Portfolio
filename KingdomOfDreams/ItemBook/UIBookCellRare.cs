using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBookCellRare : MonoBehaviour
{
    public Image imgRare1; //���� ������1 �̹���
    public Image imgRare2; //���� ������2 �̹���
    public Button btnClaim; //���� ȹ�� ��ư
    public Image deem1; //���� ������1 �̹��� deem
    public Image deem2; //���� ������2 �̹��� deem
    public Image deem3; //���� ������ �̹��� deem
    public Image deem4; //���� ��ư �̹��� deem
    public Image imgThema; //�ൿ �̹���
    public Image imgReward; //���� �̹���

    private bool isClaim = false; //���� ȹ�� ����/ ���� ���� �Ǻ�
    private BookData fieldData;
    private int stageNum; //���� �������� ��ȣ

    void Start()
    {
        this.btnClaim.interactable = isClaim;

        this.btnClaim.onClick.AddListener(() => //���� ȹ�� ��ư
        {
            Debug.LogFormat("���� {0}�� �ޱ�", fieldData.reward_count);

            //���� �������� ���� ȹ������ ����
            var claimData = InfoManager.instance.BookInfos.Find(x => x.id == fieldData.id);
            claimData.claim = 1;

            //���� ���� (�ڵ����� ���� 15�� ���� �Ǵ� �� 600��)
            var dataTable = DataManager.instance.GetBookDatas().Find(x => x.id == fieldData.id);
            if (dataTable.id == 5002 || dataTable.id == 5005 || dataTable.id == 5008 || dataTable.id == 5011 || dataTable.id == 5014)
            {
                var itemInfo = InfoManager.instance.IngredientInfos.Find(x => x.id == dataTable.reward_id);
                if(itemInfo.auto == 5)
                {
                    itemInfo.auto += dataTable.reward_count;
                }
                else if(itemInfo.auto == 1)
                {
                    itemInfo.auto = dataTable.reward_count;

                }

                InfoManager.instance.SaveIngredientInfos();

                //�κ��丮 UI Refresh �̺�Ʈ ���� -> UIInventoryScroll Ŭ������
                EventDispatcher.instance.SendEvent((int)LHMEventType.eEventType.REFRESH_UI_INVENTORY);
                //���� ������ �̸� �����ϴ� �̺�Ʈ -> UIBook Ŭ������
                EventDispatcher.instance.SendEvent<string>((int)LHMEventType.eEventType.CLAIM_BOOK_ITEM, "�ڵ����� 15�� ����");

            }
            else if(dataTable.id == 5003 || dataTable.id == 5006 || dataTable.id == 5009 || dataTable.id == 50012 || dataTable.id == 50015)
            {
                //���� �������� �޾ƿ���
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

                //�� ���� �ޱ�
                switch (this.stageNum)
                {
                    case 4:
                        var mainGo5 = GameObject.FindObjectOfType<Tutorial05Main>();
                        mainGo5.GetDream(600);
                        break;
                    case 5:
                        var mainGo6 = GameObject.FindObjectOfType<Stage06Main>();
                        mainGo6.GetDream(600);
                        break;
                    case 6:
                        var mainGo7 = GameObject.FindObjectOfType<Stage07Main>();
                        mainGo7.GetDream(600);
                        break;
                    case 7:
                        var mainGo8 = GameObject.FindObjectOfType<Stage08Main>();
                        mainGo8.GetDream(600);
                        break;
                }

                InfoManager.instance.SaveBookInfo();

                //���� ������ �̸� �����ϴ� �̺�Ʈ -> UIBook Ŭ������
                EventDispatcher.instance.SendEvent<string>((int)LHMEventType.eEventType.CLAIM_BOOK_ITEM, "�� 600��");

            }

            InfoManager.instance.SaveBookInfo();

            this.isClaim = false;
            this.btnClaim.interactable = isClaim;


        });
    }

    public void Init(BookData data)
    {
        this.fieldData = data;

        var atlas = AtlasManager.instance.GetAtlasByName("book"); //���� ��Ʋ�� ��������

        //book_data, book_item_data ������ ���̺� ��Ī �۾�
        BookItemData rareItem1 = DataManager.instance.dicBookItemData[data.rare1_id]; //���� ���� �� ���� ������1 id = ���� ������ id
        BookItemData rareItem2 = DataManager.instance.dicBookItemData[data.rare2_id]; //���� ���� �� ���� ������2 id = ���� ������ id

        this.imgRare1.sprite = atlas.GetSprite(rareItem1.sprite_name);
        this.imgRare2.sprite = atlas.GetSprite(rareItem2.sprite_name);

        //�ൿ UI
        switch (data.action)
        {
            case 2: //����
                this.imgThema.sprite = atlas.GetSprite("logging"); //�ൿ �̹���
                this.imgReward.sprite = atlas.GetSprite("equip_shield_wood"); //���� �̹���
                break;
            case 3: //����
                this.imgThema.sprite = atlas.GetSprite("fishing");
                this.imgReward.sprite = atlas.GetSprite("equip_fish");
                break;
            case 4: //ä��
                this.imgThema.sprite = atlas.GetSprite("gathering");
                this.imgReward.sprite = atlas.GetSprite("cabbage");
                break;
            case 5: //����
                this.imgThema.sprite = atlas.GetSprite("mining");
                this.imgReward.sprite = atlas.GetSprite("equip_stone");
                break;
            case 6: //���
                this.imgThema.sprite = atlas.GetSprite("hunting");
                this.imgReward.sprite = atlas.GetSprite("icon_itemicon_meat");
                break;

        }

        //������ UI
        for (int i = 0; i < InfoManager.instance.BookItemInfos.Count; i++)
        {
            var info = InfoManager.instance.BookItemInfos[i];

            if (info.id == data.rare1_id && info.exist == 1)
            {
                this.deem1.gameObject.SetActive(false);
            }
            else if (info.id == data.rare2_id && info.exist == 1)
            {
                this.deem2.gameObject.SetActive(false);
            }
        }

        //����, Claim��ư UI
        var infoRare1 = InfoManager.instance.BookItemInfos.Find(x => x.id == data.rare1_id);
        var infoRare2 = InfoManager.instance.BookItemInfos.Find(x => x.id == data.rare2_id);
        if (infoRare1 != null && infoRare2 != null && infoRare1.exist == 1 && infoRare2.exist == 1)
        {
            this.deem3.gameObject.SetActive(false);
            this.deem4.gameObject.SetActive(false);

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
