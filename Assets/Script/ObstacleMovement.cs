using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f;              // ��ֹ��� �������� �̵��ϴ� �ӵ�
    public Transform[] spawnPoints;       // ��ֹ��� �ٽ� ��Ÿ�� ��ġ(1��, 2��, 3��)
    private float leftBoundary;           // ȭ�� ���� ���
    private float rightBoundary;          // ȭ�� ������ ���

    void Start()
    {
        // ȭ�� ��� �� ���� (ī�޶� ���)
        float screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        leftBoundary = -screenHalfWidth - 1f;   // ȭ�� ���� ��� (ȭ�� ������ ���� �� �������� ����)
        rightBoundary = screenHalfWidth + 1f;   // ȭ�� ������ ��� (ȭ�� ������ ���� �� �������� ����)
    }

    void Update()
    {
        // ��ֹ��� �������� �̵�
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // ���� ��踦 ����� �����ʿ��� �ٽ� ������ ��ġ�� ���ġ
        if (transform.position.x < leftBoundary)
        {
            RespawnAtRight();
        }
    }

    // �����ʿ��� ��ֹ��� ������ ���� ����Ʈ���� �ٽ� ��Ÿ���� �ϴ� �Լ�
    void RespawnAtRight()
    {
        // ������ ���� ��ġ ���� (1��, 2��, 3��)
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint = spawnPoints[randomIndex];

        // ��ֹ��� ������ ��迡�� �ٽ� ��Ÿ���� ����
        Vector3 newPosition = randomSpawnPoint.position;
        newPosition.x = rightBoundary;   // x ��ǥ�� ������ ���� ����
        newPosition.z = 0f;  // Z�� ���� 0 �Ǵ� ���ϴ� ������ ����
        transform.position = newPosition;  // ���ο� ��ġ�� ��ֹ� ���ġ
    }
}
