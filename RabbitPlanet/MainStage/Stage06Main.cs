using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage06Main : MonoBehaviour
{
    public MainPlayer player; //플레이어
    public UIStageDirector director; //캔버스

    //장애물
    public GameObject fire;
    private void Start()
    {
        this.director.txtMaxHeartCount.text = this.player.maxHeartCount.ToString();

        //장애물 초기화
        this.fire.GetComponent<FireObstacle>().Init(1, 4); //1초뒤에 실행돼서 4초마다 반복

    }
    void Update()
    {
        this.director.txtCurrentHeartCount.text = this.player.currentHeartCount.ToString();  //update에 두는 이유 : 현재 하트 개수를 계속 알아야 함

        if (this.player.currentHeartCount == this.player.maxHeartCount) //현재 하트 개수가 0이면 디렉터의 스테이지 클리어 이미지 보여줌
        {
            this.player.isStopped = true;
            this.director.ShowStageClear();
        }
    }
}
