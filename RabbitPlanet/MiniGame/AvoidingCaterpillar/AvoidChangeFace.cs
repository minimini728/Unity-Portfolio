using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidChangeFace : MonoBehaviour
{
    public Material[] materials; //ǥ��
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    public void ChangeFace(int num) //ǥ�� �ٲٴ� �޼���
    {
        if (this.rend != null)
        {
            rend.material = materials[num];
        }
    }
}
