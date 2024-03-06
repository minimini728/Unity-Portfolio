using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOutPostDirector : MonoBehaviour
{
    public GameObject UIInventory; //�κ��丮 UI
    public GameObject UIBuildNotice; //���ʱ��� �Ǽ� UI

    public UITutorialFinal UITutorial;
    void Start()
    {
        this.UIInventory.GetComponent<UIInventory>().Init(); //�κ��丮 UI �ʱ�ȭ
    }
    private void Update()
    {
        StartCoroutine(ShowUIInventory());
    }

    IEnumerator ShowUIInventory()
    {
        //TabŰ�� ������ ���� �κ��丮 UI �����ֱ�
        if (Input.GetKey(KeyCode.Tab)) 
        {
            this.UIInventory.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            this.UIInventory.gameObject.SetActive(false);
        }

        yield return null;

    }
    public void ShowUIBuildNotice() //���ʱ��� �Ǽ� UI �����ֱ�
    {
        this.UIBuildNotice.gameObject.SetActive(true);
    }

    public void HideUIBuildNotice() //���ʱ��� �Ǽ� UI ������
    {
        this.UIBuildNotice.gameObject.SetActive(false);
    }

}
