using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Inventory : MonoBehaviour
{
    public TimeSpan offlineTimeSpan;
    private string ExitTimeKey = "ExitTime";

    private float timer;  //Ÿ�̸� ����
    private float interval = 300f;  //������ ���� ���� (5�� = 300��)

    //�׽�Ʈ����������ȣ
    private int stageNum;


    private void Start()
    {
        string.IsNullOrEmpty(DataManager.instance.GetNpcData(3005).prefab_name);

        //���� �������� ��ȣ ��������
        foreach (StageInfo stageInfo in InfoManager.instance.StageInfos)
        {
            Debug.LogFormat("<color>stageInfo.stage : {0}, stageInfo.isClear : {1}</color>", stageInfo.stage, stageInfo.isClear);
            if (stageInfo.isClear == false)
            {
                //���ϴ°��� stageInfo.num ����
                this.stageNum = stageInfo.stage;
                break;
            }
        }

    }

    public void Init()
    {
        //��� ȹ�� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler<int>((int)LHMEventType.eEventType.GET_INGREDIENT, this.GetIngredientEventHandler);

        //�����ϱ� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler<int>((int)LHMEventType.eEventType.COMBINE_INGREDIENT, this.CombineIngredientEventHandler);

        //�������� ��� ���� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler<DateTime>((int)LHMEventType.eEventType.GIVE_OFFLINE_INGREDIENT, this.GiveOfflineIngredientEventHandler);

    }

    private void GetIngredientEventHandler(short type, int a)
    {
        //�������� ������ ���� ��� ȹ�� ���� ����
        var myLevel = InfoManager.instance.MagicToolInfo.Find(x => x.id == 300).level;
        if (myLevel == 0) //�������� ���� ��
        {
            this.GetIngredient(a);
        }
        else if (myLevel >= 1) //�������� ���� ��
        {
            var data = DataManager.instance.GetMagicToolLevelDatas().Find(x => x.level == myLevel);
            for (int i = 0; i < data.add_magic_property; i++) //�������� ������ ���� ��� ȹ�淮 ����
            {
                this.GetIngredient(a);
            }
        }

        //Ʃ�丮�� ���� ȹ��
        if (a == 2004 && InfoManager.instance.IngredientInfos.Find(x => x.id == 2004).amount == 1)
        {

        }
        else if (a == 2004 && InfoManager.instance.IngredientInfos.Find(x => x.id == 2004).amount == 2)
        {
            EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_BOOK_ITEM, 6000);
        }

        //������ ���� ȹ��
        float normalItemProb = 0.14f;
        float rareItemProb = 0.08f; //0.08

        int[] actions = new int[] { 2000, 2001, 2002, 2003, 2004 };

        //���� �������� ��ȣ ��������
        foreach (StageInfo stageInfo in InfoManager.instance.StageInfos)
        {
            Debug.LogFormat("<color>stageInfo.stage : {0}, stageInfo.isClear : {1}</color>", stageInfo.stage, stageInfo.isClear);
            if (stageInfo.isClear == false)
            {
                //���ϴ°��� stageInfo.num ����
                this.stageNum = stageInfo.stage;
                break;
            }
        }

        //6�������� �̻��϶����� ����ȹ��
        if (this.stageNum >= 5)
        {
            foreach (int action in actions)
            {
                int itemAmount = InfoManager.instance.IngredientInfos.Find(x => x.id == action).amount;
                int magicToolLevel = InfoManager.instance.MagicToolInfo.Find(x => x.id == 300).level;

                if (a == action && magicToolLevel == 0)//��������X�϶� ���� ȹ���ϱ�
                {
                    float randomGetValue = Random.value;
                    if (randomGetValue <= normalItemProb) //��־����� ȹ���ϱ�
                    {
                        int randomItemValue = 0;
                        switch (action)
                        {
                            case 2000:
                                randomItemValue = Random.Range(6002, 6005);
                                break;
                            case 2001:
                                randomItemValue = Random.Range(6009, 6012);
                                break;
                            case 2002:
                                randomItemValue = Random.Range(6016, 6019);
                                break;
                            case 2003:
                                randomItemValue = Random.Range(6023, 6026);
                                break;
                            case 2004:
                                randomItemValue = Random.Range(6030, 6033);
                                break;
                        }
                        EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_BOOK_ITEM, randomItemValue);
                    }
                    else //���� ������ �� ����
                    {
                        Debug.Log("No item acquired");
                    }
                }
                else if (a == action && magicToolLevel >= 1)//��������O(����)�϶� ���� ȹ���ϱ�
                {
                    var wisdom = InfoManager.instance.MagicToolInfo.Find(x => x.id == 330);
                    var data = DataManager.instance.GetMagicToolLevelDatas().Find(x => x.level == wisdom.level);
                    float plusProb = data.add_wisdom_property * 0.01f; //�������� ���� �Ӽ� ������ ���� ���� ȹ�淮 ����

                    float randomGetValue = Random.value;
                    int randomItemValue = 0;
                    if (randomGetValue <= normalItemProb) //14% Ȯ���� ��� �������� ȹ��
                    {
                        switch (action)
                        {
                            case 2000:
                                randomItemValue = Random.Range(6002, 6005);
                                break;
                            case 2001:
                                randomItemValue = Random.Range(6009, 6012);
                                break;
                            case 2002:
                                randomItemValue = Random.Range(6016, 6019);
                                break;
                            case 2003:
                                randomItemValue = Random.Range(6023, 6026);
                                break;
                            case 2004:
                                randomItemValue = Random.Range(6030, 6033);
                                break;
                        }
                        EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_BOOK_ITEM, randomItemValue);
                    }
                    else if (randomGetValue > normalItemProb && randomGetValue <= normalItemProb + rareItemProb + plusProb) //���� ���� Ȯ���� ���� �������� ȹ��
                    {
                        Debug.LogFormat("���� ������ {0}%�� ����", rareItemProb + plusProb);
                        switch (action)
                        {
                            case 2000:
                                randomItemValue = Random.Range(6005, 6009);
                                break;
                            case 2001:
                                randomItemValue = Random.Range(6012, 6016);
                                break;
                            case 2002:
                                randomItemValue = Random.Range(6019, 6023);
                                break;
                            case 2003:
                                randomItemValue = Random.Range(6026, 6030);
                                break;
                            case 2004:
                                randomItemValue = Random.Range(6033, 6037);
                                break;
                        }
                        EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_BOOK_ITEM, randomItemValue);
                    }
                    else //���� ������ �� ����
                    {
                        Debug.Log("No item acquired");
                    }
                }
            }
        }
    }

    private void Update()
    {        
        if (stageNum >= 5) //6������������ �ڵ�����
        {
            //�ð� ������Ʈ
            timer += Time.deltaTime;
            //������ ����
            if (timer >= interval)
            {
                timer = 0f;  //Ÿ�̸� �ʱ�ȭ

                //������ ����
                Debug.Log("<color=#FFB6C1>�ڵ����� ȣ��</color>");
                GiveGift();

            }
        }

    }
    
    private void OnApplicationPause(bool isPause) //���� �������� �ð� ���� �޼���
    {
        if (isPause)
        {
            //���� ���� �� ���� �ð��� ����
            DateTime currentExitTime = DateTime.Now;
            string exitTimeStr = Convert.ToString(currentExitTime.ToBinary());

            PlayerPrefs.SetString(ExitTimeKey, exitTimeStr);
            PlayerPrefs.Save();

            Debug.Log("���� �ð� ����: " + currentExitTime.ToString());
        }
    }
    private void OnApplicationQuit() //���� �������� �ð� ���� �޼���
    {
        //���� ���� �� ���� �ð��� ����
        DateTime currentExitTime = DateTime.Now;
        string exitTimeStr = Convert.ToString(currentExitTime.ToBinary());

        PlayerPrefs.SetString(ExitTimeKey, exitTimeStr);
        PlayerPrefs.Save();

        Debug.Log("���� �ð� ����: " + currentExitTime.ToString());
    }

    private void GiveOfflineIngredientEventHandler(short type, DateTime time) //�������� ���� �̺�Ʈ �޼���
    {
        this.OfflineAutoGift(time);
    }

    public void OfflineAutoGift(DateTime lastTime) //�������� ���� ���� ��� �޼���
    {
        //�������νð��� ����ð� �� ���
        var currentTime = DateTime.Now;

        TimeSpan spanStart = new TimeSpan(lastTime.Day, lastTime.Hour, lastTime.Minute, lastTime.Second);
        TimeSpan spanEnd = new TimeSpan(currentTime.Day, currentTime.Hour, currentTime.Minute, currentTime.Second);
        TimeSpan spanGap = spanEnd.Subtract(spanStart);

        Debug.Log("lastTIme: " + lastTime);
        Debug.Log("currentTime: " + currentTime);
        Debug.Log("spanGap: " + spanGap);

        TimeSpan twoHours = new TimeSpan(2, 0, 0);
        int maxGiftCount = (int)(spanGap.TotalMinutes / 5); //5�и��� 1���� �������ִ� �ִ� Ƚ��
        
        Debug.LogFormat("<color=yellowr>{0}</color>", maxGiftCount);

        //2�ð� �̻� ���� ���, 2�ð��� �ش��ϴ�
        if (spanGap >= twoHours)
        {
            Debug.Log("<color=yellowr>2�ð��� �ش��ϴ� �������� ����");
            for (int i = 0; i < 24; i++) //2�ð��� �ش��ϴ� 24���� ���� Ƚ���� ����
            {
                OfflineGiveGift();
            }
        }
        //2�ð� �̸��� ���, spanGap�� �ش��ϴ�
        else
        {
            Debug.Log("<color=yellowr>2�ð� �̸� �������� ����");
            for (int i = 0; i < maxGiftCount; i++)
            {
                OfflineGiveGift();
            }
        }

        Debug.Log("<color=pink>�������μ���â �̺�Ʈ</color>");
        //�������� ���� ���� UI �����ִ� �̺�Ʈ ���� -> UIOffline Ŭ������
        EventDispatcher.instance.SendEvent<TimeSpan>((int)LHMEventType.eEventType.SHOW_OFFLINE_GIFT_EXPLAIN, spanGap);
    }
    private void OfflineGiveGift() //�������� ��� ���� �޼���
    {
        //��Ḧ �ִ� �ڵ�
        int[] ingredientIds = { 2000, 2001, 2002, 2003, 2004 };

        foreach (int id in ingredientIds)
        {
            this.GetIngredient(id);
        }

    }

    private void GiveGift() //�ڵ����� �޼���
    {
        //��Ḧ �ִ� �ڵ�
        Debug.Log("<color=yellow>�ڵ����޽���</color>");

        int[] ingredientIds = { 2000, 2001, 2002, 2003, 2004 };

        foreach (int id in ingredientIds) //�ڵ����� ���� ���� ����
        {
            int autoCount = InfoManager.instance.IngredientInfos.Find(x => x.id == id)?.auto ?? 0;

            for (int i = 0; i < autoCount; i++)
            {
                this.GetIngredient(id);
            }
        }

    }
    private void CombineIngredientEventHandler(short type, int num) //��� ���� �̺�Ʈ �޼���
    {
        this.GetIngredient(num);
    }

    public void GetIngredient(int num) //��� ȹ�� �޼���
    {
        var data = DataManager.instance.GetIngredientData(num);

        var id = data.id;
        var foundInfo = InfoManager.instance.IngredientInfos.Find(x => x.id == id);

        if (foundInfo == null) //��Ḧ ó�� ȹ��
        {
            IngredientInfo info = new IngredientInfo(id);
            InfoManager.instance.IngredientInfos.Add(info);
            if (info.id == 2004) //5������������ ���� Ʃ�丮�� ����
            {
                EventDispatcher.instance.SendEvent<int>((int)LHMEventType.eEventType.GET_BOOK_ITEM, 6001);
            }
        }
        else
        {
            foundInfo.amount++;
        }

        InfoManager.instance.SaveIngredientInfos();
        //�κ��丮 UI refresh ���ִ� �̺�Ʈ ���� -> UIInventoryScroll Ŭ������
        EventDispatcher.instance.SendEvent((int)LHMEventType.eEventType.REFRESH_UI_INVENTORY);

    }
    private void OnDestroy()
    {   
        //�̺�Ʈ �ߺ� ����, �̺�Ʈ ����
        EventDispatcher.instance.RemoveEventHandler<int>((int)LHMEventType.eEventType.COMBINE_INGREDIENT, this.CombineIngredientEventHandler);
        EventDispatcher.instance.RemoveEventHandler<DateTime>((int)LHMEventType.eEventType.GIVE_OFFLINE_INGREDIENT, this.GiveOfflineIngredientEventHandler);
        EventDispatcher.instance.RemoveEventHandler<int>((int)LHMEventType.eEventType.GET_INGREDIENT, this.GetIngredientEventHandler);

    }

}
