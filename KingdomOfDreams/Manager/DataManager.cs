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

    //�÷��� �ʱ�ȭ (��ť ��� ���Ҷ�)
    private Dictionary<int, IngredientData> dicIngredientData; //�κ��丮
    public Dictionary<int, BookData> dicBookData; //����
    public Dictionary<int, BookItemData> dicBookItemData; //���� ������
    public Dictionary<int, DreamPieceData> dicDreamPieceData; //���� ����
    public Dictionary<int, MagicToolData> dicMagicToolData; //��������
    public Dictionary<int, MagicToolLevelData> dicMagicToolLevelData; //�������� ����

    //�κ��丮 ������
    public IngredientData GetIngredientData(int id)
    {
        return this.dicIngredientData[id];
    }

    public void LoadIngredientData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Datas/LHMDatas/Titem_data");

        var json = asset.text;

        //������ȭ 
        IngredientData[] arrItemDatas = JsonConvert.DeserializeObject<IngredientData[]>(json);

        this.dicIngredientData = arrItemDatas.ToDictionary(x => x.id); 
        Debug.LogFormat("item data loaded : {0}", this.dicIngredientData.Count);
    }

    //���� ������
    public BookData GetBookData(int id)
    {
        return this.dicBookData[id];
    }

    public void LoadBookData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Datas/LHMDatas/book_data");
        var json = asset.text;
        Debug.Log(json);

        BookData[] arrItemDatas = JsonConvert.DeserializeObject<BookData[]>(json);

        this.dicBookData = arrItemDatas.ToDictionary(x => x.id);

        Debug.LogFormat("book data loaded: {0}", this.dicBookData.Count);
    }

    public List<BookData> GetBookDatas()
    {
        return this.dicBookData.Values.ToList();
    }

    //���� ������ ������
    public BookItemData GetBookItemData(int id)
    {
        return this.dicBookItemData[id];
    }

    public void LoadBookItemData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Datas/LHMDatas/book_item_data");
        var json = asset.text;
        Debug.Log(json);

        BookItemData[] arrItemDatas = JsonConvert.DeserializeObject<BookItemData[]>(json);

        this.dicBookItemData = arrItemDatas.ToDictionary(x => x.id);
    }

    public List<BookItemData> GetBookItemDatas()
    {
        return this.dicBookItemData.Values.ToList();
    }

    //�������� ������
    public DreamPieceData GetDreamPieceData(int id)
    {
        return this.dicDreamPieceData[id];
    }

    public void LoadDreamPieceData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Datas/LHMDatas/dream_piece_data");
        var json = asset.text;

        DreamPieceData[] arrItemDatas = JsonConvert.DeserializeObject<DreamPieceData[]>(json);

        this.dicDreamPieceData = arrItemDatas.ToDictionary(x => x.id);

    }

    public List<DreamPieceData> GetDreamPieceDatas()
    {
        return this.dicDreamPieceData.Values.ToList();
    }

    //�������� ������
    public MagicToolData GetMagicToolData(int id)
    {
        return this.dicMagicToolData[id];
    }

    public void LoadMagicToolData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Datas/LHMDatas/magicTool_data");
        var json = asset.text;
        Debug.Log(json);

        MagicToolData[] arrItemDatas = JsonConvert.DeserializeObject<MagicToolData[]>(json);

        this.dicMagicToolData = arrItemDatas.ToDictionary(x => x.id);

    }

    public List<MagicToolData> GetMagicToolDatas()
    {
        return this.dicMagicToolData.Values.ToList();
    }

    //�������� ����������
    public MagicToolLevelData GetMagicToolLevelData(int id)
    {
        return this.dicMagicToolLevelData[id];
    }

    public void LoadMagicToolLevelData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Datas/LHMDatas/magicTool_level_data");
        var json = asset.text;

        MagicToolLevelData[] arrItemDatas = JsonConvert.DeserializeObject<MagicToolLevelData[]>(json);

        this.dicMagicToolLevelData = arrItemDatas.ToDictionary(x => x.id);
    }

    public List<MagicToolLevelData> GetMagicToolLevelDatas()
    {
        return this.dicMagicToolLevelData.Values.ToList();
    }

}
