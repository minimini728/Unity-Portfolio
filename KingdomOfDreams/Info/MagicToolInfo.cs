using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicToolInfo
{
    public int id;
    public int level; //마법도구 레벨

    public MagicToolInfo(int id, int level = 1)
    {
        this.id = id;
        this.level = level;
    }
}
