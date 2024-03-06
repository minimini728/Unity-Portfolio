using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage02Main : MonoBehaviour
{
    public MainPlayer player;
    public UIStageDirector director;

    public GameObject[] block; //��鸮�� ��
    void Start()
    {
        this.director.txtMaxHeartCount.text = this.player.maxHeartCount.ToString();

        this.block[0].GetComponent<ShakeBlock>().Init(3); //3�ʵ� ����
        this.block[1].GetComponent<ShakeBlock>().Init(8); //8�ʵ� ����
        this.block[2].GetComponent<ShakeBlock>().Init(13);
        this.block[3].GetComponent<ShakeBlock>().Init(18);
    }
    void Update()
    {
        this.director.txtCurrentHeartCount.text = this.player.currentHeartCount.ToString();  //update�� �δ� ���� : ���� ��Ʈ ������ ��� �˾ƾ� ��

        if (this.player.currentHeartCount == this.player.maxHeartCount) //���� ��Ʈ ������ 0�̸� ������ �������� Ŭ���� �̹��� ������
        {
            this.player.isStopped = true;
            this.director.ShowStageClear();
        }
    }
}

