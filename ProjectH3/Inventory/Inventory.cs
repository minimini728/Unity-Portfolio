using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    void Start()
    {   
        //�κ��丮 ȹ�� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler<int>((int)EventEnum.eEventType.GetItem, this.GetItem);
    }

    void GetItem(short type, int num) //�κ��丮 ȹ�� �޼���
    {
        var data = DataManager.instance.GetInventoryData(num); //num�� ������ id�� �κ��丮 ������ ��������

        var id = data.id; //id�� int�� ��ȯ
        var foundInfo = InfoManager.instance.InventoryInfos.Find(x => x.id == id); //�κ��丮���� id�� ������ ������ ã��

        if(foundInfo == null) //������ ó�� ȹ��
        {
            InventoryInfo info = new InventoryInfo(id);
            InfoManager.instance.InventoryInfos.Add(info);
        }
        else
        {
            foundInfo.amount++; //������ ���� ����
        }

        InfoManager.instance.SaveInventoryInfos(); //�κ��丮 ����

        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshInventoryUI); //UI �κ��丮 ������Ʈ �̺�Ʈ ȣ��
    }

    private void OnDisable()
    {   
        //�̺�Ʈ �ߺ� ������ ���� �̺�Ʈ ����
        EventDispatcher.instance.RemoveEventHandler<int>((int)EventEnum.eEventType.GetItem, this.GetItem);
    }
}
