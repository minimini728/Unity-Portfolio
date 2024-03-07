using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookItemData
{
    public int id; //도감 아이템 id
    public string name; //아이템 이름
    public int type; // 1: 레어1, 2: 레어2, 3: 노멀1, 4: 노멀2, 5: 노멀3
    public int action; //행동 1: 튜토리얼, 2: 벌목, 3: 낚시, 4: 채집, 5: 광질, 6: 사냥
    public string sprite_name; //아이템 이미지 이름
    public float percent; //아이템 획득 확률
}
