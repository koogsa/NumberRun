using UnityEngine;
using System.Collections;

public class GoblinSpawner : MonoBehaviour
{
    // �� ���� ��ġ�� ��� ������
    public GameObject goblinPrefabFloor1;
    public GameObject goblinPrefabFloor2;
    public GameObject goblinPrefabFloor3;

    // 1��, 2��, 3���� ���� ��ġ
    public Transform[] spawnPoints;  // 0: 1��, 1: 2��, 2: 3��

    public float spawnTime = 5f;  // ��� ���� �ð� ����

    private bool[] hasGoblin = new bool[3];  // �� ���� ����� �ִ��� ���� ����

    void Start()
    {
        StartCoroutine(SpawnGoblin());
    }

    IEnumerator SpawnGoblin()
    {
        while (true)
        {
            // ����� ���� ���� ã��
            int randomFloor = GetRandomEmptyFloor();

            if (randomFloor != -1)  // ����� ���� ���� ���� ���� ����
            {
                Transform spawnLocation = spawnPoints[randomFloor];

                // �ش� ���� �´� ��� ������ ����
                GameObject goblinPrefab = GetGoblinPrefab(randomFloor);

                // ��� ����
                GameObject goblin = Instantiate(goblinPrefab, spawnLocation.position, Quaternion.Euler(0, 180, 0));

                // �ش� ���� ����� �����Ѵٰ� ���� ����
                hasGoblin[randomFloor] = true;

                // ����� �ı��Ǹ� �ش� �� ���¸� �����
                GoblinController goblinController = goblin.GetComponent<GoblinController>();
                goblinController.OnGoblinDestroyed += () => hasGoblin[randomFloor] = false;
            }

            // ���� ��� �ð�
            yield return new WaitForSeconds(spawnTime);
        }
    }

    // ����� ���� ���� �����ϰ� �����ϴ� �Լ�
    int GetRandomEmptyFloor()
    {
        // ����� ���� ���� ã��
        int[] emptyFloors = new int[3];
        int emptyCount = 0;

        for (int i = 0; i < hasGoblin.Length; i++)
        {
            if (!hasGoblin[i])
            {
                emptyFloors[emptyCount] = i;
                emptyCount++;
            }
        }

        // ����� ���� ���� ���ٸ� -1 ��ȯ
        if (emptyCount == 0) return -1;

        // ����� ���� �� �� �ϳ��� �����ϰ� ����
        return emptyFloors[Random.Range(0, emptyCount)];
    }

    // ���� �´� ��� ������ ��ȯ
    GameObject GetGoblinPrefab(int floorIndex)
    {
        switch (floorIndex)
        {
            case 0: return goblinPrefabFloor1;  // 1��
            case 1: return goblinPrefabFloor2;  // 2��
            case 2: return goblinPrefabFloor3;  // 3��
            default: return null;
        }
    }
}
