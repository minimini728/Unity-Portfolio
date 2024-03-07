using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBookCell : MonoBehaviour
{
    public Image imgRare; //���� ������ �̹���
    public Image imgNormal; //��� ������ �̹���
    public Button btnClaim; //���� ȹ�� ��ư
    public Image deem1; //���� ������ �̹��� deem
    public Image deem2; //��� ������ �̹��� deem
    public Image deem3; //���� ������ �̹��� deem
    public Image deem4; //ȹ�� ��ư �̹��� deem
    public Image imgThema; //���� �̹���

    private int stageNum; //���� �������� ��ȣ
    private bool isClaim = false; //���� ȹ�� ����/ ���� ���� �Ǻ�

    void Start()
    {
        this.btnClaim.interactable = isClaim;

        this.btnClaim.onClick.AddListener(() => //���� ȹ�� ��ư
        {
            Debug.LogFormat("�� {0}�� ȹ��", DataManager.instance.GetBookDatas().Find(x => x.id == 5000).reward_count);
            
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@����ޱ� �ڵ�@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //���� �������� ��ȣ �޾ƿ���
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

            var claimData = InfoManager.instance.BookInfos.Find(x => x.id == 5000); //Ʃ�丮�� ���� ���� �� ���� ã��
            claimData.claim = 1; //ȹ������ ���� 0:ȹ��X, 1:ȹ��0

            InfoManager.instance.SaveBookInfo(); //���� ���� �����ϱ�

            //ȹ���ϸ� ��ư ��Ȱ��ȭ
            this.isClaim = false; 
            this.btnClaim.interactable = isClaim;

            //���� ������ �̸� �����ϴ� �̺�Ʈ -> UIBook Ŭ������
            EventDispatcher.instance.SendEvent<string>((int)LHMEventType.eEventType.CLAIM_BOOK_ITEM, "�� 30��");
        });

    }
    public void Init(BookData data) //�ʱ�ȭ
    {
        var atlas = AtlasManager.instance.GetAtlasByName("book"); //���� ��Ʋ�� ��������

        //book_data, book_item_data ������ ���̺� ��Ī �۾�
        BookItemData rareItem = DataManager.instance.dicBookItemData[data.rare1_id]; //���� Ʃ�丮�� �� ���� ������ id = ���� ������ id
        BookItemData normalItem = DataManager.instance.dicBookItemData[data.normal1_id]; //���� Ʃ�丮�� �� ��� ������ id = ���� ������ id

        this.imgRare.sprite = atlas.GetSprite(rareItem.sprite_name); 
        this.imgNormal.sprite = atlas.GetSprite(normalItem.sprite_name);
        this.imgThema.sprite = atlas.GetSprite("tutorial");

        for (int i = 0; i < InfoManager.instance.BookItemInfos.Count; i++) //���� ���� ��ȸ�ϸ鼭 ȹ�� ���� deem Ȱ��ȭ/��Ȱ��ȭ
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

                if (InfoManager.instance.BookInfos.Find(x => x.id == 5000).claim != 1) //���� ȹ�� ������ �� ��ư Ȱ��ȭ
                {
                    this.isClaim = true;
                    this.btnClaim.interactable = isClaim;
                }

            }

        }

    }

}
