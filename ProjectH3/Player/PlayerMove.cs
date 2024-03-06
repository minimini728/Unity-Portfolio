using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public Transform InvisibleCameraOrigin; //�÷��̾� �þ� ��ġ

    public Animator anim; //�÷��̾� �ִϸ�����
    private float speed = 3f; //�⺻ �ӵ�
    private float runSpeed = 8f; // �޸��� �ӵ�

    public float TurnSpeed = 3; //ȸ�� �ӵ�
    public float VerticalRotMin = -80; //���� ȸ�� ��
    public float VerticalRotMax = 80; //���� ȸ�� ��

    public float removalRadius = 20f; //�ñر� �Ÿ�
    public GameObject[] itemPrefabs; //��� ������ ���

    public GameObject gun; //�� ������Ʈ
    public Transform gunTransform; //���� Transform
    public Transform handTransform; //���� Transform

    private Vector3 originalGunPosition; //�� �ʱ� ��ġ ��
    private Quaternion originalGunRotation; //�� �ʱ� ȸ�� ��

    public bool isDraw = false; //���� ���� üũ

    public GameObject skill; //��ų ����Ʈ ������Ʈ

    public Rigidbody rigidbody; //�÷��̾� ������ٵ�

    private Scene currentScene; //���� �� (���ʱ������� �� �ٰ� �ϱ� ����)

    void Start()
    {   
        //���� �÷��� ���� ���� ���� ��������
        Scene currentScene = SceneManager.GetActiveScene();
        this.currentScene = currentScene;

        //���� �ʱ� ��ġ�� ȸ������ ����
        originalGunPosition = gunTransform.localPosition;
        originalGunRotation = gunTransform.localRotation;

        //�ʱ� ��ų ����Ʈ ����
        this.skill.GetComponent<ParticleSystem>().Stop();

    }
    void Update()
    {
        StartCoroutine(Move());

        if (Input.GetKeyDown(KeyCode.Mouse1)) //��Ŭ�� �� �� ����
        {
            this.anim.SetTrigger("Draw");
            this.anim.SetBool("isDraw", true);
            Invoke("ToggleDrawState", 0.5f);

        }
        else if (Input.GetKeyUp(KeyCode.Mouse1)) //��Ŭ�� ������ �� ����ġ
        {
            Invoke("ToggleDrawState", 0.5f);
            this.anim.SetBool("isDraw", false);
            Invoke("OriginalPlaceGun", 0.5f); //�� ����ġ
        }

        if (isDraw)
        {
            this.MoveGunToHand(); //�տ� �� ���

            if (Input.GetKeyDown(KeyCode.Mouse0)) //��Ŭ������ �� �߻�
            {
                gun.gameObject.GetComponent<HMGun>().Shoot();
                this.anim.SetTrigger("Fire");
            }

        }

        if (Input.GetKeyDown(KeyCode.Q)) //QŰ�� �ñر�
        {
            if (InfoManager.instance.PlayerInfo.skill >= 100) //��ų ������ 100 �̻��� �� ��ų ��� ����
            {
                this.anim.SetTrigger("Skill");
                this.skill.GetComponent<ParticleSystem>().Play();

                this.PerformUltimateAttack();

                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ResetPlayerSkill); //��ų ������ ���� �̺�Ʈ ȣ��
            }
        }

    }

    IEnumerator Move()
    {
        yield return null;
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        // �̵�
        Vector3 dir = new Vector3(h, 0, v);

        float currentSpeed;
        //���ʱ������� �� �ٰ�
        if (this.currentScene.name == "OutPost" || this.currentScene.name == "OutPostTuto")
        {
            currentSpeed = speed;
        }
        else
        {
            // �̵� �ӵ� Shift ����
            currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;
        }

        //ȸ��
        var rotInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        var rot = rigidbody.rotation.eulerAngles;
        rot.y += rotInput.x * TurnSpeed;
        Quaternion newRotation = Quaternion.Euler(rot);
        rigidbody.MoveRotation(newRotation);

        //�̵� ������ ȸ�� �������� ��ȯ
        Vector3 rotatedDir = newRotation * dir;

        //���� ���� �� ���� �Է��� ���ٸ� (Ű������ �ƹ� �Է��� ���ٸ�)
        if (Mathf.Approximately(h, 0) && Mathf.Approximately(v, 0))
        {
            //Idle����
        }
        else
        {
            //�̵�
            Vector3 newPosition = rigidbody.position + rotatedDir.normalized * currentSpeed * Time.deltaTime;
            rigidbody.MovePosition(newPosition);

            //�ִϸ��̼� ����
            if (Input.GetKey(KeyCode.LeftShift) && this.currentScene.name != "OutPost" && this.currentScene.name != "OutPostTuto")
            {   
                //�� ��
                this.anim.SetBool("isRun", true);
                this.anim.SetFloat("RVertical", v);
                this.anim.SetFloat("RHorizontal", h);
            }
            else
            { 
                //���� ��
                this.anim.SetBool("isRun", false);
                this.anim.SetBool("isWalk", true);
                this.anim.SetFloat("Vertical", v);
                this.anim.SetFloat("Horizontal", h);
            }

        }

        //���콺 �Է¿� ���� �þ� ����
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

    void PerformUltimateAttack() //�ñر� ����
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, removalRadius); //�ֺ� �ݶ��̴� �迭�� �ֱ�

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Mob")) //���� �������
            {   
                //�� ����
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.HitMob);

                if (collider.transform.parent.parent != null) //��� ����
                {
                    //�θ��� �θ� ������Ʈ�� ��Ȱ��ȭ
                    collider.transform.parent.parent.gameObject.SetActive(false);

                    int randomValue = Random.Range(0, 100); // 0 �̻� 100 �̸��� ���� �� ����

                    //50%�� Ȯ���� ������ ����
                    if (randomValue < 50)
                    {
                        int randomIndex = Random.Range(0, itemPrefabs.Length);
                        GameObject selectedPrefab = itemPrefabs[randomIndex];

                        Instantiate(selectedPrefab, collider.transform.position, Quaternion.identity);
                    }
                }
                else //ũ�� ����
                {
                    collider.gameObject.SetActive(false);

                    int randomValue = Random.Range(0, 100); //0 �̻� 100 �̸��� ���� �� ����

                    //50%�� Ȯ���� ������ ����
                    if (randomValue < 50)
                    {
                        int randomIndex = Random.Range(0, itemPrefabs.Length);
                        GameObject selectedPrefab = itemPrefabs[randomIndex];

                        Instantiate(selectedPrefab, collider.transform.position, Quaternion.identity);
                    }

                }
            }
            else if (collider.CompareTag("Boss")) //���� �������
            {   
                //���� ����
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.DoubleHitBoss);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AdvEC")) //��� ���� ��ǰ
        {
            //��� ���� ��ǰ ȹ�� �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.GetItem, 103);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("BioSample")) //���� ��� ����
        {   
            //���� ��� ���� ȹ�� �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.GetItem, 102);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("MetalAlloy")) //�ݼ� �ձ�
        {
            //�ݼ� �ձ� ȹ�� �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.GetItem, 101);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("EnergyCrystal")) //������ ����
        {   
            //������ ���� ȹ�� �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.GetItem, 100);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Mob")) //��
        {   
            //�÷��̾� HP ���� �̺�Ʈ ȣ��
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.DeclinePlayerHp);
        }
        else if (collision.gameObject.CompareTag("Boss")) //����
        {   
            //�÷��̾� HP ���� �̺�Ʈ 2�� ȣ��
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.DeclinePlayerHp);
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.DeclinePlayerHp);
        }

    }
    public void MoveGunToHand()
    {
        //���� ��ġ�� �տ� ��ġ��Ŵ
        if (gunTransform != null && handTransform != null)
        {
            //���� ���� ���� ���� ��ġ�� �տ� ��ġ��Ŵ
            gunTransform.position = handTransform.position;

            //���� ������ ���� forward �������� ����
            Vector3 handForward = handTransform.forward;
            handForward.y = 0f; //y �� ȸ���� ���� (���� ���⸸ ���)
            gunTransform.rotation = Quaternion.LookRotation(handForward, Vector3.up);
        }
    }

    private void ToggleDrawState()
    {
        isDraw = !isDraw;
    }

    private void OriginalPlaceGun()
    {
        // �� ����ġ
        gunTransform.localPosition = originalGunPosition;
        gunTransform.localRotation = originalGunRotation;

    }

    private void OnDisable()
    {
        Destroy(this);
    }
}
