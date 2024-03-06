using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardestRightEnemy : MonoBehaviour
{
    [SerializeField]
    private bool moveLeft;
    private float moveSpeed = 3f;

    void Update()
    {
        if (moveLeft)
        {
            Vector3 temp = transform.position;
            temp.x += moveSpeed * Time.deltaTime;
            transform.position = temp;
        }
        else
        {
            Vector3 temp = transform.position;
            temp.x -= moveSpeed * Time.deltaTime;
            transform.position = temp;
        }
    }
    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Rock"))
        {
            moveLeft = !moveLeft;
        }

        if (target.gameObject.CompareTag("Player"))
        {
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.HardestGameOver);
            Destroy(this.gameObject);
        }
    }
}
