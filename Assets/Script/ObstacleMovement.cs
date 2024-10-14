using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f;              // 장애물이 왼쪽으로 이동하는 속도
    public Transform[] spawnPoints;       // 장애물이 다시 나타날 위치(1층, 2층, 3층)
    private float leftBoundary;           // 화면 왼쪽 경계
    private float rightBoundary;          // 화면 오른쪽 경계

    void Start()
    {
        // 화면 경계 값 설정 (카메라 기반)
        float screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        leftBoundary = -screenHalfWidth - 1f;   // 화면 왼쪽 경계 (화면 밖으로 조금 더 나가도록 설정)
        rightBoundary = screenHalfWidth + 1f;   // 화면 오른쪽 경계 (화면 밖으로 조금 더 나가도록 설정)
    }

    void Update()
    {
        // 장애물을 왼쪽으로 이동
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // 왼쪽 경계를 벗어나면 오른쪽에서 다시 랜덤한 위치로 재배치
        if (transform.position.x < leftBoundary)
        {
            RespawnAtRight();
        }
    }

    // 오른쪽에서 장애물을 랜덤한 스폰 포인트에서 다시 나타나게 하는 함수
    void RespawnAtRight()
    {
        // 랜덤한 스폰 위치 선택 (1층, 2층, 3층)
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint = spawnPoints[randomIndex];

        // 장애물을 오른쪽 경계에서 다시 나타나게 설정
        Vector3 newPosition = randomSpawnPoint.position;
        newPosition.x = rightBoundary;   // x 좌표는 오른쪽 경계로 설정
        newPosition.z = 0f;  // Z축 값을 0 또는 원하는 값으로 고정
        transform.position = newPosition;  // 새로운 위치로 장애물 재배치
    }
}
