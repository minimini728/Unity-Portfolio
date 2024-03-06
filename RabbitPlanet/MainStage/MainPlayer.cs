using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    public int maxHeartCount; //�ִ� ��Ʈ ��
    public int currentHeartCount; //���� ��Ʈ ��

    private float speed = 3f; //�÷��̾� �̵� �ӵ�
    private Animator anim; //�÷��̾� �ִϸ�����

    private float currentSpeed; //�÷��̾� �߰� �̵� �ӵ�
    private bool isJumping; //���� ����

    //���� ���� �� �÷��̾� ���߱�
    public bool isStopped;

    //���̽�ƽ
    public VariableJoystick joy;

    private void Awake()
    {
        this.maxHeartCount = GameObject.FindGameObjectsWithTag("Heart").Length; //�ִ� ��Ʈ ���� �ʱ�ȭ
        this.currentHeartCount = 0;
    }

    private void Start()
    {
        AudioListener.volume = 5;

        this.anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        StartCoroutine(Move());
        this.StopMove();    //���� �����Ǹ� ����
    }

    IEnumerator Move()
    {
        float h = Input.GetAxisRaw("Horizontal") + joy.Horizontal;
        float v = Input.GetAxisRaw("Vertical") + joy.Vertical;

        Vector3 dir = Vector3.Normalize(new Vector3(h, 0, v));

        this.transform.Translate(dir * (this.speed + currentSpeed) * Time.deltaTime, Space.World);

        var angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        if (dir != Vector3.zero)
        {
            this.anim.SetInteger("State", 1);
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
        else
        {
            this.anim.SetInteger("State", 0);
        }

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Heart")) //��Ʈ �Ծ��� ��
        {
            this.currentHeartCount++;
            SoundManager.PlaySFX("Pop");
            Destroy(other.gameObject);
        }
    }

    //���� �÷��� �޼���
    public void ApplyAcceleration(float acceleration, float maxSpeed)
    {
        currentSpeed += acceleration;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }

    //���� �÷��� �޼���
    public void Jump(float jumpForce)
    {
        if (!isJumping)
        {
            // �÷��̾ y �������� ����
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Board")) //�Ϲ� ��
        {
            this.currentSpeed = 0;
            isJumping = false;
        }
        else if (collision.gameObject.CompareTag("Out"))    //�� ������ �������� �̴ϰ��� ����
        {
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.StartMiniGame);
        }
    }

    public void StopMove()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (this.isStopped)
        {
            this.speed = 0;
            this.currentSpeed = 0;
            rb.velocity = Vector3.zero;
        }
    }
}
