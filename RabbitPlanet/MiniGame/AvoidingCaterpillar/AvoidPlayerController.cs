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

        //ǥ�� �ٲٴ� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler<int>((int)EventEnum.eEventType.AvoidChangeFace, new EventHandler<int>((type, num) =>
        {
            this.face.ChangeFace(num);
        }));
    }

    void Update()
    {
        //Ű���� �Է��� ���� ���� Move �ڷ�ƾ ȣ��
        dirx = Input.GetAxisRaw("Horizontal") + joy.Horizontal;
        if (dirx != 0)
        {
            StartCoroutine("Move");
        }
        else
        {   
            //�÷��̾ ���� ����
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    IEnumerator Move()
    {
        yield return null;

        Vector3 dir = new Vector3(dirx, 0, 0);
        //�̵�
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
        //ȸ��
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
