using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    public Button btnTitle;
    public Button btnAd;

    private FullAdmob admobManager;

    private void Awake()
    {
        btnAd.enabled = true;

    }
    void Start()
    {
        admobManager = FindObjectOfType<FullAdmob>();

        this.btnTitle.onClick.AddListener(() =>
        {   
            //���ο� ���� ���� �̺�Ʈ ���� -> App Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.NewGame);
        });

        this.btnAd.onClick.AddListener(() =>
        {
            admobManager.LoadLoadInterstitialAd(); //���� ��û �� �̴ϰ��� �ѹ� �� ����
            btnAd.enabled = false;

        });

    }

}
