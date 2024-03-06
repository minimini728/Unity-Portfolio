using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutPostMain : MonoBehaviour
{
    public UIOutPostDirector director; //전초기지 캔버스

    public GameObject beforeBuilding; //전초기지 터
    public GameObject afterBuilding; //전초기지 건물
    public GameObject interactionObj; //전초기지 알림 물음표
    void Start()
    {   
        //이벤트들 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.BuildOutPost, this.BuildOutPost);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ShowUIBuildNotice, this.ShowUIBuildNotice);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.HideUIBuildNotice, this.HideUIBuildNotice);


        //전초기지가 이미 지어졌으면 전 건물 비활성화, 후 건물 활성화, 이펙트 비활성화
        if (InfoManager.instance.OutPostInfos.isBuild == true)
        {
            this.beforeBuilding.gameObject.SetActive(false);
            this.afterBuilding.gameObject.SetActive(true);
            this.interactionObj.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        Cursor.visible = false; //마우스 커서 숨기기
    }
    void BuildOutPost(short type) //전초기지 건설 메서드
    {
        this.beforeBuilding.gameObject.SetActive(false); //전초기지 터 비활성화
        this.afterBuilding.gameObject.SetActive(true); //전초기지 건물 활성화
        this.afterBuilding.GetComponent<Building>().StartBuild(); //전초기지 건설 시작

        InfoManager.instance.OutPostInfos.isBuild = true; //전초기지 건설로 변경
        InfoManager.instance.SaveOutPostInfo();

        this.interactionObj.gameObject.SetActive(false); //전초기지 알림 물음표 비활성화

        StartCoroutine(this.ShowDialogue());

    }

    void ShowUIBuildNotice(short type) //전초기지 건축재료 알림 UI 보여주기
    {
        this.director.ShowUIBuildNotice();
    }

    void HideUIBuildNotice(short type) //전초기지 건축재료 알림 UI 가리기
    {
        this.director.HideUIBuildNotice();
    }
    private void OnDisable()
    {   
        //이벤트 중복 방지 이벤트 제거
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.BuildOutPost, this.BuildOutPost);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.ShowUIBuildNotice, this.ShowUIBuildNotice);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnum.eEventType.HideUIBuildNotice, this.HideUIBuildNotice);
    }

    IEnumerator ShowDialogue()
    {
        yield return new WaitForSeconds(5f);

        this.director.UITutorial.speechBubble0.SetActive(true);
    }
}
