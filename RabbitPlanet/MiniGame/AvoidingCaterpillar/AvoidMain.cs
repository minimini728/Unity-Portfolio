using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidMain : MonoBehaviour
{
    public bool isDie;

    public AvoidPlayerController player; //플레이어
    public UIAvoidDirector director; //캔버스
    public AvoidGenerator generator; //장애물 스포너

    private void Awake()
    {
        //게임 방법 설명중 플레이 안되게 멈추기
        Time.timeScale = 0;
    }

    void Start()
    {   
        //게임 오버 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.AvoidGameOver, this.GameOver);
        //게임 클리어 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.AvoidGameClear, this.GameClear);
        //득점 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.AvoidUpScore, UpScore);

        this.isDie = false;
        this.director.Init();
    }
    void UpScore(short type) //득점 이벤트 메서드
    {
        this.director.IncreaseScore();
    }

    void GameOver(short type) //게임 오버 이벤트 메서드
    {
        this.isDie = true;
        this.director.ShowUIGameOver();
        this.generator.Pause();
        if (this.player != null)
        {
            this.player.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void GameClear(short type) //게임 클리어 이벤트 메서드
    {
        this.director.ShowUIClear();
        this.generator.Pause();
        if (this.player != null)
        {
            this.player.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    private void OnDisable()
    {      
        //이벤트 중복 방지, 제거
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.AvoidGameOver, this.GameOver);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.AvoidGameClear, this.GameClear);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.AvoidUpScore, UpScore);
    }
}
