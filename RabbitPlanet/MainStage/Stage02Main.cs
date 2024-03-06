using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage02Main : MonoBehaviour
{
    public MainPlayer player;
    public UIStageDirector director;

    public GameObject[] block; //흔들리는 블럭
    void Start()
    {
        this.director.txtMaxHeartCount.text = this.player.maxHeartCount.ToString();

        this.block[0].GetComponent<ShakeBlock>().Init(3); //3초뒤 실행
        this.block[1].GetComponent<ShakeBlock>().Init(8); //8초뒤 실행
        this.block[2].GetComponent<ShakeBlock>().Init(13);
        this.block[3].GetComponent<ShakeBlock>().Init(18);
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

