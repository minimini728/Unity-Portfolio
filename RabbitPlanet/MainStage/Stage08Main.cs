using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage08Main : MonoBehaviour
{
    public MainPlayer player; //�÷��̾�
    public UIStageDirector director; //ĵ����

    //��ֹ�
    public GameObject[] shakeBlock; //��鸮�� ��
    public GameObject poison; //��
    private void Start()
    {
        this.director.txtMaxHeartCount.text = this.player.maxHeartCount.ToString();

        //��ֹ� �ʱ�ȭ
        this.shakeBlock[0].GetComponent<ShakeBlock>().Init(1);
        this.shakeBlock[1].GetComponent<ShakeBlock>().Init(3);
        this.shakeBlock[2].GetComponent<ShakeBlock>().Init(4);
        this.shakeBlock[3].GetComponent<ShakeBlock>().Init(6);

        this.poison.GetComponent<PoisonGenerator>().Init(19f, 24f, 5f, 1f, 6f);
        

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
