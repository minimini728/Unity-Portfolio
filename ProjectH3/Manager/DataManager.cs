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

    private Dictionary<int, InventoryData> dicInventoryData; //인벤토리 딕셔너리

    public void LoadInventoryData() //인벤토리 데이터 불러오기
    {
        TextAsset asset = Resources.Load<TextAsset>("Data/inventory_data");
        var json = asset.text;
        Debug.Log(json);

        //역직렬화
        InventoryData[] arrItemDatas = JsonConvert.DeserializeObject<InventoryData[]>(json);

        this.dicInventoryData = arrItemDatas.ToDictionary(x => x.id);

        Debug.LogFormat("iventory data loaded: {0}", this.dicInventoryData.Count);
    }

    public InventoryData GetInventoryData(int id) //인벤토리 딕셔너리 찾기
    {
        return this.dicInventoryData[id];
    }

    public List<InventoryData> GetInventoryDatas()
    {
        return this.dicInventoryData.Values.ToList();
    }
}
