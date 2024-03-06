using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidChangeFace : MonoBehaviour
{
    public Material[] materials; //표정
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    public void ChangeFace(int num) //표정 바꾸는 메서드
    {
        if (this.rend != null)
        {
            rend.material = materials[num];
        }
    }
}
