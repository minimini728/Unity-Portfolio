using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDropItem : MonoBehaviour
{
    public GameObject[] box; //�����ۻ��� ����
    public GameObject effectPrefab; //�ѿ� �¾� ������ ����Ʈ
    public GameObject orbOfLifePrefab; //������ ���� ������
    public GameObject orbOfPowerPrefab; //���� ���� ������
    void Start()
    {   
        //�ʱ� ����
        box[0].gameObject.SetActive(true);
        box[1].gameObject.SetActive(true);
        box[2].gameObject.SetActive(true);
        this.effectPrefab.GetComponent<ParticleSystem>().Stop();

    }

    public void PlayEffect()
    {
        box[0].gameObject.SetActive(false);
        box[1].gameObject.SetActive(false);
        box[2].gameObject.SetActive(false);

        this.effectPrefab.GetComponent<ParticleSystem>().Play();

        this.DropItem();
    }

    void DropItem()
    {
        float randomValue = Random.value; //0���� 1 ������ ������ �� ����

        if (randomValue < 0.65f) //0.65���� ������ ������ ���� ������ ���
        {
            Instantiate(orbOfLifePrefab, this.transform.position, this.transform.rotation);
        }
        else //�׷��� ������ ���� ���� ������ ���
        {
            Instantiate(orbOfPowerPrefab, this.transform.position, this.transform.rotation);
        }

        Destroy(this);
    }

}
