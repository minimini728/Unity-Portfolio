using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class App : MonoBehaviour
{
    public enum eSceneType
    {
        App,
        Title,
        Outpost,
        OutpostTuto,
        BattleField,
        BattleFieldTuto,
    }

    private eSceneType state; //���� ��

    public bool isNewbie; //���� üũ

    public GameObject LoadingSources; //�ε�â
    public Image imgProgressBar; //�ε���
    private AsyncOperation op; //�ε� �񵿱� �۾�

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        //�κ��丮 ������ �ε�
        DataManager.instance.LoadInventoryData();

        Debug.Log(Application.persistentDataPath);

        string playerPath = InfoManager.instance.playerPath; 

        if (!InfoManager.instance.IsNewbie(playerPath)) //���� üũ
        {
            //���� ����
            Debug.Log("<color=yellow>��������</color>");

            //�÷��̾� ���� ������ �ε�
            InfoManager.instance.LoadPlayerInfo();
            //�κ��丮 ���� ������ �ε�
            InfoManager.instance.LoadInventoryInfo();
            //���ʱ��� ���� ������ �ε�
            InfoManager.instance.LoadOutPostInfo();

            this.isNewbie = false;
        }
        else
        {
            this.isNewbie = true;

            //�ű� ����
            Debug.Log("<color=yellow>�ű�����</color>");

            //�÷��̾� ���� ����
            InfoManager.instance.PlayerInfo = new PlayerInfo();
            //�÷��̾� ���� ����
            InfoManager.instance.SavePlayerInfo();

            //�κ��丮 ������ ����
            InfoManager.instance.InventoryInfoInit();

            //���ʱ��� ���� ����
            InfoManager.instance.OutPostInfos = new OutPostInfo();
            //���ʱ��� ���� ����
            InfoManager.instance.SaveOutPostInfo();


        }

        this.state = eSceneType.Title;

        this.ChangeScene(this.state); //Ÿ��Ʋ�� �� ��ȯ

    }

    void ChangeToOutPost(short type) //���ʱ����� �� ��ȯ �̺�Ʈ
    {
        this.state = eSceneType.Outpost;
        this.ChangeScene(this.state);
    }
    void ChangeToOutPostTuto(short type)
    {
        this.state = eSceneType.OutpostTuto;
        this.ChangeScene(this.state);
    }

    void ChangeToBattleField(short type) //���������� �� ��ȯ �̺�Ʈ
    {
        this.state = eSceneType.BattleField;
        this.ChangeScene(this.state);
    }

    void ChangeToBattleFieldTuto(short type)
    {
        this.state = eSceneType.BattleFieldTuto;
        this.ChangeScene(this.state);
    }
    public void ChangeScene(eSceneType sceneType) //�� ��ȯ �޼���
    {
        switch (sceneType)
        {
            case eSceneType.Title:
                var titleOper = SceneManager.LoadSceneAsync("Title");

                //�ε�â
                this.op = titleOper;
                StartCoroutine(this.LoadScene());

                titleOper.completed += (obj) =>
                {
                    this.imgProgressBar.fillAmount = 0; //�ε��� �ʱ�ȭ
                    this.LoadingSources.SetActive(false); //�ε�â ��Ȱ��ȭ

                    //���ʱ����� �� ��ȯ �̺�Ʈ �ߺ� ����
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ChangeToOutPost, ChangeToOutPost);
                    //���ʱ����� �� ��ȯ �̺�Ʈ ���
                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ChangeToOutPost, ChangeToOutPost);

                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ChangeToOutPostTuTo, ChangeToOutPostTuto);
                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ChangeToOutPostTuTo, ChangeToOutPostTuto);
                };
                break;

            case eSceneType.Outpost:
                var outPostOper = SceneManager.LoadSceneAsync("Outpost");

                //�ε�â
                this.op = outPostOper;
                StartCoroutine(this.LoadScene());

                outPostOper.completed += (obj) =>
                {   
                    if(this.imgProgressBar != null) //�� üũ
                    {
                        this.imgProgressBar.fillAmount = 0; //�ε��� �ʱ�ȭ

                    }
                    if (this.LoadingSources != null) //�� üũ
                    {
                        this.LoadingSources.SetActive(false); //�ε�â ��Ȱ��ȭ
                    }

                    //���������� �� ��ȯ �̺�Ʈ �ߺ� ����
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ChangeToBattleField, ChangeToBattleField);
                    //���������� �� ��ȯ �̺�Ʈ ���
                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ChangeToBattleField, ChangeToBattleField);
                };
                break;

            case eSceneType.OutpostTuto:
                var outPostTutoOper = SceneManager.LoadSceneAsync("OutpostTuto");

                //�ε�â
                this.op = outPostTutoOper;
                StartCoroutine(this.LoadScene());

                outPostTutoOper.completed += (obj) =>
                {
                    this.imgProgressBar.fillAmount = 0;
                    this.LoadingSources.SetActive(false);

                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ChangeToBattleFieldTuto, ChangeToBattleFieldTuto);
                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ChangeToBattleFieldTuto, ChangeToBattleFieldTuto);
                };
                break;

            case eSceneType.BattleField:
                var battleFieldOper = SceneManager.LoadSceneAsync("BattleField");

                //�ε�â
                this.op = battleFieldOper;
                StartCoroutine(this.LoadScene());

                battleFieldOper.completed += (obj) =>
                {
                    this.imgProgressBar.fillAmount = 0; //�ε��� �ʱ�ȭ
                    this.LoadingSources.SetActive(false); //�ε�â ��Ȱ��ȭ

                    var battleFieldMain = GameObject.FindObjectOfType<BattleFieldMain>();
                    battleFieldMain.Init();

                    //���ʱ����� �� ��ȯ �̺�Ʈ �ߺ�����
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ChangeToOutPost, ChangeToOutPost);
                    //���ʱ����� �� ��ȯ �̺�Ʈ ���
                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ChangeToOutPost, ChangeToOutPost);
                };
                break;

            case eSceneType.BattleFieldTuto:
                var battleFieldOperTuto = SceneManager.LoadSceneAsync("BattleFieldTuto");

                //�ε�â
                this.op = battleFieldOperTuto;
                StartCoroutine(this.LoadScene());

                battleFieldOperTuto.completed += (obj) =>
                {
                    this.imgProgressBar.fillAmount = 0;
                    this.LoadingSources.SetActive(false);

                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ChangeToOutPost, ChangeToOutPost);
                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ChangeToOutPost, ChangeToOutPost);
                };
                break;


        }
    }
    IEnumerator LoadScene() //�ε�â, �ε��� ���� �ڷ�ƾ
    {
        yield return null;
        this.LoadingSources.SetActive(true); //�ε�â Ȱ��ȭ
        op.allowSceneActivation = false; //�� 90%�� �ε�
        float timer = 0.0f; 
        while (!op.isDone) //�� �ε� �Ϸ�Ǳ� ������
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                imgProgressBar.fillAmount = Mathf.Lerp(imgProgressBar.fillAmount, op.progress, timer);
                if (imgProgressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                imgProgressBar.fillAmount = Mathf.Lerp(imgProgressBar.fillAmount, 1f, timer);
                if (imgProgressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

}
