using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float activationDelay = 1.0f; //건물 건설 지연 시간
    
    void Start()
    {
        if(InfoManager.instance.OutPostInfos.isBuild == true) //건초기지를 이미 지은 상태
        {
            foreach (Transform child in transform)
            {
                if (!child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    public void StartBuild()
    {
        //1초 간격으로 ActivateNextChild 함수 호출 시작
        InvokeRepeating("ActivateNextChild", 0.0f, activationDelay);

    }
    void ActivateNextChild()
    {
        //다음 비활성화된 자식 오브젝트를 찾아 활성화
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(true);
                break; //다음 자식 오브젝트를 찾지 않고 루프 종료
            }
        }

    }
}
