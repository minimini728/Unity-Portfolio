using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public float acceleration = 10f; //발판에서 가속도
    public float maxSpeed = 15f;    //발판의 최대 속도
    public float jumpForce = 0.5f;   //플레이어를 날려보내는 힘

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("플레이어 감지");
            MainPlayer player = other.gameObject.GetComponent<MainPlayer>();
            if (player != null)
            {
                player.ApplyAcceleration(acceleration, maxSpeed);
                player.Jump(jumpForce);
            }
        }
    }

}
