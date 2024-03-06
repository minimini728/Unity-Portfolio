using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints; //경유지
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private float moveSpeed;
    private int wayPointCount;
    private int currentIndex = 0;

    private void Awake()
    {
        wayPointCount = wayPoints.Length;

        transform.position = wayPoints[currentIndex].position;

        currentIndex++;

        StartCoroutine("MoveTo");
    }

    private IEnumerator MoveTo()
    {
        while (true)
        {
            yield return StartCoroutine("Movement");

            yield return new WaitForSeconds(waitTime);

            if (currentIndex < wayPointCount - 1)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
        }
    }

    private IEnumerator Movement()
    {
        while (true)
        {
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.1f)
            {
                transform.position = wayPoints[currentIndex].position;

                break;

            }

            yield return null;

        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //Debug.Log("플레이어 들어옴");
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //Debug.Log("플레이어 나감");
            collision.transform.SetParent(null);
        }
    }
}
