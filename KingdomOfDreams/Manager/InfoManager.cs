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

    //아이템 인포 초기화
    public void IngredientInfoInit()
    {
        this.IngredientInfos = new List<IngredientInfo>();
        this.SaveIngredientInfos();
    }
    //아이템 인포 정보 가져오기
    public IngredientInfo GetIngredientInfo(int id)
    {
        //id는 재료 id
        var info = InfoManager.instance.IngredientInfos.Find(x => x.id == id);
        return info;
    }
    //아이템 인포 저장
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
        //역직렬화 
        this.IngredientInfos = JsonConvert.DeserializeObject<List<IngredientInfo>>(json);

    }
    public void LoadPlayerInfo()
    {
        var json = File.ReadAllText(playerPath);
        //역직렬화 
        this.PlayerInfo = JsonConvert.DeserializeObject<PlayerInfo>(json);

    }
    
    //도감 인포 저장
    public void SaveBookInfo()
    {
        var path = string.Format("{0}/book_info.json", Application.persistentDataPath);

        var json = JsonConvert.SerializeObject(this.BookInfos);

        File.WriteAllText(path, json);
    }
    //도감 로드
    public void LoadBookInfo()
    {
        var path = string.Format("{0}/book_info.json", Application.persistentDataPath);
        string json = File.ReadAllText(path);

        //역직렬화
        var bookInfo = JsonConvert.DeserializeObject<List<BookInfo>>(json);

        //InfoManager에 저장
        InfoManager.instance.BookInfos = bookInfo;


    }
    //도감 아이템 인포 저장
    public void SaveBookItemInfo()
    {
        var path = string.Format("{0}/book_item_info.json", Application.persistentDataPath);

        var json = JsonConvert.SerializeObject(this.BookItemInfos);

        File.WriteAllText(path, json);

    }
    //도감 아이템 로드
    public void LoadBookItemInfo()
    {
        var path = string.Format("{0}/book_item_info.json", Application.persistentDataPath);
        string json = File.ReadAllText(path);

        //역직렬화
        var bookItemInfo = JsonConvert.DeserializeObject<List<BookItemInfo>>(json);

        //InfoManager에 저장
        InfoManager.instance.BookItemInfos = bookItemInfo;

    }
    //꿈 조각 인포 저장
    public void SaveDreamPieceInfo()
    {
        var path = string.Format("{0}/dream_piece_info.json", Application.persistentDataPath);

        var json = JsonConvert.SerializeObject(this.DreamPieceInfo);

        File.WriteAllText(path, json);

    }
    //꿈 조각 로드
    public void LoadDreamPieceInfo()
    {
        var path = string.Format("{0}/dream_piece_info.json", Application.persistentDataPath);
        string json = File.ReadAllText(path);

        //역직렬화
        var dreamPieceInfo = JsonConvert.DeserializeObject<List<DreamPieceInfo>>(json);

        //InfoManager에 저장
        InfoManager.instance.DreamPieceInfo = dreamPieceInfo;

    }
    //마법 도구 저장
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

    //마법 도구 로드
    public void LoadMagicToolInfo()
    {
        var path = string.Format("{0}/magicTool_info.json", Application.persistentDataPath);
        string json = File.ReadAllText(path);

        //역직렬화
        var magicToolInfo = JsonConvert.DeserializeObject<List<MagicToolInfo>>(json);

        //InfoManager에 저장
        InfoManager.instance.MagicToolInfo = magicToolInfo;
    }

}
