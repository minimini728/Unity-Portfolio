using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutPostMain : MonoBehaviour
{
    public UIOutPostDirector director; //���ʱ��� ĵ����

    public GameObject beforeBuilding; //���ʱ��� ��
    public GameObject afterBuilding; //���ʱ��� �ǹ�
    public GameObject interactionObj; //���ʱ��� �˸� ����ǥ
    void Start()
    {   
        //�̺�Ʈ�� ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.BuildOutPost, this.BuildOutPost);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ShowUIBuildNotice, this.ShowUIBuildNotice);
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.HideUIBuildNotice, this.HideUIBuildNotice);


        //���ʱ����� �̹� ���������� �� �ǹ� ��Ȱ��ȭ, �� �ǹ� Ȱ��ȭ, ����Ʈ ��Ȱ��ȭ
        if (InfoManager.instance.OutPostInfos.isBuild == true)
        {
            this.beforeBuilding.gameObject.SetActive(false);
            this.afterBuilding.gameObject.SetActive(true);
            this.interactionObj.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        Cursor.visible = false; //���콺 Ŀ�� �����
    }
    void BuildOutPost(short type) //���ʱ��� �Ǽ� �޼���
    {
        this.beforeBuilding.gameObject.SetActive(false); //���ʱ��� �� ��Ȱ��ȭ
        this.afterBuilding.gameObject.SetActive(true); //���ʱ��� �ǹ� Ȱ��ȭ
        this.afterBuilding.GetComponent<Building>().StartBuild(); //���ʱ��� �Ǽ� ����

        InfoManager.instance.OutPostInfos.isBuild = true; //���ʱ��� �Ǽ��� ����
        InfoManager.instance.SaveOutPostInfo();

        this.interactionObj.gameObject.SetActive(false); //���ʱ��� �˸� ����ǥ ��Ȱ��ȭ

        StartCoroutine(this.ShowDialogue());

    }

    void ShowUIBuildNotice(short type) //���ʱ��� ������� �˸� UI �����ֱ�
    {
        this.director.ShowUIBuildNotice();
    }

    void HideUIBuildNotice(short type) //���ʱ��� ������� �˸� UI ������
    {
        this.director.HideUIBuildNotice();
    }
    private void OnDisable()
    {   
        //�̺�Ʈ �ߺ� ���� �̺�Ʈ ����
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
