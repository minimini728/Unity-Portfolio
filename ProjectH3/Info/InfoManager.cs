using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class InfoManager
{
    public static readonly InfoManager instance = new InfoManager();

    public string playerPath = string.Format("{0}/player_info.json", Application.persistentDataPath); //�÷��̾� ���� �ּ�
    public string inventoryPath = string.Format("{0}/inventory_info.json", Application.persistentDataPath); //�κ��丮 ���� �ּ�
    public string outpostPath = string.Format("{0}/outpost_info.json", Application.persistentDataPath); //���ʱ��� ���� �ּ�
    public PlayerInfo PlayerInfo { get; set; } //�÷��̾� ���� ���� �Ӽ�
    public OutPostInfo OutPostInfos { get; set; } //���ʱ��� ���� ���� �Ӽ�

    public List<InventoryInfo> InventoryInfos { get; set; } //�κ��丮 ���� ���� ����Ʈ

    private InfoManager() { }
    public bool IsNewbie(string path) //���� üũ
    {
        bool existFile = File.Exists(path);
        //Debug.LogFormat("Exists: {0}", existFile);
        return !existFile;
    }
    public void LoadPlayerInfo() //�÷��̾� ���� �ε�
    {
        var json = File.ReadAllText(playerPath);
        //������ȭ 
        this.PlayerInfo = JsonConvert.DeserializeObject<PlayerInfo>(json);
        
        Debug.Log("<color=yellow>[load success] player_info.json</color>");
    }

    public void SavePlayerInfo() //�÷��̾� ���� ����
    {   
        //����ȭ
        var json = JsonConvert.SerializeObject(this.PlayerInfo);
        File.WriteAllText(playerPath, json);
        
        Debug.Log("<color=yellow>[save success] player_info.json</color>");
    }

    public void InventoryInfoInit() //�κ��丮 ���� �� ����
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

    public void LoadInventoryInfo() //����� �κ��丮 �ҷ�����
    {
        string json = File.ReadAllText(inventoryPath);

        //������ȭ
        var inventoryinfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(json);

        InfoManager.instance.InventoryInfos = inventoryinfo;

        Debug.Log("<color=yellow>[load success] invnetory_info.json</color>");
    }

    public void SaveInventoryInfos() //�κ��丮 �����ϱ�
    {
        //����ȭ
        var json = JsonConvert.SerializeObject(this.InventoryInfos);

        File.WriteAllText(inventoryPath, json);

        Debug.Log("<color=yellow>[save success] invnetory_info.json</color>");
    }

    public void LoadOutPostInfo() //���ʱ��� �Ǽ� ���� �ҷ�����
    {
        string json = File.ReadAllText(outpostPath);

        //������ȭ
        var outpostInfo = JsonConvert.DeserializeObject<OutPostInfo>(json);

        InfoManager.instance.OutPostInfos = outpostInfo;

        Debug.Log("<color=yellow>[load success] outpost_info.json</color>");
    }

    public void SaveOutPostInfo() //���ʱ��� �Ǽ� ���� �����ϱ�
    {
        //����ȭ
        var json = JsonConvert.SerializeObject(this.OutPostInfos);

        File.WriteAllText(outpostPath, json);

        Debug.Log("<color=yellow>[save success] outpost_info.json</color>");

    }
}
