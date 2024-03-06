using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidMain : MonoBehaviour
{
    public bool isDie;

    public AvoidPlayerController player; //�÷��̾�
    public UIAvoidDirector director; //ĵ����
    public AvoidGenerator generator; //��ֹ� ������

    private void Awake()
    {
        //���� ��� ������ �÷��� �ȵǰ� ���߱�
        Time.timeScale = 0;
    }

    void Start()
    {   
        //���� ���� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.AvoidGameOver, this.GameOver);
        //���� Ŭ���� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.AvoidGameClear, this.GameClear);
        //���� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.AvoidUpScore, UpScore);

        this.isDie = false;
        this.director.Init();
    }
    void UpScore(short type) //���� �̺�Ʈ �޼���
    {
        this.director.IncreaseScore();
    }

    void GameOver(short type) //���� ���� �̺�Ʈ �޼���
    {
        this.isDie = true;
        this.director.ShowUIGameOver();
        this.generator.Pause();
        if (this.player != null)
        {
            this.player.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void GameClear(short type) //���� Ŭ���� �̺�Ʈ �޼���
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
        //�̺�Ʈ �ߺ� ����, ����
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.AvoidGameOver, this.GameOver);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.AvoidGameClear, this.GameClear);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.AvoidUpScore, UpScore);
    }
}
