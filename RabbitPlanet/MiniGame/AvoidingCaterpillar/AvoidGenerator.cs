using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidGenerator : MonoBehaviour
{
    public GameObject enemyPrefab; //����
    public GameObject cloverPrefab; //Ŭ�ι�

    private float generatorEnemyPosition; //���� ���� ��ġ
    private float generatorCloverPosition; //Ŭ�ι� ���� ��ġ
    private bool isPause;
    
    void Start()
    {
        this.isPause = false;
        float wormTime = Random.Range(1f, 2f);
        float cloverTime = Random.Range(1f, 2f);
        InvokeRepeating("GenWorm", 1f, wormTime);
        InvokeRepeating("GenClover", 1f, cloverTime);

    }

    void Update()
    {
        generatorEnemyPosition = Random.Range(-10f, 10f);
        generatorCloverPosition = Random.Range(-10f, 10f);
    }

    void GenWorm()
    {   
        if(isPause != true)
        {
            var enemy = Instantiate(enemyPrefab, new Vector3(generatorEnemyPosition, 8.6f, 0), this.transform.rotation);
        }
    }
    void GenClover()
    {
        if (isPause != true)
        {
            var clover = Instantiate(cloverPrefab, new Vector3(generatorCloverPosition, 8.6f, 0), this.transform.rotation);
        }
    }

    public void Pause()
    {
        this.isPause = true;
    }
}
