using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class InfoManager
{
    public static readonly InfoManager instance = new InfoManager();

    public string ingredientPath = string.Format("{0}/ingredient_info.json", Application.persistentDataPath);
    public string dreamPath = string.Format("{0}/dream_info.json", Application.persistentDataPath);

    public List<IngredientInfo> IngredientInfos { get; set; }

    public List<BookInfo> BookInfos { get; set; }

    public List<BookItemInfo> BookItemInfos { get; set; }

    public List<DreamPieceInfo> DreamPieceInfo { get; set; }

    public List<MagicToolInfo> MagicToolInfo { get; set; }

    private InfoManager() { }

    //������ ���� �ʱ�ȭ
    public void IngredientInfoInit()
    {
        this.IngredientInfos = new List<IngredientInfo>();
        this.SaveIngredientInfos();
    }
    //������ ���� ���� ��������
    public IngredientInfo GetIngredientInfo(int id)
    {
        //id�� ��� id
        var info = InfoManager.instance.IngredientInfos.Find(x => x.id == id);
        return info;
    }
    //������ ���� ����
    public void SaveIngredientInfos()
    {
        var path = string.Format("{0}/ingredient_info.json", Application.persistentDataPath);

        var json = JsonConvert.SerializeObject(this.IngredientInfos);

        File.WriteAllText(ingredientPath, json);
    }

    public void LoadIngredientInfos()
    {
        var path = string.Format("{0}/ingredient_info.json", Application.persistentDataPath);
        var json = File.ReadAllText(ingredientPath);
        //������ȭ 
        this.IngredientInfos = JsonConvert.DeserializeObject<List<IngredientInfo>>(json);

    }
    public void LoadPlayerInfo()
    {
        var json = File.ReadAllText(playerPath);
        //������ȭ 
        this.PlayerInfo = JsonConvert.DeserializeObject<PlayerInfo>(json);

    }
    
    //���� ���� ����
    public void SaveBookInfo()
    {
        var path = string.Format("{0}/book_info.json", Application.persistentDataPath);

        var json = JsonConvert.SerializeObject(this.BookInfos);

        File.WriteAllText(path, json);
    }
    //���� �ε�
    public void LoadBookInfo()
    {
        var path = string.Format("{0}/book_info.json", Application.persistentDataPath);
        string json = File.ReadAllText(path);

        //������ȭ
        var bookInfo = JsonConvert.DeserializeObject<List<BookInfo>>(json);

        //InfoManager�� ����
        InfoManager.instance.BookInfos = bookInfo;


    }
    //���� ������ ���� ����
    public void SaveBookItemInfo()
    {
        var path = string.Format("{0}/book_item_info.json", Application.persistentDataPath);

        var json = JsonConvert.SerializeObject(this.BookItemInfos);

        File.WriteAllText(path, json);

    }
    //���� ������ �ε�
    public void LoadBookItemInfo()
    {
        var path = string.Format("{0}/book_item_info.json", Application.persistentDataPath);
        string json = File.ReadAllText(path);

        //������ȭ
        var bookItemInfo = JsonConvert.DeserializeObject<List<BookItemInfo>>(json);

        //InfoManager�� ����
        InfoManager.instance.BookItemInfos = bookItemInfo;

    }
    //�� ���� ���� ����
    public void SaveDreamPieceInfo()
    {
        var path = string.Format("{0}/dream_piece_info.json", Application.persistentDataPath);

        var json = JsonConvert.SerializeObject(this.DreamPieceInfo);

        File.WriteAllText(path, json);

    }
    //�� ���� �ε�
    public void LoadDreamPieceInfo()
    {
        var path = string.Format("{0}/dream_piece_info.json", Application.persistentDataPath);
        string json = File.ReadAllText(path);

        //������ȭ
        var dreamPieceInfo = JsonConvert.DeserializeObject<List<DreamPieceInfo>>(json);

        //InfoManager�� ����
        InfoManager.instance.DreamPieceInfo = dreamPieceInfo;

    }
    //���� ���� ����
    public void SaveMagicToolInfo()
    {
        var path = string.Format("{0}/magicTool_info.json", Application.persistentDataPath);

        var json = JsonConvert.SerializeObject(this.MagicToolInfo);

        File.WriteAllText(path, json);

    }

    public TicketInfo GetTicketInfo()
    {
        var ticketInfo = InfoManager.instance.TicketInfo;
        return ticketInfo;
    }

    //���� ���� �ε�
    public void LoadMagicToolInfo()
    {
        var path = string.Format("{0}/magicTool_info.json", Application.persistentDataPath);
        string json = File.ReadAllText(path);

        //������ȭ
        var magicToolInfo = JsonConvert.DeserializeObject<List<MagicToolInfo>>(json);

        //InfoManager�� ����
        InfoManager.instance.MagicToolInfo = magicToolInfo;
    }

}
