using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookItemInfo
{
    public int id;
    public int exist; //0: ȹ����, 1: ȹ����

    public BookItemInfo(int id, int exist = 0)
    {
        this.id = id;
        this.exist = exist;
    }
}
