using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{   
    //������ ���� txt��
    public Text txtEnergyCrystalAmount;
    public Text txtMetalAlloyAmount;
    public Text txtBioSampleAmount;
    public Text txtAdvECAmount;

    public void Init() //�ʱ�ȭ (�� Director���� ȣ��)
    {
        //UI �κ��丮 ������Ʈ �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.RefreshInventoryUI, this.RefreshUI);

        //UI �κ��丮 ������Ʈ �ʱ� ����
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshInventoryUI);

    }

    void RefreshUI(short type)
    {
        Debug.Log("<color=red>��������</color>");

        if (this.txtEnergyCrystalAmount != null) //��üũ
            //�κ��丮���� ������ ���� ������ ã�Ƽ� string���� ��ȯ
            this.txtEnergyCrystalAmount.text = InfoManager.instance.InventoryInfos.Find(x => x.id == 100).amount.ToString();
        if (this.txtMetalAlloyAmount != null)
            this.txtMetalAlloyAmount.text = InfoManager.instance.InventoryInfos.Find(x => x.id == 101).amount.ToString();
        if (this.txtBioSampleAmount != null)
            this.txtBioSampleAmount.text = InfoManager.instance.InventoryInfos.Find(x => x.id == 102).amount.ToString();
        if (this.txtAdvECAmount != null)
            this.txtAdvECAmount.text = InfoManager.instance.InventoryInfos.Find(x => x.id == 103).amount.ToString();

    }

    private void OnDisable()
    {
        Destroy(this);
        //�̺�Ʈ �ߺ� ���� �̺�Ʈ ����
        //EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.RefreshInventoryUI, this.RefreshUI);
    }

}
