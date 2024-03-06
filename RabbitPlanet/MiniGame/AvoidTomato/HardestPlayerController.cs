using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardestPlayerController : MonoBehaviour
{
    private float dirx;
    private float dirz;
    private float speed = 2f;
    private bool isDie;
    private Animator anim;

    public VariableJoystick joy;

    void Start()
    {
        this.isDie = false;
        this.anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        dirx = Input.GetAxisRaw("Horizontal") + joy.Horizontal;
        dirz = Input.GetAxisRaw("Vertical") + joy.Vertical;

        if (!isDie)
        {
            StartCoroutine("Move");
        }

    }

    IEnumerator Move()
    {
        yield return null;

        Vector3 dir = new Vector3(dirx, 0, dirz);

        //이동
        this.transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (dirx > 0)
        {
            this.anim.SetInteger("Move", 2);
        }
        else if (dirx < 0)
        {
            this.anim.SetInteger("Move", 3);
        }
        else if(dirz != 0)
        {
            this.anim.SetInteger("Move", 1);
        }
        else
        {
            this.anim.SetInteger("Move", 0);
        }

        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        // 회전
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    public void Stopped()
    {
        this.isDie = true;
    }
    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Heart"))
        {   
            //게임 클리어 이벤트 전송 -> HardestMain 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.HardestGoal);
        }

    }

}
