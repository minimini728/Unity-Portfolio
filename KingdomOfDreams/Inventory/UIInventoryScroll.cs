using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryScroll : MonoBehaviour
{
    public Transform content; //스크롤뷰 위치
    public GameObject itemCellPrefab; //스크롤뷰에 들어갈 cell

    public void Start()
    {
        
    }
    public void Init() //초기화
    {
        for (int i = 0; i < InfoManager.instance.IngredientInfos.Count; i++) //보유 인벤토리 순회해서 스크롤뷰에 아이템 cell 붙이기
        {
            var go = Instantiate(this.itemCellPrefab, this.content);
            var itemCell = go.GetComponent<UIInventoryCell>();

            //id, 아이콘, 수량
            //itemCell.Init();
            var info = InfoManager.instance.IngredientInfos[i];
            var data = DataManager.instance.GetIngredientData(info.id);
            var atlas = AtlasManager.instance.GetAtlasByName("inventoryItem");
            var sprite = atlas.GetSprite(data.sprite_name);
            var amount = info.amount;
            itemCell.Init(info.id, data.name, sprite, amount);

        }

        //스크롤뷰 UI refresh 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)LHMEventType.eEventType.REFRESH_UI_INVENTORY, new EventHandler((type) =>
        {
            this.Refresh();
        }));
    }

    public void Refresh() //스크롤뷰 UI refresh 메서드
    {
        if (this.content != null)
        {
            foreach (Transform child in this.content) //스크롤뷰에 있는 아이템 cell 떼고
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < InfoManager.instance.IngredientInfos.Count; i++) //다시 붙이기
            {
                var go = Instantiate(this.itemCellPrefab, this.content);
                var itemCell = go.GetComponent<UIInventoryCell>();

                var info = InfoManager.instance.IngredientInfos[i];
                var data = DataManager.instance.GetIngredientData(info.id);
                var atlas = AtlasManager.instance.GetAtlasByName("inventoryItem");
                var sprite = atlas.GetSprite(data.sprite_name);
                var amount = info.amount;
                itemCell.Init(info.id, data.name, sprite, amount);

            }
        }
    }
}
