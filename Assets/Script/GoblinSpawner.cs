using UnityEngine;
using System.Collections;

public class GoblinSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;  // ��� ������
    public Transform[] spawnPoints;  // 1, 2, 3���� ���� ��ġ
    public float minSpawnTime = 2f;  // �ּ� ���� �ð� ����
    public float maxSpawnTime = 5f;  // �ִ� ���� �ð� ����

    private bool[] hasGoblin;  // �� ���� ����� �ִ��� ���θ� ����

    // ���������� ���۵Ǹ� �������� ����
    void Start()
    {
        hasGoblin = new bool[spawnPoints.Length];  // �� ���� ��� ���� �ʱ�ȭ
        StartCoroutine(SpawnGoblins());
    }

    // ����� ���� �ð� �������� �����ϰ� ����
    IEnumerator SpawnGoblins()
    {
        while (true)  // ���������� ���� ������ �ݺ�
        {
            // ������ �ð� ���
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            // ����� ���� ���� ã��
            int randomIndex = GetRandomEmptyFloor();

            // ����� ���� ���� ���� ��쿡�� ����� ����
            if (randomIndex != -1)
            {
                Transform spawnLocation = spawnPoints[randomIndex];

                // ��� ���� (y������ 180�� ȸ��)
                GameObject goblin = Instantiate(goblinPrefab, spawnLocation.position, Quaternion.Euler(0, 180, 0));

                // ����� ������ �� ���� ������Ʈ
                hasGoblin[randomIndex] = true;

                // ����� �ı��� �� ȣ��� �̺�Ʈ ����
                goblin.GetComponent<GoblinController>().OnGoblinDestroyed += () => hasGoblin[randomIndex] = false;
            }
        }
    }

    // ����� ���� ������ ���� �����ϴ� �Լ�
    int GetRandomEmptyFloor()
    {
        // ����� ���� ���� ã��
        int[] emptyFloors = new int[spawnPoints.Length];
        int emptyCount = 0;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!hasGoblin[i])
            {
                emptyFloors[emptyCount] = i;
                emptyCount++;
            }
        }

        // ����� ���� ���� ���ٸ� -1 ��ȯ
        if (emptyCount == 0) return -1;

        // ����� ���� �� �߿��� �����ϰ� ����
        return emptyFloors[Random.Range(0, emptyCount)];
    }
}
