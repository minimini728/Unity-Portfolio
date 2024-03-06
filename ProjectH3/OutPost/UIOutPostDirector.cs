using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOutPostDirector : MonoBehaviour
{
    public GameObject UIInventory; //인벤토리 UI
    public GameObject UIBuildNotice; //전초기지 건설 UI

    public UITutorialFinal UITutorial;
    void Start()
    {
        this.UIInventory.GetComponent<UIInventory>().Init(); //인벤토리 UI 초기화
    }
    private void Update()
    {
        StartCoroutine(ShowUIInventory());
    }

    IEnumerator ShowUIInventory()
    {
        //Tab키를 누르는 동안 인벤토리 UI 보여주기
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
    public void ShowUIBuildNotice() //전초기지 건설 UI 보여주기
    {
        this.UIBuildNotice.gameObject.SetActive(true);
    }

    public void HideUIBuildNotice() //전초기지 건설 UI 가리기
    {
        this.UIBuildNotice.gameObject.SetActive(false);
    }

}
