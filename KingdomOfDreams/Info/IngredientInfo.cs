using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInfo
{
    public int id;
    public int amount;
    public int auto; //자동공급 개수

    public IngredientInfo(int id, int amount = 1, int auto = 1)
    {
        this.id = id;
        this.amount = amount;
        this.auto = auto;
    }

}
