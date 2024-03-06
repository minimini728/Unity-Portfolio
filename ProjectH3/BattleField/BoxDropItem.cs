using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDropItem : MonoBehaviour
{
    public GameObject[] box; //아이템상자 묶음
    public GameObject effectPrefab; //총에 맞아 터지는 이펙트
    public GameObject orbOfLifePrefab; //생명의 구슬 아이템
    public GameObject orbOfPowerPrefab; //힘의 구슬 아이템
    void Start()
    {   
        //초기 설정
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
        float randomValue = Random.value; //0에서 1 사이의 무작위 값 생성

        if (randomValue < 0.65f) //0.65보다 작으면 생명의 구슬 아이템 드랍
        {
            Instantiate(orbOfLifePrefab, this.transform.position, this.transform.rotation);
        }
        else //그렇지 않으면 힘의 구슬 아이템 드랍
        {
            Instantiate(orbOfPowerPrefab, this.transform.position, this.transform.rotation);
        }

        Destroy(this);
    }

}
