using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public Transform aimLine; //에임선 이미지의 Transform 컴포넌트
    public LineRenderer lineRenderer; //LineRenderer 컴포넌트

    public GameObject[] itemPrefabs;

    public AudioSource audio;

    void Start()
    {
        //LineRenderer 초기 설정
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
                //정면으로 레이캐스트를 쏘는 함수를 호출하고, 레이와 에임선을 그림
                ShootRaycast();

                audio.Play();
            }

        }
    }

    void ShootRaycast()
    {
        //메인 카메라의 정면 방향으로 레이 쏘기
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;

        //레이캐스트를 실행하고, 레이가 물체에 부딪히면 처리
        if (Physics.Raycast(ray, out hit))
        {
            //부딪힌 물체의 정보를 사용할 수 있음
            Debug.Log("Hit object: " + hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Mob")) //몹에 닿았을 때
            {
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.HitMob);

                //플레이어 스킬값 상승
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.IncreaseSkillValue);


                if (hit.collider.transform.parent.parent != null) //드론 몹에 닿았을 때
                {
                    //부모의 부모 오브젝트를 비활성화
                    hit.collider.transform.parent.parent.gameObject.SetActive(false);

                    int randomValue = Random.Range(0, 100); //0 이상 100 미만의 랜덤 값 생성

                    //50%의 확률로 아이템 생성
                    if (randomValue < 50)
                    {
                        int randomIndex = Random.Range(0, itemPrefabs.Length);
                        GameObject selectedPrefab = itemPrefabs[randomIndex];

                        Instantiate(selectedPrefab, hit.collider.transform.position, Quaternion.identity);
                    }
                }
                else //크랩 몹에 닿았을 때
                {
                    hit.collider.gameObject.SetActive(false);

                    int randomValue = Random.Range(0, 100); //0 이상 100 미만의 랜덤 값 생성

                    //50%의 확률로 아이템 생성
                    if (randomValue < 50)
                    {
                        int randomIndex = Random.Range(0, itemPrefabs.Length);
                        GameObject selectedPrefab = itemPrefabs[randomIndex];

                        Instantiate(selectedPrefab, hit.collider.transform.position, Quaternion.identity);
                    }
                }
            }
            else if (hit.collider.CompareTag("Boss")) //보스 몬스터에 닿았을 때
            {               
                //플레이어 스킬값 상승
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.IncreaseSkillValue);

                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.HitBoss);
            }
            else if (hit.collider.CompareTag("Box")) //아이템 상자에 닿았을 때
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
