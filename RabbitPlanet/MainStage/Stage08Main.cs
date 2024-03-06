using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage08Main : MonoBehaviour
{
    public MainPlayer player; //플레이어
    public UIStageDirector director; //캔버스

    //장애물
    public GameObject[] shakeBlock; //흔들리는 블럭
    public GameObject poison; //독
    private void Start()
    {
        this.director.txtMaxHeartCount.text = this.player.maxHeartCount.ToString();

        //장애물 초기화
        this.shakeBlock[0].GetComponent<ShakeBlock>().Init(1);
        this.shakeBlock[1].GetComponent<ShakeBlock>().Init(3);
        this.shakeBlock[2].GetComponent<ShakeBlock>().Init(4);
        this.shakeBlock[3].GetComponent<ShakeBlock>().Init(6);

        this.poison.GetComponent<PoisonGenerator>().Init(19f, 24f, 5f, 1f, 6f);
        

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
