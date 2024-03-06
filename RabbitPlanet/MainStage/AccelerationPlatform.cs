using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPlatform : MonoBehaviour
{
    [SerializeField]
    private float accelForce;
    [SerializeField]
    private Vector3 direction;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Movement3D>().MoveTo(direction, accelForce);
        }
    }

}
