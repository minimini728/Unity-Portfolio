using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    public float moveSpeed = 4f; //아이템 이동 속도
    public float magnetDistance = 4f; //아이템과 플레이어 사이 거리

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position); //아이템과 플레이어 사이 거리 계산

        if(distance <= magnetDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 
                moveSpeed * Time.deltaTime); //아이템이 플레이어에게로 이동
        }
    }
}
