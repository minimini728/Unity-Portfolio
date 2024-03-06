using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public Transform aimLine; //���Ӽ� �̹����� Transform ������Ʈ
    public LineRenderer lineRenderer; //LineRenderer ������Ʈ

    public GameObject[] itemPrefabs;

    public AudioSource audio;

    void Start()
    {
        //LineRenderer �ʱ� ����
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.005f;
        lineRenderer.endWidth = 0.005f;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                //�������� ����ĳ��Ʈ�� ��� �Լ��� ȣ���ϰ�, ���̿� ���Ӽ��� �׸�
                ShootRaycast();

                audio.Play();
            }

        }
    }

    void ShootRaycast()
    {
        //���� ī�޶��� ���� �������� ���� ���
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;

        //����ĳ��Ʈ�� �����ϰ�, ���̰� ��ü�� �ε����� ó��
        if (Physics.Raycast(ray, out hit))
        {
            //�ε��� ��ü�� ������ ����� �� ����
            Debug.Log("Hit object: " + hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Mob")) //���� ����� ��
            {
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.HitMob);

                //�÷��̾� ��ų�� ���
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.IncreaseSkillValue);


                if (hit.collider.transform.parent.parent != null) //��� ���� ����� ��
                {
                    //�θ��� �θ� ������Ʈ�� ��Ȱ��ȭ
                    hit.collider.transform.parent.parent.gameObject.SetActive(false);

                    int randomValue = Random.Range(0, 100); //0 �̻� 100 �̸��� ���� �� ����

                    //50%�� Ȯ���� ������ ����
                    if (randomValue < 50)
                    {
                        int randomIndex = Random.Range(0, itemPrefabs.Length);
                        GameObject selectedPrefab = itemPrefabs[randomIndex];

                        Instantiate(selectedPrefab, hit.collider.transform.position, Quaternion.identity);
                    }
                }
                else //ũ�� ���� ����� ��
                {
                    hit.collider.gameObject.SetActive(false);

                    int randomValue = Random.Range(0, 100); //0 �̻� 100 �̸��� ���� �� ����

                    //50%�� Ȯ���� ������ ����
                    if (randomValue < 50)
                    {
                        int randomIndex = Random.Range(0, itemPrefabs.Length);
                        GameObject selectedPrefab = itemPrefabs[randomIndex];

                        Instantiate(selectedPrefab, hit.collider.transform.position, Quaternion.identity);
                    }
                }
            }
            else if (hit.collider.CompareTag("Boss")) //���� ���Ϳ� ����� ��
            {               
                //�÷��̾� ��ų�� ���
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.IncreaseSkillValue);

                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.HitBoss);
            }
            else if (hit.collider.CompareTag("Box")) //������ ���ڿ� ����� ��
            {
                StartCoroutine(DropItem(hit.collider.gameObject));
            }

        }

    }

    IEnumerator DropItem(GameObject obj)
    {
        obj.GetComponent<BoxDropItem>().PlayEffect();
        yield return null;
    }

}
