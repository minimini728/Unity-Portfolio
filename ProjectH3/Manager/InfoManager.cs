using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class InfoManager
{
    public static readonly InfoManager instance = new InfoManager();

    public string playerPath = string.Format("{0}/player_info.json", Application.persistentDataPath); //플레이어 정보 주소
    public string inventoryPath = string.Format("{0}/inventory_info.json", Application.persistentDataPath); //인벤토리 정보 주소
    public string outpostPath = string.Format("{0}/outpost_info.json", Application.persistentDataPath); //전초기지 정보 주소
    public PlayerInfo PlayerInfo { get; set; } //플레이어 관련 인포 속성
    public OutPostInfo OutPostInfos { get; set; } //전초기지 관련 인포 속성

    public List<InventoryInfo> InventoryInfos { get; set; } //인벤토리 관련 인포 리스트

    private InfoManager() { }
    public bool IsNewbie(string path) //뉴비 체크
    {
        bool existFile = File.Exists(path);
        //Debug.LogFormat("Exists: {0}", existFile);
        return !existFile;
    }
    public void LoadPlayerInfo() //플레이어 인포 로드
    {
        var json = File.ReadAllText(playerPath);
        //역직렬화 
        this.PlayerInfo = JsonConvert.DeserializeObject<PlayerInfo>(json);
        
        Debug.Log("<color=yellow>[load success] player_info.json</color>");
    }

    public void SavePlayerInfo() //플레이어 인포 저장
    {   
        //직렬화
        var json = JsonConvert.SerializeObject(this.PlayerInfo);
        File.WriteAllText(playerPath, json);
        
        Debug.Log("<color=yellow>[save success] player_info.json</color>");
    }

    public void InventoryInfoInit() //인벤토리 생성 및 저장
    {
        this.InventoryInfos = new List<InventoryInfo>();

        for (int i = 100; i < 104; i++)
        {
            InventoryInfo info = new InventoryInfo(i);
            InfoManager.instance.InventoryInfos.Add(info);
        }

        this.SaveInventoryInfos();
    }

    public InventoryInfo GetInventoryInfo(int id)
    {
        var info = InfoManager.instance.InventoryInfos.Find(x => x.id == id);
        return info;
    }

    public void LoadInventoryInfo() //저장된 인벤토리 불러오기
    {
        string json = File.ReadAllText(inventoryPath);

        //역직렬화
        var inventoryinfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(json);

        InfoManager.instance.InventoryInfos = inventoryinfo;

        Debug.Log("<color=yellow>[load success] invnetory_info.json</color>");
    }

    public void SaveInventoryInfos() //인벤토리 저장하기
    {
        //직렬화
        var json = JsonConvert.SerializeObject(this.InventoryInfos);

        File.WriteAllText(inventoryPath, json);

        Debug.Log("<color=yellow>[save success] invnetory_info.json</color>");
    }

    public void LoadOutPostInfo() //전초기지 건설 여부 불러오기
    {
        string json = File.ReadAllText(outpostPath);

        //역직렬화
        var outpostInfo = JsonConvert.DeserializeObject<OutPostInfo>(json);

        InfoManager.instance.OutPostInfos = outpostInfo;

        Debug.Log("<color=yellow>[load success] outpost_info.json</color>");
    }

    public void SaveOutPostInfo() //전초기지 건설 여부 저장하기
    {
        //직렬화
        var json = JsonConvert.SerializeObject(this.OutPostInfos);

        File.WriteAllText(outpostPath, json);

        Debug.Log("<color=yellow>[save success] outpost_info.json</color>");

    }
}
