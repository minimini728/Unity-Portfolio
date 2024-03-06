using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    public int maxHeartCount; //최대 하트 수
    public int currentHeartCount; //현재 하트 수

    private float speed = 3f; //플레이어 이동 속도
    private Animator anim; //플레이어 애니메이터

    private float currentSpeed; //플레이어 추가 이동 속도
    private bool isJumping; //점프 유무

    //게임 오버 시 플레이어 멈추기
    public bool isStopped;

    //조이스틱
    public VariableJoystick joy;

    private void Awake()
    {
        this.maxHeartCount = GameObject.FindGameObjectsWithTag("Heart").Length; //최대 하트 개수 초기화
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
        this.StopMove();    //게임 오버되면 멈춤
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
        if (other.gameObject.CompareTag("Heart")) //하트 먹었을 때
        {
            this.currentHeartCount++;
            SoundManager.PlaySFX("Pop");
            Destroy(other.gameObject);
        }
    }

    //가속 플랫폼 메서드
    public void ApplyAcceleration(float acceleration, float maxSpeed)
    {
        currentSpeed += acceleration;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }

    //점프 플랫폼 메서드
    public void Jump(float jumpForce)
    {
        if (!isJumping)
        {
            // 플레이어를 y 방향으로 점프
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Board")) //일반 땅
        {
            this.currentSpeed = 0;
            isJumping = false;
        }
        else if (collision.gameObject.CompareTag("Out"))    //땅 밖으로 떨어지면 미니게임 시작
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
