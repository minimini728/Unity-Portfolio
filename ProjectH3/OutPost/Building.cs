using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float activationDelay = 1.0f; //�ǹ� �Ǽ� ���� �ð�
    
    void Start()
    {
        if(InfoManager.instance.OutPostInfos.isBuild == true) //���ʱ����� �̹� ���� ����
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
        //1�� �������� ActivateNextChild �Լ� ȣ�� ����
        InvokeRepeating("ActivateNextChild", 0.0f, activationDelay);

    }
    void ActivateNextChild()
    {
        //���� ��Ȱ��ȭ�� �ڽ� ������Ʈ�� ã�� Ȱ��ȭ
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(true);
                break; //���� �ڽ� ������Ʈ�� ã�� �ʰ� ���� ����
            }
        }

    }
}
