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
            //새로운 게임 실행 이벤트 전송 -> App 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.NewGame);
        });

        this.btnAd.onClick.AddListener(() =>
        {
            admobManager.LoadLoadInterstitialAd(); //광고 시청 후 미니게임 한번 더 실행
            btnAd.enabled = false;

        });

    }

}
