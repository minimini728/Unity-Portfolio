using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryCell : MonoBehaviour
{
    public Image imgIcon; //아이템 이미지
    public Text txtName; //아이템 이름
    public Text txtAmount; //아이템 수량
    public Text txtAuto; //자동지급 개수 텍스트

    public void Init(int id, string name, Sprite sprite, int amount)
    {
        var data = InfoManager.instance.IngredientInfos.Find(x => x.id == id); //보유 인벤토리에서 정보 가져오기

        this.imgIcon.sprite = sprite;
        this.txtName.text = string.Format("{0}", name);
        this.txtAmount.text = string.Format("{0}", amount);
        if(data.id == 2005 || data.id == 2006 || data.id == 2007)
        {
            this.txtAuto.text = string.Format(" ");
        }
        else
        {
            this.txtAuto.text = string.Format("{0}개/5분", data.auto);
        }

    }
}
