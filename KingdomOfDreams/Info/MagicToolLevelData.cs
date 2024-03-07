using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicToolLevelData
{
    public int id; //마법도구 레벨 id
    public int level; //마법도구 레벨 1~10 단계
    public int magic_piece_require; //마법의 조각 필요 개수
    public int speed_piece_require; //신속의 조각 필요 개수
    public int detox_piece_require; //정화의 조각 필요 개수
    public int wisdom_piece_require; //지혜의 조각 필요 개수
    public int add_magic_property; //마법 속성 증가량
    public float add_speed_property; //신속 속성 증가량
    public float add_detox_property; //정화 속성 증가량
    public int add_wisdom_property; //지혜 속성 증가량
}
