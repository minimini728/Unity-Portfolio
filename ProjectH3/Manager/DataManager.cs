using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Linq;


public class DataManager
{
    public static readonly DataManager instance = new DataManager();

    private DataManager() { }

    private Dictionary<int, InventoryData> dicInventoryData; //�κ��丮 ��ųʸ�

    public void LoadInventoryData() //�κ��丮 ������ �ҷ�����
    {
        TextAsset asset = Resources.Load<TextAsset>("Data/inventory_data");
        var json = asset.text;
        Debug.Log(json);

        //������ȭ
        InventoryData[] arrItemDatas = JsonConvert.DeserializeObject<InventoryData[]>(json);

        this.dicInventoryData = arrItemDatas.ToDictionary(x => x.id);

        Debug.LogFormat("iventory data loaded: {0}", this.dicInventoryData.Count);
    }

    public InventoryData GetInventoryData(int id) //�κ��丮 ��ųʸ� ã��
    {
        return this.dicInventoryData[id];
    }

    public List<InventoryData> GetInventoryDatas()
    {
        return this.dicInventoryData.Values.ToList();
    }
}
