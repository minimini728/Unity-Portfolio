using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBlock : MonoBehaviour
{
    private bool isMovingUp = true; //���� �̵� ���� ����
    private int moveCount = 0; //�̵� Ƚ��
    private float speed = 3f; //�̵� �ӵ�
    public float waitSecond;

    public void Init(float a)
    {
        this.waitSecond = a;
    }

    void Update()
    {   
        if(this.waitSecond != 0)
        {
            StartCoroutine(Fall(this.waitSecond));
        }

        if(this.transform.position.y <= -20f)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Fall(float a)
    {
        yield return new WaitForSeconds(a);
        //�̵� Ƚ���� 4������ ���� ���� �̵�
        if (moveCount < 4)
        {
            //���� �̵� ���� ���
            if (isMovingUp)
            {
                //y ��ġ�� ���� �̵�
                transform.Translate(Vector3.up * Time.deltaTime * this.speed);

                //������Ʈ�� Y ��ġ�� 0.5 �̻��� ��� �Ʒ��� �̵� �������� �����ϰ� �̵� Ƚ�� ����
                if (transform.position.y >= 0.5f)
                {
                    isMovingUp = false;
                    moveCount++;
                }
            }
            //�Ʒ��� �̵� ���� ���
            else
            {
                //y ��ġ�� �Ʒ��� �̵�
                transform.Translate(Vector3.down * Time.deltaTime * this.speed);

                //������Ʈ�� Y ��ġ�� -0.5 ������ ��� ���� �̵� �������� �����ϰ� �̵� Ƚ�� ����
                if (transform.position.y <= -0.5f)
                {
                    isMovingUp = true;
                    moveCount++;
                }
            }
        }

        //moveCount�� 4ȸ �̻��� ��� ������Ʈ�� ����Ʈ��
        if (moveCount >= 4)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 5f);
        }
    }

}
