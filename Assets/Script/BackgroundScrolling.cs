using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float speed;
    public Transform[] backgrounds;

    float leftPosX = 0f;
    float backgroundWidth;  // ����� ���� �ʺ� ����ϱ� ���� ����
    float xScreenHalfSize;

    void Start()
    {
        // ī�޶��� ȭ�� ũ�⸦ �������� ���� ��� ���
        float yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

        // ���� ���� ȭ���� ���� ������ ����
        leftPosX = -(xScreenHalfSize * 2);

        // ��� �ϳ��� ���� ũ�� ��� (��� ��������Ʈ�� ������ �ʺ��� ���)
        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // ����� �������� speed��ŭ �̵�
            backgrounds[i].position += new Vector3(-speed, 0, 0) * Time.deltaTime;

            // ����� ������ ���� ȭ�� ���� ������ �� �������� �̵����� ��
            if (backgrounds[i].position.x < -(xScreenHalfSize + backgroundWidth / 2))
            {
                // ����� ������ ������ �ٽ� �̵� (����� ���� ũ�⸸ŭ �̵�)
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x + backgroundWidth * backgrounds.Length, nextPos.y, nextPos.z);
                backgrounds[i].position = nextPos;
            }
        }
    }
}
