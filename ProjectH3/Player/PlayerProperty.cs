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
        //�̺�Ʈ�� ���
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

    public void IncreaseSkillValue(short type) //��ų 5 ���� �޼���
    {
        if(this.skill < 100)
        {
            this.skill += 5; //skill +5

            InfoManager.instance.PlayerInfo.skill = this.skill; //PlayerInfo�Ӽ��� skill���� �ʵ� skill�� �Ҵ�
            InfoManager.instance.SavePlayerInfo(); //PlayerInfo�Ӽ��� Json���� ����

            //UI��ų�� 5 ���� �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.IncreasePlayerSkillBar);
        }
    }

    void FullSkillValue(short type) //��ų Ǯ �޼���
    {
        if(this.skill < 100)
        {
            this.skill = 100;

            InfoManager.instance.PlayerInfo.skill = this.skill;
            InfoManager.instance.SavePlayerInfo();

            //UI��ų�� Ǯ �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.FullPlayerSkillBar);

        }
    }
    void ResetSkillValue(short type) //��ų 0���� �ʱ�ȭ �޼���
    {
        this.skill = 0;

        InfoManager.instance.PlayerInfo.skill = this.skill;
        InfoManager.instance.SavePlayerInfo();

        //UI��ų�� �ʱ�ȭ �̺�Ʈ ȣ��
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ResetPlayerSkillBar);
    }
    void ResetHpValue(short type) //hp 100���� �ʱ�ȭ �޼���
    {
        this.hp = 100;

        InfoManager.instance.PlayerInfo.hp = this.hp;
        InfoManager.instance.SavePlayerInfo();

        //UI hp�� �ʱ�ȭ �̺�Ʈ ȣ��
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ResetPlayerHpBar);
    }

    void IncreaseHpValue(short type) //hp 5 ���� �޼���
    {
        if(this.hp < 100)
        {
            this.hp += 5;

            InfoManager.instance.PlayerInfo.hp = this.hp;
            InfoManager.instance.SavePlayerInfo();

            //UI hp�� 5 ���� �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.IncreasePlayerHpBar);
        }
    }

    void DeclineHpValue(short type) //hp 5 ���� �޼���
    {
        if(this.hp <= 100 && this.hp > 0) //0<hp<=100 �����϶�
        {
            this.hp -= 5;

            InfoManager.instance.PlayerInfo.hp = this.hp;
            InfoManager.instance.SavePlayerInfo();

            //UI hp�� 5 ���� �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.DeclinePlayerHpBar);
        }

        if(this.hp == 0)
        {   
            //�׾ ���ʱ����� ��ȯ�Ǵ� �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ChangeToOutPost);
            this.hp = 1;
        }
    }

    private void OnDisable()
    {
        this.ResetSkillValue(1); //��ų 0���� �ʱ�ȭ
        this.ResetHpValue(1); //hp 100���� �ʱ�ȭ

        //�̺�Ʈ �ߺ� ������ ���� ����
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.IncreaseSkillValue, this.IncreaseSkillValue);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ResetPlayerSkill, this.ResetSkillValue);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.IncreasePlayerHp, this.IncreaseHpValue);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.DeclinePlayerHp, this.DeclineHpValue);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.FullPlayerSkillValue, this.FullSkillValue);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ResetPlayerHpValue, this.ResetHpValue);

    }
}
