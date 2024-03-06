using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    void Start()
    {   
        //인벤토리 획득 이벤트 등록
        EventDispatcher.instance.AddEventHandler<int>((int)EventEnum.eEventType.GetItem, this.GetItem);
    }

    void GetItem(short type, int num) //인벤토리 획득 메서드
    {
        var data = DataManager.instance.GetInventoryData(num); //num과 동일한 id인 인벤토리 데이터 가져오기

        var id = data.id; //id를 int로 변환
        var foundInfo = InfoManager.instance.InventoryInfos.Find(x => x.id == id); //인벤토리에서 id와 동일한 아이템 찾기

        if(foundInfo == null) //아이템 처음 획득
        {
            InventoryInfo info = new InventoryInfo(id);
            InfoManager.instance.InventoryInfos.Add(info);
        }
        else
        {
            foundInfo.amount++; //아이템 갯수 증가
        }

        InfoManager.instance.SaveInventoryInfos(); //인벤토리 저장

        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshInventoryUI); //UI 인벤토리 업데이트 이벤트 호출
    }

    private void OnDisable()
    {   
        //이벤트 중복 방지를 위한 이벤트 제거
        EventDispatcher.instance.RemoveEventHandler<int>((int)EventEnum.eEventType.GetItem, this.GetItem);
    }
}
