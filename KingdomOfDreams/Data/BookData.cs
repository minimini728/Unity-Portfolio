using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookData 
{
    public int id; //도감 id
    public string name; //행동 이름
    public int action; //1:튜토리얼 2:벌목 3: 낚시 4: 채집 5: 광질 6: 사냥
    public int rare1_id; //레어 아이템1 id
    public int rare2_id; //레어 아이템2 id
    public int normal1_id; //노멀 아이템1 id
    public int normal2_id; //노멀 아이템2 id
    public int normal3_id; //노멀 아이템3 id
    public int reward_id; //보상 아이템 id
    public int reward_count; //보상 아이템 갯수
}
