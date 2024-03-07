using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBookCellNormal : MonoBehaviour
{
    public Image imgNormal1; //��� ������1 �̹���
    public Image imgNormal2; //��� ������2 �̹���
    public Image imgNormal3; //��� ������3 �̹���
    public Button btnClaim; //���� ȹ�� ��ư
    public Image deem1; //��� ������1 �̹��� deem
    public Image deem2; //��� ������2 �̹��� deem
    public Image deem3; //��� ������3 �̹��� deem
    public Image deem4; //���� ������ �̹��� deem
    public Image deem5; //���� ��ư �̹��� deem
    public Image imgThema; //�ൿ �̹���
    public Image imgReward; //���� �̹���

    private bool isClaim = false; //���� ȹ�� ����/ ���� ���� �Ǻ�
    private BookData fieldData;
    void Start()
    {
        this.btnClaim.interactable = isClaim;

        this.btnClaim.onClick.AddListener(() => //���� ȹ�� ��ư
        {
            Debug.LogFormat("���� {0}�� �ޱ�", fieldData.reward_count);

            //���� �������� ���� ȹ������ ����
            var dataInfo = InfoManager.instance.BookInfos.Find(x => x.id == fieldData.id);
            dataInfo.claim = 1;

            //�ڵ����� ���� ����
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

            //�κ��丮 UI Refresh �̺�Ʈ ���� -> UIInventoryScroll Ŭ������
            EventDispatcher.instance.SendEvent((int)LHMEventType.eEventType.REFRESH_UI_INVENTORY);
            //���� ������ �̸� �����ϴ� �̺�Ʈ -> UIBook Ŭ������
            EventDispatcher.instance.SendEvent<string>((int)LHMEventType.eEventType.CLAIM_BOOK_ITEM, "�ڵ����� 5�� ����");
        });
    }

    public void Init(BookData data)
    {
        this.fieldData = data;

        var atlas = AtlasManager.instance.GetAtlasByName("book"); //���� ��Ʋ�� ��������

        //book_data, book_item_data ������ ���̺� ��Ī �۾�
        BookItemData normalItem1 = DataManager.instance.dicBookItemData[data.normal1_id]; //���� ��� �� ��� ������1 id = ���� ������ id
        BookItemData normalItem2 = DataManager.instance.dicBookItemData[data.normal2_id]; //���� ��� �� ��� ������2 id = ���� ������ id
        BookItemData normalItem3 = DataManager.instance.dicBookItemData[data.normal3_id]; //���� ��� �� ��� ������3 id = ���� ������ id

        this.imgNormal1.sprite = atlas.GetSprite(normalItem1.sprite_name);
        this.imgNormal2.sprite = atlas.GetSprite(normalItem2.sprite_name);
        this.imgNormal3.sprite = atlas.GetSprite(normalItem3.sprite_name);

        //�ൿ �̹���
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

        //����, Claim��ư UI
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
