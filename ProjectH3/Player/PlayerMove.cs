using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public Transform InvisibleCameraOrigin; //플레이어 시야 위치

    public Animator anim; //플레이어 애니메이터
    private float speed = 3f; //기본 속도
    private float runSpeed = 8f; // 달리기 속도

    public float TurnSpeed = 3; //회전 속도
    public float VerticalRotMin = -80; //상하 회전 각
    public float VerticalRotMax = 80; //상하 회전 각

    public float removalRadius = 20f; //궁극기 거리
    public GameObject[] itemPrefabs; //드랍 아이템 목록

    public GameObject gun; //총 오브젝트
    public Transform gunTransform; //총의 Transform
    public Transform handTransform; //손의 Transform

    private Vector3 originalGunPosition; //총 초기 위치 값
    private Quaternion originalGunRotation; //총 초기 회전 값

    public bool isDraw = false; //조준 상태 체크

    public GameObject skill; //스킬 이펙트 오브젝트

    public Rigidbody rigidbody; //플레이어 리지드바디

    private Scene currentScene; //현재 씬 (전초기지에서 못 뛰게 하기 위해)

    void Start()
    {   
        //현재 플레이 중인 씬의 정보 가져오기
        Scene currentScene = SceneManager.GetActiveScene();
        this.currentScene = currentScene;

        //총의 초기 위치와 회전값을 저장
        originalGunPosition = gunTransform.localPosition;
        originalGunRotation = gunTransform.localRotation;

        //초기 스킬 이펙트 정지
        this.skill.GetComponent<ParticleSystem>().Stop();

    }
    void Update()
    {
        StartCoroutine(Move());

        if (Input.GetKeyDown(KeyCode.Mouse1)) //우클릭 시 총 조준
        {
            this.anim.SetTrigger("Draw");
            this.anim.SetBool("isDraw", true);
            Invoke("ToggleDrawState", 0.5f);

        }
        else if (Input.GetKeyUp(KeyCode.Mouse1)) //우클릭 해제시 총 원위치
        {
            Invoke("ToggleDrawState", 0.5f);
            this.anim.SetBool("isDraw", false);
            Invoke("OriginalPlaceGun", 0.5f); //총 원위치
        }

        if (isDraw)
        {
            this.MoveGunToHand(); //손에 총 들기

            if (Input.GetKeyDown(KeyCode.Mouse0)) //좌클릭으로 총 발사
            {
                gun.gameObject.GetComponent<HMGun>().Shoot();
                this.anim.SetTrigger("Fire");
            }

        }

        if (Input.GetKeyDown(KeyCode.Q)) //Q키로 궁극기
        {
            if (InfoManager.instance.PlayerInfo.skill >= 100) //스킬 게이지 100 이상일 때 스킬 사용 가능
            {
                this.anim.SetTrigger("Skill");
                this.skill.GetComponent<ParticleSystem>().Play();

                this.PerformUltimateAttack();

                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ResetPlayerSkill); //스킬 게이지 리셋 이벤트 호출
            }
        }

    }

    IEnumerator Move()
    {
        yield return null;
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        // 이동
        Vector3 dir = new Vector3(h, 0, v);

        float currentSpeed;
        //전초기지에서 못 뛰게
        if (this.currentScene.name == "OutPost" || this.currentScene.name == "OutPostTuto")
        {
            currentSpeed = speed;
        }
        else
        {
            // 이동 속도 Shift 여부
            currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;
        }

        //회전
        var rotInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        var rot = rigidbody.rotation.eulerAngles;
        rot.y += rotInput.x * TurnSpeed;
        Quaternion newRotation = Quaternion.Euler(rot);
        rigidbody.MoveRotation(newRotation);

        //이동 방향을 회전 방향으로 변환
        Vector3 rotatedDir = newRotation * dir;

        //만약 수직 및 수평 입력이 없다면 (키보드의 아무 입력이 없다면)
        if (Mathf.Approximately(h, 0) && Mathf.Approximately(v, 0))
        {
            //Idle상태
        }
        else
        {
            //이동
            Vector3 newPosition = rigidbody.position + rotatedDir.normalized * currentSpeed * Time.deltaTime;
            rigidbody.MovePosition(newPosition);

            //애니메이션 설정
            if (Input.GetKey(KeyCode.LeftShift) && this.currentScene.name != "OutPost" && this.currentScene.name != "OutPostTuto")
            {   
                //뛸 때
                this.anim.SetBool("isRun", true);
                this.anim.SetFloat("RVertical", v);
                this.anim.SetFloat("RHorizontal", h);
            }
            else
            { 
                //걸을 때
                this.anim.SetBool("isRun", false);
                this.anim.SetBool("isWalk", true);
                this.anim.SetFloat("Vertical", v);
                this.anim.SetFloat("Horizontal", h);
            }

        }

        //마우스 입력에 따른 시야 상하
        if (InvisibleCameraOrigin != null)
        {
            rot = InvisibleCameraOrigin.localRotation.eulerAngles;
            rot.x -= rotInput.y * TurnSpeed;
            if (rot.x > 180)
                rot.x -= 360;
            rot.x = Mathf.Clamp(rot.x, VerticalRotMin, VerticalRotMax);
            InvisibleCameraOrigin.localRotation = Quaternion.Euler(rot);
        }

    }

    void PerformUltimateAttack() //궁극기 공격
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, removalRadius); //주변 콜라이더 배열에 넣기

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Mob")) //몹에 닿았을때
            {   
                //몹 공격
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.HitMob);

                if (collider.transform.parent.parent != null) //드론 몬스터
                {
                    //부모의 부모 오브젝트를 비활성화
                    collider.transform.parent.parent.gameObject.SetActive(false);

                    int randomValue = Random.Range(0, 100); // 0 이상 100 미만의 랜덤 값 생성

                    //50%의 확률로 아이템 생성
                    if (randomValue < 50)
                    {
                        int randomIndex = Random.Range(0, itemPrefabs.Length);
                        GameObject selectedPrefab = itemPrefabs[randomIndex];

                        Instantiate(selectedPrefab, collider.transform.position, Quaternion.identity);
                    }
                }
                else //크랩 몬스터
                {
                    collider.gameObject.SetActive(false);

                    int randomValue = Random.Range(0, 100); //0 이상 100 미만의 랜덤 값 생성

                    //50%의 확률로 아이템 생성
                    if (randomValue < 50)
                    {
                        int randomIndex = Random.Range(0, itemPrefabs.Length);
                        GameObject selectedPrefab = itemPrefabs[randomIndex];

                        Instantiate(selectedPrefab, collider.transform.position, Quaternion.identity);
                    }

                }
            }
            else if (collider.CompareTag("Boss")) //보스 닿았을때
            {   
                //보스 공격
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.DoubleHitBoss);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AdvEC")) //고급 전자 부품
        {
            //고급 전자 부품 획득 이벤트 호출
            EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.GetItem, 103);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("BioSample")) //생물 기술 샘플
        {   
            //생물 기술 샘플 획득 이벤트 호출
            EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.GetItem, 102);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("MetalAlloy")) //금속 합금
        {
            //금속 합금 획득 이벤트 호출
            EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.GetItem, 101);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("EnergyCrystal")) //에너지 결정
        {   
            //에너지 결정 획득 이벤트 호출
            EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.GetItem, 100);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Mob")) //몹
        {   
            //플레이어 HP 감소 이벤트 호출
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.DeclinePlayerHp);
        }
        else if (collision.gameObject.CompareTag("Boss")) //보스
        {   
            //플레이어 HP 감소 이벤트 2번 호출
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.DeclinePlayerHp);
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.DeclinePlayerHp);
        }

    }
    public void MoveGunToHand()
    {
        //총의 위치를 손에 일치시킴
        if (gunTransform != null && handTransform != null)
        {
            //조준 중일 때는 총의 위치를 손에 일치시킴
            gunTransform.position = handTransform.position;

            //총의 방향을 손의 forward 방향으로 설정
            Vector3 handForward = handTransform.forward;
            handForward.y = 0f; //y 축 회전을 막음 (수평 방향만 사용)
            gunTransform.rotation = Quaternion.LookRotation(handForward, Vector3.up);
        }
    }

    private void ToggleDrawState()
    {
        isDraw = !isDraw;
    }

    private void OriginalPlaceGun()
    {
        // 총 원위치
        gunTransform.localPosition = originalGunPosition;
        gunTransform.localRotation = originalGunRotation;

    }

    private void OnDisable()
    {
        Destroy(this);
    }
}
