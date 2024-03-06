using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObstacle : MonoBehaviour
{
    public FireParticle fireParticle;
    public float startFire;
    public float repeatFire;
    void Start()
    {
        float dur = this.fireParticle.GetComponent<ParticleSystem>().duration;
        //Debug.Log("dur: " + dur);
        InvokeRepeating("OnFire", startFire, repeatFire); //�Ҳ� �ѱ�
        InvokeRepeating("OffFire", startFire + dur, repeatFire); //�Ҳ� ����
        
    }
    public void Init(float startFire, float repeatFire)
    {
        this.startFire = startFire;
        this.repeatFire = repeatFire;
    }
    void OnFire()
    {
        //Debug.Log("repeat: " + repeatFire + "���� ����");
        this.fireParticle.gameObject.SetActive(true);
    }

    void OffFire()
    {
        this.fireParticle.gameObject.SetActive(false);
    }
}
