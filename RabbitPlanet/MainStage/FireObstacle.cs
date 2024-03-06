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
        InvokeRepeating("OnFire", startFire, repeatFire); //ºÒ²É ÄÑ±â
        InvokeRepeating("OffFire", startFire + dur, repeatFire); //ºÒ²É ²ô±â
        
    }
    public void Init(float startFire, float repeatFire)
    {
        this.startFire = startFire;
        this.repeatFire = repeatFire;
    }
    void OnFire()
    {
        //Debug.Log("repeat: " + repeatFire + "¸¶´Ù ½ÇÇà");
        this.fireParticle.gameObject.SetActive(true);
    }

    void OffFire()
    {
        this.fireParticle.gameObject.SetActive(false);
    }
}
