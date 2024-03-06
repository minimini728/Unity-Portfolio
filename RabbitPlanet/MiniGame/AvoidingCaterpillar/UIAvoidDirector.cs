using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAvoidDirector : MonoBehaviour
{
    public UIGameOver uiGameOver;
    public UIGameRule uiGameRule;
    public UIMemoryClear uiClear; 

    public Text txtScore;
    public int score;

    public void Init()
    {
        this.score = 0;
    }
    public void ShowUIGameOver()
    {
        if(this.uiGameOver != null)
        {
            this.uiGameOver.gameObject.SetActive(true);
        }
    }

    public void ShowUIClear()
    {   
        if(this.uiClear != null)
        {
            this.uiClear.gameObject.SetActive(true);

        }
    }

    public void IncreaseScore()
    {
        this.score++;
        if(this.txtScore != null)
        {
            this.txtScore.text = this.score.ToString();

        }

        if (score >= 5)
        {   
            //게임 클리어 이벤트 전송 -> AvoidMain 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.AvoidGameClear);
        }
    }

}
