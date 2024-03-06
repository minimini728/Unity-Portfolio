using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardestMain : MonoBehaviour
{
    public UIHardestDirector director; //캔버스
    public HardestPlayerController player; //플레이어

    private void Awake()
    {
        //게임 방법 설명중 플레이 안되게 멈추기
        Time.timeScale = 0;
    }

    void Start()
    {   
        //게임 오버 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.HardestGameOver, this.GameOver);
        //게임 클리어 이벤트 등록
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
        //이벤트 중복 방지, 제거
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.HardestGameOver, this.GameOver);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.HardestGoal, this.GameClear);

    }
}
