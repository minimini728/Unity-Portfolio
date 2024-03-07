using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryScroll : MonoBehaviour
{
    public Transform content; //��ũ�Ѻ� ��ġ
    public GameObject itemCellPrefab; //��ũ�Ѻ信 �� cell

    public void Start()
    {
        
    }
    public void Init() //�ʱ�ȭ
    {
        for (int i = 0; i < InfoManager.instance.IngredientInfos.Count; i++) //���� �κ��丮 ��ȸ�ؼ� ��ũ�Ѻ信 ������ cell ���̱�
        {
            var go = Instantiate(this.itemCellPrefab, this.content);
            var itemCell = go.GetComponent<UIInventoryCell>();

            //id, ������, ����
            //itemCell.Init();
            var info = InfoManager.instance.IngredientInfos[i];
            var data = DataManager.instance.GetIngredientData(info.id);
            var atlas = AtlasManager.instance.GetAtlasByName("inventoryItem");
            var sprite = atlas.GetSprite(data.sprite_name);
            var amount = info.amount;
            itemCell.Init(info.id, data.name, sprite, amount);

        }

        //��ũ�Ѻ� UI refresh �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)LHMEventType.eEventType.REFRESH_UI_INVENTORY, new EventHandler((type) =>
        {
            this.Refresh();
        }));
    }

    public void Refresh() //��ũ�Ѻ� UI refresh �޼���
    {
        if (this.content != null)
        {
            foreach (Transform child in this.content) //��ũ�Ѻ信 �ִ� ������ cell ����
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < InfoManager.instance.IngredientInfos.Count; i++) //�ٽ� ���̱�
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
