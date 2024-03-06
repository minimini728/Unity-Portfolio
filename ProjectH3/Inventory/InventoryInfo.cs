using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInfo
{
    public int id;
    public int amount;

    public InventoryInfo(int id, int amount = 0)
    {
        this.id = id;
        this.amount = amount;
    }
}
