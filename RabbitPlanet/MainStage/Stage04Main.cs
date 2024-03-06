using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage04Main : MonoBehaviour
{
    public MainPlayer player; //�÷��̾�
    public UIStageDirector director; //ĵ����

    //��ֹ�
    public GameObject poison; //�� �մ� ��ֹ�
    public GameObject[] shakeBlock; //��鸮�� ��

    private void Start()
    {
        //��ֹ� Ʈ���� �̺�Ʈ �ʱ�ȭ
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ObstacleTrigger, FallBlock);

        this.director.txtMaxHeartCount.text = this.player.maxHeartCount.ToString();

        //��ֹ� �ʱ�ȭ, �� ��ġ ����
        this.poison.GetComponent<PoisonGenerator>().Init(0, 7f, 1f, 0f, 7f);

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

    void FallBlock(short type)
    {   
        if(this.shakeBlock[0] != null && this.shakeBlock[1] != null && this.shakeBlock[2] != null && this.shakeBlock[3] != null)
        {
            this.shakeBlock[0].GetComponent<ShakeBlock>().Init(1);
            this.shakeBlock[1].GetComponent<ShakeBlock>().Init(3);
            this.shakeBlock[2].GetComponent<ShakeBlock>().Init(5);
            this.shakeBlock[3].GetComponent<ShakeBlock>().Init(7);

        }

    }
}
