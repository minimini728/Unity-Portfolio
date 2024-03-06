using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage10Main : MonoBehaviour
{
    public MainPlayer player;
    public UIStageDirector director;

    //장애물
    public GameObject[] fire;
    public GameObject[] poison;
    private void Start()
    {
        this.director.txtMaxHeartCount.text = this.player.maxHeartCount.ToString();

        //장애물 초기화
        this.fire[0].GetComponent<FireObstacle>().Init(3, 5);
        this.fire[1].GetComponent<FireObstacle>().Init(3, 5);
        this.fire[2].GetComponent<FireObstacle>().Init(3, 5);
        //-21 -14 2 0 7
        this.poison[0].GetComponent<PoisonGenerator>().Init(-21f, -14f, 1f, 0f, 7f);
        //-21 -15 2 22.5 27.5
        this.poison[1].GetComponent<PoisonGenerator>().Init(-21f, -15f, 1f, 22.5f, 27.5f);


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
