using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTool : MonoBehaviour
{
    void Start()
    {   
        if(InfoManager.instance.MagicToolInfo.Find(x => x.id == 300).level >= 1 ) //마법도구 레벨 가져오기
        {
            this.SpeedUp(); //신속 속성 메서드
        }
    }
    public void Init()
    {
        Debug.Log("마법도구 Init");

        //마법도구 생성, 업그레이드 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)LHMEventType.eEventType.CREATED_MAGIC_TOOL, new EventHandler((type) =>
        {
            this.CreateMagicTool();
        }));

        EventDispatcher.instance.AddEventHandler((int)LHMEventType.eEventType.UPGRADE_MAGIC_TOOL, new EventHandler((type) =>
        {
            this.UpgradeMagicTool();
        }));

        //마법 조각 획득 이벤트 등록 a: 꿈의 조각 id
        EventDispatcher.instance.AddEventHandler<int>((int)LHMEventType.eEventType.GET_DREAM_PIECE, new EventHandler<int>((type, a) =>
        {
            this.GetDreamPiece(a);

            //마법 도구 UI 레벨 체크 이벤트 전송 -> UIMagicTool 클래스로
            EventDispatcher.instance.SendEvent((int)LHMEventType.eEventType.CHECK_MAGICTOOL_LEVEL);

            InfoManager.instance.SaveDreamPieceInfo();
        }));

    }

    public void GetDreamPiece(int num) //꿈의 조각 획득 메서드
    {
        var data = DataManager.instance.GetDreamPieceData(num); //꿈의 조각 정보 가져오기

        var id = data.id;
        var foundInfo = InfoManager.instance.DreamPieceInfo.Find(x => x.id == id);

        if (foundInfo == null) //꿈의 조각 처음 획득
        {
            DreamPieceInfo info = new DreamPieceInfo(id, 1);
            InfoManager.instance.DreamPieceInfo.Add(info);
        }
        else
        {
            foundInfo.amount++;
        }

        //마법 도구 UI Refresh 이벤트 전송 -> UIMagicTool 클래스로
        EventDispatcher.instance.SendEvent((int)LHMEventType.eEventType.REFRESH_UI_MAGICTOOL);

    }

    public void CreateMagicTool() //마법 도구 생성 메서드
    {
        Debug.Log("<color=yellow>마법 도구 생성</color>");

        var magicPiece = InfoManager.instance.MagicToolInfo.Find(x => x.id == 300); //마법 도구 데이터 가져오기
        if (magicPiece != null) //마법 도구가 level 0 인 상태
        {
            magicPiece.level = 1; //마법 도구 level 1로 
        }

        var speedPiece = new MagicToolInfo(310); //마법 도구 신속 속성 붙이기
        InfoManager.instance.MagicToolInfo.Add(speedPiece);
        var detoxPiece = new MagicToolInfo(320); //마법 도구 정화 속성 붙이기
        InfoManager.instance.MagicToolInfo.Add(detoxPiece);
        var wisdomPiece = new MagicToolInfo(330); //마법 도구 지혜 속성 붙이기
        InfoManager.instance.MagicToolInfo.Add(wisdomPiece);

        InfoManager.instance.SaveMagicToolInfo(); //마법 도구 저장하기

        //마법 도구 생성 이펙트 재생
        EventDispatcher.instance.SendEvent((int)LHMEventType.eEventType.CREATE_MAGIC_CIRCLE);

        SpeedUp();
    }

    public void UpgradeMagicTool() //마법 도구 업그레이드 메서드
    {
        var magicPiece = InfoManager.instance.MagicToolInfo.Find(x => x.id == 300);
        if (magicPiece != null)
        {
            magicPiece.level++;
        }

        var speedPiece = InfoManager.instance.MagicToolInfo.Find(x => x.id == 310);
        if (speedPiece != null)
        {
            speedPiece.level++;
        }

        var detoxPiece = InfoManager.instance.MagicToolInfo.Find(x => x.id == 320);
        if (detoxPiece != null)
        {
            detoxPiece.level++;
        }

        var widsdomPiece = InfoManager.instance.MagicToolInfo.Find(x => x.id == 330);
        if (widsdomPiece != null)
        {
            widsdomPiece.level++;
        }

        InfoManager.instance.SaveMagicToolInfo(); //마법 도구 저장하기

        Debug.LogFormat("<color=yellow>마법도구 {0}단계 업그레이드</color>", InfoManager.instance.MagicToolInfo.Find(x => x.id == 300).level);

        SpeedUp();
    }

    //정화하기
    public void Detox()
    {

    }
    //이속증가하기
    public void SpeedUp()
    {
        //마법도구 레벨에 따른 이속 변화
        var myLevel = InfoManager.instance.MagicToolInfo.Find(x => x.id == 300).level; //마법도구 레벨 가져오기

        if (myLevel >= 1) //1레벨 이상이면
        {
            var data = DataManager.instance.GetMagicToolLevelDatas().Find(x => x.level == myLevel); //레벨 데이터 담기

            var player = GameObject.FindObjectOfType<PlayerMono>();

            player.speed = 5 * data.add_speed_property; //현재 속도 * 이속능력

            var farming = GameObject.FindObjectOfType<Farming>();
            if (farming != null)
            {
                farming.GetOriginalValues();
                farming.SetPoisonDuration(data.add_detox_property);
            }

        }
        
    }

}
