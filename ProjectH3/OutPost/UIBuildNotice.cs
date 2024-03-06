using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildNotice : MonoBehaviour
{
    public Button btnBuild;
    void Start()
    {
        if (InfoManager.instance.InventoryInfos.Find(x => x.id == 100).amount >= 5 &&
            InfoManager.instance.InventoryInfos.Find(x => x.id == 101).amount >= 5 &&
            InfoManager.instance.InventoryInfos.Find(x => x.id == 103).amount >= 5)
        {
            this.btnBuild.interactable = true;
        }
        else
        {
            this.btnBuild.interactable = false;
        }

        this.btnBuild.onClick.AddListener(() =>
        {
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.BuildOutPost);
            this.gameObject.SetActive(false);
        });
    }
    void Update()
    {
        // Y 키를 누르면 btnBuild 버튼 눌리게
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (this.btnBuild.interactable)
            {
                this.btnBuild.onClick.Invoke();
            }
        }
    }
}
