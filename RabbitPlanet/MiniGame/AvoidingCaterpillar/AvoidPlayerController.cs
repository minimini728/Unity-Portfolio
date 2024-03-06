using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidPlayerController : MonoBehaviour
{
    private float dirx;
    private float speed = 4f;
    private Animator anim;
    public AvoidChangeFace face;

    public VariableJoystick joy;

    void Start()
    {
        this.anim = this.GetComponent<Animator>();

        //표정 바꾸는 이벤트 등록
        EventDispatcher.instance.AddEventHandler<int>((int)EventEnum.eEventType.AvoidChangeFace, new EventHandler<int>((type, num) =>
        {
            this.face.ChangeFace(num);
        }));
    }

    void Update()
    {
        //키보드 입력이 있을 때만 Move 코루틴 호출
        dirx = Input.GetAxisRaw("Horizontal") + joy.Horizontal;
        if (dirx != 0)
        {
            StartCoroutine("Move");
        }
        else
        {   
            //플레이어가 앞을 보게
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    IEnumerator Move()
    {
        yield return null;

        Vector3 dir = new Vector3(dirx, 0, 0);
        //이동
        this.transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(dirx > 0)
        {
            this.anim.SetInteger("Move", 2);
        }
        else if (dirx < 0)
        {
            this.anim.SetInteger("Move", 1);
        }
        else
        {
            this.anim.SetInteger("Move", 0);
        }

        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        //회전
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
