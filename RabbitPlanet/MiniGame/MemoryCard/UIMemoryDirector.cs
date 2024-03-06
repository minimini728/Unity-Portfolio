using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMemoryDirector : MonoBehaviour
{
    public UIMemoryClear uiclear;
    public UIGameOver uiGameOver;

    public Text timerText; //Ÿ�̸�
    public float totalTime = 60f; //���ѽð� 60��

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
        //�ð��� ������ �ݿø��Ͽ� �ؽ�Ʈ�� ǥ��
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
