using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    public float moveSpeed = 4f; //������ �̵� �ӵ�
    public float magnetDistance = 4f; //�����۰� �÷��̾� ���� �Ÿ�

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position); //�����۰� �÷��̾� ���� �Ÿ� ���

        if(distance <= magnetDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 
                moveSpeed * Time.deltaTime); //�������� �÷��̾�Է� �̵�
        }
    }
}
