using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardestMain : MonoBehaviour
{
    public UIHardestDirector director; //ĵ����
    public HardestPlayerController player; //�÷��̾�

    private void Awake()
    {
        //���� ��� ������ �÷��� �ȵǰ� ���߱�
        Time.timeScale = 0;
    }

    void Start()
    {   
        //���� ���� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.HardestGameOver, this.GameOver);
        //���� Ŭ���� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.HardestGoal, this.GameClear);
    }

    void GameOver(short type)
    {
        this.director.ShowUIGameOver();
        this.player.Stopped();
    }

    void GameClear(short type)
    {
        this.director.ShowUIGoal();
        this.player.Stopped();
    }

    private void OnDisable()
    {   
        //�̺�Ʈ �ߺ� ����, ����
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.HardestGameOver, this.GameOver);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.HardestGoal, this.GameClear);

    }
}
