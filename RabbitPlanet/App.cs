using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{   
    public enum eSceneType
    {
        App, Title, Stage01, Stage02, Stage03, Stage04, Stage05, Stage06, Stage07, Stage08, Stage09, Stage10,
        Avoid, Flappy, Hardest, Jumping, Matching, Maze, Memory
    }

    private eSceneType state; //현재 씬
    private eSceneType preState; //이전 씬
    int sceneCount = 0; //씬 번호
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        this.state = eSceneType.Title;

        this.ChangeScene(this.state); //타이틀 씬으로 이동

        //미니게임 클리어후 메인 게임으로 이동 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.BackMainGame, new EventHandler((type) =>
        {
            this.state = preState;
            this.ChangeScene(this.state);

        }));

        //미니게임 시작 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.StartMiniGame, new EventHandler((type) =>
        {
            this.preState = this.state;
            int randomNum = Random.Range(12, 19); //미니게임 랜덤으로 플레이
            this.state = (eSceneType)randomNum;
            this.ChangeScene(this.state);

        }));

        //새게임 시작 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.NewGame, new EventHandler((type) =>
        {
            this.state = eSceneType.Title;
            this.ChangeScene(state);

            this.sceneCount = 0;
        }));

    }

    //메인 게임 다음 스테이지로 이동 이벤트
    void ChangeMainGame(short type)
    {
        eSceneType[] sceneNum = { eSceneType.Stage02, eSceneType.Stage03, eSceneType.Stage04,
            eSceneType.Stage05, eSceneType.Stage06, eSceneType.Stage07, eSceneType.Stage08, eSceneType.Stage09, eSceneType.Stage10 };

        this.state = sceneNum[sceneCount];
        this.ChangeScene(this.state);

        sceneCount++;

        //이벤트 중복 방지, 제거
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);
    }

    //메인 게임 시작 이벤트
    void StartMainGame(short type)
    {
        this.state = eSceneType.Stage01;
        this.ChangeScene(this.state);
    }
    
    //씬 전환 메서드
    public void ChangeScene(eSceneType sceneType)
    {
        switch (sceneType)
        {
            case eSceneType.Title:
                var titleOper = SceneManager.LoadSceneAsync("Title");
                titleOper.completed += (obj) =>
                {   
                    //이벤트 중복 방지, 제거
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.StartMainGame, StartMainGame);
                    //메인 게임 시작 이벤트 등록
                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.StartMainGame, StartMainGame);
                };
                break;

            case eSceneType.Stage01:
                var stage01Oper = SceneManager.LoadSceneAsync("Stage01");
                stage01Oper.completed += (obj) =>
                {
                    //이벤트 중복 방지, 제거
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);
                    //메인 게임 다음 스테이지로 이동 이벤트 등록
                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                };
                break;

            case eSceneType.Stage02:
                var stage02Oper = SceneManager.LoadSceneAsync("Stage02");
                stage02Oper.completed += (obj) =>
                {
                    //이벤트 중복 방지, 제거
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                };
                break;

            case eSceneType.Stage03:
                var stage03Oper = SceneManager.LoadSceneAsync("Stage03");
                stage03Oper.completed += (obj) =>
                {
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                };
                break;

            case eSceneType.Stage04:
                var stage04Oper = SceneManager.LoadSceneAsync("Stage04");
                stage04Oper.completed += (obj) =>
                {
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                };
                break;

            case eSceneType.Stage05:
                var stage05Oper = SceneManager.LoadSceneAsync("Stage05");
                stage05Oper.completed += (obj) =>
                {
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                };
                break;

            case eSceneType.Stage06:
                var stage06Oper = SceneManager.LoadSceneAsync("Stage06");
                stage06Oper.completed += (obj) =>
                {
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                };
                break;

            case eSceneType.Stage07:
                var stage07Oper = SceneManager.LoadSceneAsync("Stage07");
                stage07Oper.completed += (obj) =>
                {
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                };
                break;

            case eSceneType.Stage08:
                var stage08Oper = SceneManager.LoadSceneAsync("Stage08");
                stage08Oper.completed += (obj) =>
                {
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                };
                break;

            case eSceneType.Stage09:
                var stage09Oper = SceneManager.LoadSceneAsync("Stage09");
                stage09Oper.completed += (obj) =>
                {
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                    EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                };
                break;

            case eSceneType.Stage10:
                var stage10Oper = SceneManager.LoadSceneAsync("Stage10");
                stage10Oper.completed += (obj) =>
                {
                    EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ClearStage, ChangeMainGame);

                };
                break;

            case eSceneType.Avoid:
                var avoidOper = SceneManager.LoadSceneAsync("Avoid");
                avoidOper.completed += (obj) =>
                {

                };
                break;

            case eSceneType.Flappy:
                var flappyOper = SceneManager.LoadSceneAsync("Flappy");
                flappyOper.completed += (obj) =>
                {

                };
                break;

            case eSceneType.Hardest:
                var hardestOper = SceneManager.LoadSceneAsync("Hardest");
                hardestOper.completed += (obj) =>
                {

                };
                break;

            case eSceneType.Jumping:
                var jumpingOper = SceneManager.LoadSceneAsync("Jumping");
                jumpingOper.completed += (obj) =>
                {

                };
                break;

            case eSceneType.Matching:
                var matchingOper = SceneManager.LoadSceneAsync("Matching");
                matchingOper.completed += (obj) =>
                {

                };
                break;

            case eSceneType.Maze:
                var mazeOper = SceneManager.LoadSceneAsync("Maze");
                mazeOper.completed += (obj) =>
                {

                };
                break;

            case eSceneType.Memory:
                var memoryOper = SceneManager.LoadSceneAsync("Memory");
                memoryOper.completed += (obj) =>
                {

                };
                break;
        }
    }
}
