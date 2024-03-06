using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerProperty : MonoBehaviour
{
    public float hp;
    public float damage;
    public float skill;
    void Start()
    {   
        //이벤트들 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.IncreaseSkillValue, this.IncreaseSkillValue);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ResetPlayerSkill, this.ResetSkillValue);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.IncreasePlayerHp, this.IncreaseHpValue);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.DeclinePlayerHp, this.DeclineHpValue);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.FullPlayerSkillValue, this.FullSkillValue);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ResetPlayerHpValue, this.ResetHpValue);

        this.hp = InfoManager.instance.PlayerInfo.hp;
        this.damage = InfoManager.instance.PlayerInfo.damage;
        this.skill = InfoManager.instance.PlayerInfo.skill;
    }

    public void IncreaseSkillValue(short type) //스킬 5 상향 메서드
    {
        if(this.skill < 100)
        {
            this.skill += 5; //skill +5

            InfoManager.instance.PlayerInfo.skill = this.skill; //PlayerInfo속성의 skill값에 필드 skill값 할당
            InfoManager.instance.SavePlayerInfo(); //PlayerInfo속성값 Json으로 저장

            //UI스킬바 5 상향 이벤트 호출
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.IncreasePlayerSkillBar);
        }
    }

    void FullSkillValue(short type) //스킬 풀 메서드
    {
        if(this.skill < 100)
        {
            this.skill = 100;

            InfoManager.instance.PlayerInfo.skill = this.skill;
            InfoManager.instance.SavePlayerInfo();

            //UI스킬바 풀 이벤트 호출
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.FullPlayerSkillBar);

        }
    }
    void ResetSkillValue(short type) //스킬 0으로 초기화 메서드
    {
        this.skill = 0;

        InfoManager.instance.PlayerInfo.skill = this.skill;
        InfoManager.instance.SavePlayerInfo();

        //UI스킬바 초기화 이벤트 호출
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ResetPlayerSkillBar);
    }
    void ResetHpValue(short type) //hp 100으로 초기화 메서드
    {
        this.hp = 100;

        InfoManager.instance.PlayerInfo.hp = this.hp;
        InfoManager.instance.SavePlayerInfo();

        //UI hp바 초기화 이벤트 호출
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ResetPlayerHpBar);
    }

    void IncreaseHpValue(short type) //hp 5 상향 메서드
    {
        if(this.hp < 100)
        {
            this.hp += 5;

            InfoManager.instance.PlayerInfo.hp = this.hp;
            InfoManager.instance.SavePlayerInfo();

            //UI hp바 5 상향 이벤트 호출
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.IncreasePlayerHpBar);
        }
    }

    void DeclineHpValue(short type) //hp 5 감소 메서드
    {
        if(this.hp <= 100 && this.hp > 0) //0<hp<=100 사이일때
        {
            this.hp -= 5;

            InfoManager.instance.PlayerInfo.hp = this.hp;
            InfoManager.instance.SavePlayerInfo();

            //UI hp바 5 감소 이벤트 호출
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.DeclinePlayerHpBar);
        }

        if(this.hp == 0)
        {   
            //죽어서 전초기지로 귀환되는 이벤트 호출
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ChangeToOutPost);
            this.hp = 1;
        }
    }

    private void OnDisable()
    {
        this.ResetSkillValue(1); //스킬 0으로 초기화
        this.ResetHpValue(1); //hp 100으로 초기화

        //이벤트 중복 방지를 위한 제거
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.IncreaseSkillValue, this.IncreaseSkillValue);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ResetPlayerSkill, this.ResetSkillValue);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.IncreasePlayerHp, this.IncreaseHpValue);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.DeclinePlayerHp, this.DeclineHpValue);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.FullPlayerSkillValue, this.FullSkillValue);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ResetPlayerHpValue, this.ResetHpValue);

    }
}
