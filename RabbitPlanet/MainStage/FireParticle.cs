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
                Debug.Log("�ҿ� �浹");
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.StartMiniGame);
            }

        }
    }
}
