using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticle : MonoBehaviour
{
    private bool isRun = false;
    private void OnParticleCollision(GameObject other)
    {
        if (this.isRun != true)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                this.isRun = true;
                Debug.Log("불에 충돌");
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.StartMiniGame);
            }

        }
    }
}
