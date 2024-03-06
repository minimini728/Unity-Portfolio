using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{   
    //아이템 갯수 txt들
    public Text txtEnergyCrystalAmount;
    public Text txtMetalAlloyAmount;
    public Text txtBioSampleAmount;
    public Text txtAdvECAmount;

    public void Init() //초기화 (각 Director에서 호출)
    {
        //UI 인벤토리 업데이트 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.RefreshInventoryUI, this.RefreshUI);

        //UI 인벤토리 업데이트 초기 설정
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshInventoryUI);

    }

    void RefreshUI(short type)
    {
        Debug.Log("<color=red>리프레쉬</color>");

        if (this.txtEnergyCrystalAmount != null) //널체크
            //인벤토리에서 에너지 결정 갯수를 찾아서 string으로 변환
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
        //이벤트 중복 방지 이벤트 제거
        //EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.RefreshInventoryUI, this.RefreshUI);
    }

}
