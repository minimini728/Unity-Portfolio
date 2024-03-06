using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatus : MonoBehaviour
{
    public Image imgPlayerFrameBurn;
    public Image imgHpBar;
    public Image imgSkillBar;
    void Start()
    {
        this.imgHpBar.fillAmount = InfoManager.instance.PlayerInfo.hp / 100;
        this.imgSkillBar.fillAmount = InfoManager.instance.PlayerInfo.skill / 100;

        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.IncreasePlayerHpBar, this.IncreaseHpBar);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.DeclinePlayerHpBar, this.DeclineHpBar);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.IncreasePlayerSkillBar, this.IncreaseSkillBar);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ResetPlayerSkillBar, this.ResetSkillBar);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ResetPlayerHpBar, this.ResetHpBar);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.FullPlayerSkillBar, this.FullSkillBar);
    }
    void IncreaseHpBar(short type)
    {
        this.imgHpBar.fillAmount += 0.05f;
    }
    void DeclineHpBar(short type)
    {
        this.imgHpBar.fillAmount -= 0.05f;
    }
    void IncreaseSkillBar(short type)
    {
        this.imgSkillBar.fillAmount += 0.05f;

        if(this.imgSkillBar.fillAmount == 1)
        {
            this.imgPlayerFrameBurn.gameObject.SetActive(true);
        }
    }
    void FullSkillBar(short type)
    {
        this.imgSkillBar.fillAmount = 1;

        this.imgPlayerFrameBurn.gameObject.SetActive(true);
    }
    void ResetSkillBar(short type)
    {
        this.imgSkillBar.fillAmount = 0;

        this.imgPlayerFrameBurn.gameObject.SetActive(false);
    }
    void ResetHpBar(short type)
    {
        this.imgHpBar.fillAmount = 1;
    }

    private void OnDisable()
    {
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.IncreasePlayerHpBar, this.IncreaseHpBar);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.DeclinePlayerHpBar, this.DeclineHpBar);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.IncreasePlayerSkillBar, this.IncreaseSkillBar);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ResetPlayerSkillBar, this.ResetSkillBar);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ResetPlayerHpBar, this.ResetHpBar);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.FullPlayerSkillBar, this.FullSkillBar);

    }
}
