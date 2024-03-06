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

    private eSceneType state; //현재 씬

    public bool isNewbie; //뉴비 체크

    public GameObject LoadingSources; //로딩창
    public Image imgProgressBar; //로딩바
    private AsyncOperation op; //로딩 비동기 작업

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        //인벤토리 데이터 로드
        DataManager.instance.LoadInventoryData();

        Debug.Log(Application.persistentDataPath);

        string playerPath = InfoManager.instance.playerPath; 

        if (!InfoManager.instance.IsNewbie(playerPath)) //뉴비 체크
        {
            //기존 유저
            Debug.Log("<color=yellow>기존유저</color>");

            //플레이어 저장 데이터 로드
            InfoManager.instance.LoadPlayerInfo();
            //인벤토리 저장 데이터 로드
            InfoManager.instance.LoadInventoryInfo();
            //전초기지 저장 데이터 로드
            InfoManager.instance.LoadOutPostInfo();

            this.isNewbie = false;
        }
        else
        {
            this.isNewbie = true;

            //신규 유저
            Debug.Log("<color=yellow>신규유저</color>");

            //플레이어 인포 생성
            InfoManager.instance.PlayerInfo = new PlayerInfo();
            //플레이어 인포 저장
            InfoManager.instance.SavePlayerInfo();

            //인벤토리 생성과 저장
            InfoManager.instance.InventoryInfoInit();

            //전초기지 인포 생성
            InfoManager.instance.OutPostInfos = new OutPostInfo();
            //전초기지 인포 저장
            InfoManager.instance.SaveOutPostInfo();


        }

        this.state = eSceneType.Title;

        this.ChangeScene(this.state); //타이틀로 씬 전환

    }

    void ChangeToOutPost(short type) //전초기지로 씬 전환 이벤트
    {
        this.state = eSceneType.Outpost;
        this.ChangeScene(this.state);
    }
    void ChangeToOutPostTuto(short type)
    {
        this.state = eSceneType.OutpostTuto;
        this.ChangeScene(this.state);
    }

    void ChangeToBattleField(short type) //전투장으로 씬 전환 이벤트
    {
        this.state = eSceneType.BattleField;
        this.ChangeScene(this.state);
    }

    void ChangeToBattleFieldTuto(short type)
    {
        this.state = eSceneType.BattleFieldTuto;
        this.ChangeScene(this.state);
    }
    public void ChangeScene(eSceneType sceneType) //씬 전환 메서드
    {
        switch (sceneType)
        {
            case eSceneType.Title:
                var titleOper = SceneManager.LoadSceneAsync("Title");

                //로딩창
                this.op = titleOper;
                StartCoroutine(this.LoadScene());

                titleOper.completed += (obj) =>
                {
                    this.imgProgressBar.fillAmount = 0; //로딩바 초기화
                    this.LoadingSources.SetActive(false); //로딩창 비활성화

                    //전초기지로 씬 전환 이벤트 중복 제거
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ChangeToOutPost, ChangeToOutPost);
                    //전초기지로 씬 전환 이벤트 등록
                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ChangeToOutPost, ChangeToOutPost);

                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ChangeToOutPostTuTo, ChangeToOutPostTuto);
                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ChangeToOutPostTuTo, ChangeToOutPostTuto);
                };
                break;

            case eSceneType.Outpost:
                var outPostOper = SceneManager.LoadSceneAsync("Outpost");

                //로딩창
                this.op = outPostOper;
                StartCoroutine(this.LoadScene());

                outPostOper.completed += (obj) =>
                {   
                    if(this.imgProgressBar != null) //널 체크
                    {
                        this.imgProgressBar.fillAmount = 0; //로딩바 초기화

                    }
                    if (this.LoadingSources != null) //널 체크
                    {
                        this.LoadingSources.SetActive(false); //로딩창 비활성화
                    }

                    //전투장으로 씬 전환 이벤트 중복 제거
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ChangeToBattleField, ChangeToBattleField);
                    //전투장으로 씬 전환 이벤트 등록
                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ChangeToBattleField, ChangeToBattleField);
                };
                break;

            case eSceneType.OutpostTuto:
                var outPostTutoOper = SceneManager.LoadSceneAsync("OutpostTuto");

                //로딩창
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

                //로딩창
                this.op = battleFieldOper;
                StartCoroutine(this.LoadScene());

                battleFieldOper.completed += (obj) =>
                {
                    this.imgProgressBar.fillAmount = 0; //로딩바 초기화
                    this.LoadingSources.SetActive(false); //로딩창 비활성화

                    var battleFieldMain = GameObject.FindObjectOfType<BattleFieldMain>();
                    battleFieldMain.Init();

                    //전초기지로 씬 전환 이벤트 중복제거
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ChangeToOutPost, ChangeToOutPost);
                    //전초기지로 씬 전환 이벤트 등록
                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ChangeToOutPost, ChangeToOutPost);
                };
                break;

            case eSceneType.BattleFieldTuto:
                var battleFieldOperTuto = SceneManager.LoadSceneAsync("BattleFieldTuto");

                //로딩창
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
    IEnumerator LoadScene() //로딩창, 로딩바 구현 코루틴
    {
        yield return null;
        this.LoadingSources.SetActive(true); //로딩창 활성화
        op.allowSceneActivation = false; //씬 90%만 로드
        float timer = 0.0f; 
        while (!op.isDone) //씬 로딩 완료되기 전까지
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
