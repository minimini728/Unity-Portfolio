using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHardestDirector : MonoBehaviour
{
    public UIGameOver uiGameOver;
    public UIGameRule uiGameRule;
    public UIMemoryClear uiClear;

    public void ShowUIGameOver()
    {
        if(this.uiGameOver != null)
        {
            this.uiGameOver.gameObject.SetActive(true);

        }
    }

    public void ShowUIGoal()
    {   
        if(this.uiClear != null)
        {
            this.uiClear.gameObject.SetActive(true);
        }
    }

}
