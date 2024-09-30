using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float speed;
    public Transform[] backgrounds;

    float leftPosX = 0f;
    float backgroundWidth;  // 배경의 실제 너비를 계산하기 위한 변수
    float xScreenHalfSize;

    void Start()
    {
        // 카메라의 화면 크기를 기준으로 왼쪽 경계 계산
        float yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

        // 왼쪽 경계는 화면의 왼쪽 끝으로 설정
        leftPosX = -(xScreenHalfSize * 2);

        // 배경 하나의 가로 크기 계산 (배경 스프라이트가 일정한 너비일 경우)
        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // 배경을 왼쪽으로 speed만큼 이동
            backgrounds[i].position += new Vector3(-speed, 0, 0) * Time.deltaTime;

            // 배경의 오른쪽 끝이 화면 왼쪽 끝보다 더 왼쪽으로 이동했을 때
            if (backgrounds[i].position.x < -(xScreenHalfSize + backgroundWidth / 2))
            {
                // 배경을 오른쪽 끝으로 다시 이동 (배경의 가로 크기만큼 이동)
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x + backgroundWidth * backgrounds.Length, nextPos.y, nextPos.z);
                backgrounds[i].position = nextPos;
            }
        }
    }
}
