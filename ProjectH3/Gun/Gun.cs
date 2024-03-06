using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform gunTip; //�ѱ� ��ġ
    public GameObject flashParticle;
    private void Start()
    {
        flashParticle.GetComponent<ParticleSystem>().Stop();

    }
    public void Shoot()
    {
        flashParticle.GetComponent<ParticleSystem>().Play();
    }
}
