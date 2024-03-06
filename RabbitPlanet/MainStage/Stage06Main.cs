using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage06Main : MonoBehaviour
{
    public MainPlayer player; //�÷��̾�
    public UIStageDirector director; //ĵ����

    //��ֹ�
    public GameObject fire;
    private void Start()
    {
        this.director.txtMaxHeartCount.text = this.player.maxHeartCount.ToString();

        //��ֹ� �ʱ�ȭ
        this.fire.GetComponent<FireObstacle>().Init(1, 4); //1�ʵڿ� ����ż� 4�ʸ��� �ݺ�

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
