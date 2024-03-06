using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardestLeftEnemy : MonoBehaviour
{
    [SerializeField]
    private bool moveRight;
    private float moveSpeed = 3f;
    
    void Update()
    {   
        if (moveRight)
        {
            Vector3 temp = transform.position;
            temp.x -= moveSpeed * Time.deltaTime;
            transform.position = temp;
        }
        else
        {
            Vector3 temp = transform.position;
            temp.x += moveSpeed * Time.deltaTime;
            transform.position = temp;
        }
    }
    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Rock"))
        {
            moveRight = !moveRight;
        }

        if (target.gameObject.CompareTag("Player"))
        {   
            //게임 오버 이벤트 전송 -> HaredstMain 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.HardestGameOver);
            Destroy(this.gameObject);
        }
    }
}
