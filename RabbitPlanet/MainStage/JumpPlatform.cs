using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public float acceleration = 10f; //���ǿ��� ���ӵ�
    public float maxSpeed = 15f;    //������ �ִ� �ӵ�
    public float jumpForce = 0.5f;   //�÷��̾ ���������� ��

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("�÷��̾� ����");
            MainPlayer player = other.gameObject.GetComponent<MainPlayer>();
            if (player != null)
            {
                player.ApplyAcceleration(acceleration, maxSpeed);
                player.Jump(jumpForce);
            }
        }
    }

}
