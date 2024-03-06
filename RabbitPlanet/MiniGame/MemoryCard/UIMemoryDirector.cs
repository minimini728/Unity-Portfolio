using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMemoryDirector : MonoBehaviour
{
    public UIMemoryClear uiclear;
    public UIGameOver uiGameOver;

    public Text timerText; //타이머
    public float totalTime = 60f; //제한시간 60초

    private float currentTime;
    void Start()
    {
        currentTime = totalTime;
        UpdateTimerDisplay();
    }
    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            currentTime = 0;
            UpdateTimerDisplay();
            
            if(this.uiGameOver != null)
            {
                this.uiGameOver.gameObject.SetActive(true);
            }
        }
    }
    private void UpdateTimerDisplay()
    {
        //시간을 정수로 반올림하여 텍스트에 표시
        timerText.text = Mathf.Ceil(currentTime).ToString();
    }
    public void ShowUIClear()
    {   
        if(this.uiclear != null)
        {
            this.uiclear.gameObject.SetActive(true);
        }
    }
}
