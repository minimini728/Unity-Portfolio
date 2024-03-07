using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMagicTool : MonoBehaviour
{
    public Button btnClose; //�ݱ� ��ư
    public UIPieces pieces; //���� ���� UI
    //�׽�Ʈ ��ư
    public Button btnTest1;
    public Button btnTest2;
    public Button btnTest3;

    public Button btnMagicTool; //���� ���� ����, ������ ��ư
    public Image imgExclaimIcon; //������ ����ǥ ��ư
    public Text txtDetail; //���� ���� ȿ�� �ؽ�Ʈ

    void Start()
    {
        //�ʱ� UI ����
        if (InfoManager.instance.MagicToolInfo.Find(x => x.id == 300).level == 0)
        {
            this.pieces.deem.gameObject.SetActive(true);
            this.pieces.txtLevel.gameObject.SetActive(false);
        }
        else
        {
            this.pieces.deem.gameObject.SetActive(false);
            this.pieces.txtLevel.gameObject.SetActive(true);
            this.pieces.txtLevel.text = InfoManager.instance.MagicToolInfo.Find(x => x.id == 300).level.ToString();
        }

        this.TxtDetailRefresh(); //�ؽ�Ʈ UI ����
        this.CheckMagicToolLevel(); //�������� ���� üũ�Ͽ� ��ư Ȱ��ȭ, ����ǥ ǥ��

        this.btnMagicTool.onClick.AddListener(() => //�������� ����, ������ ��ư
        {
            this.imgExclaimIcon.gameObject.SetActive(false); //����ǥ ��Ȱ��ȭ
            this.pieces.deem.gameObject.SetActive(false); //��ο� ������ ��Ȱ��ȭ

            this.pieces.ResetUIPiece(); //���� ���� UI refresh

            MagicToolInfo info = InfoManager.instance.MagicToolInfo.Find(x => x.id == 300); //�������� ������ ��������
            if (info != null && info.level == 0) //�������� ������ 0�� ��� ��, �������� ���� ���� ���
            {
                //�������� ���� �̺�Ʈ ���� -> MagicTool Ŭ������
                EventDispatcher.instance.SendEvent((int)LHMEventType.eEventType.CREATED_MAGIC_TOOL);
                this.TxtDetailRefresh(); //�ؽ�Ʈ UI ����
            }
            else //���� ������ �ִ� ���
            {   
                //�������� ������ �̺�Ʈ ���� -> MagicTool Ŭ������
                EventDispatcher.instance.SendEvent((int)LHMEventType.eEventType.UPGRADE_MAGIC_TOOL);
                this.TxtDetailRefresh(); //�ؽ�Ʈ UI ����
            }

            this.pieces.Refresh(); //���� ���� ������ UI refresh
            this.pieces.txtLevel.gameObject.SetActive(true); //�������� ���� �ؽ�Ʈ Ȱ��ȭ
            this.pieces.txtLevel.text = InfoManager.instance.MagicToolInfo.Find(x => x.id == 300).level.ToString(); //�������� ���� �ʱ�ȭ
            this.btnMagicTool.GetComponent<Button>().interactable = false; //��ư ��Ȱ��ȭ

            InfoManager.instance.SaveDreamPieceInfo(); //���� ���� �����ϱ�

            this.CheckMagicToolLevel(); //���� ������ ���� ����

        });

        //�������� ������ ���� ���� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)LHMEventType.eEventType.CHECK_MAGICTOOL_LEVEL, new EventHandler((type) =>
        {
            this.CheckMagicToolLevel();
        }));

        

        //----------------�׽���------------------------------------------------------------------------

        this.btnTest1.onClick.AddListener(() =>
        {
            for (int i = 0; i < 147; i++)
            {
                EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_DREAM_PIECE, 600);


                this.CheckMagicToolLevel();

                InfoManager.instance.SaveDreamPieceInfo();
            }
        });

        this.btnTest2.onClick.AddListener(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_DREAM_PIECE, 600);
                EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_DREAM_PIECE, 601);
                EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_DREAM_PIECE, 602);
                EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_DREAM_PIECE, 603);

                this.CheckMagicToolLevel();

                InfoManager.instance.SaveDreamPieceInfo();
            }
        });

        this.btnTest3.onClick.AddListener(() =>
        {
            for (int i = 0; i < 1; i++)
            {
                EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_DREAM_PIECE, 600);
                EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_DREAM_PIECE, 601);
                EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_DREAM_PIECE, 602);
                EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_DREAM_PIECE, 603);

                this.CheckMagicToolLevel();

                InfoManager.instance.SaveDreamPieceInfo();
            }
        });

        //-------------------------------------------------------------------------------------------------
        this.btnClose.onClick.AddListener(() => //�ݱ� ��ư
        {
            this.gameObject.SetActive(false);
        });

    }

    public void Init() //�ʱ�ȭ
    {
        this.pieces.Init(); //���� ���� UI �ʱ�ȭ
    }

    public void CheckMagicToolLevel() //������ ���� ���� Ȯ�� �޼���
    {
        var myLevel = InfoManager.instance.MagicToolInfo.Find(x => x.id == 300).level;
        var data = DataManager.instance.GetMagicToolLevelDatas().Find(x => x.level == myLevel + 1);

        if(data != null)
        {
            if (InfoManager.instance.DreamPieceInfo.Find(x => x.id == 600).amount >= data.magic_piece_require
                    && InfoManager.instance.DreamPieceInfo.Find(x => x.id == 601).amount >= data.speed_piece_require
                    && InfoManager.instance.DreamPieceInfo.Find(x => x.id == 602).amount >= data.detox_piece_require
                    && InfoManager.instance.DreamPieceInfo.Find(x => x.id == 603).amount >= data.wisdom_piece_require)
            {
                this.btnMagicTool.GetComponent<Button>().interactable = true;
                this.imgExclaimIcon.gameObject.SetActive(true);
            }

        }
    }

    public void TxtDetailRefresh() //���� ���� ȿ�� �ʱ�ȭ �޼���
    {
        var myLevel = InfoManager.instance.MagicToolInfo.Find(x => x.id == 300).level;
        var data = DataManager.instance.GetMagicToolLevelDatas().Find(x => x.level == myLevel);
        if(data != null)
        {
            this.txtDetail.text = string.Format("�������� ����: {0}�ܰ�\n\nȿ��\n\n����: ȹ�淮 {1}�� ����\n\n�ż�: �̵� �ӵ� {2} % ����\n\n��ȭ: ��, �� ����� ���� {3} % ����\n\n����: ���� ���� ������ ȹ�� ���� ȹ�� Ȯ�� {4} % ����"
                 , myLevel, data.add_magic_property, data.add_speed_property, data.add_detox_property, data.add_wisdom_property);
        }
    }
}
