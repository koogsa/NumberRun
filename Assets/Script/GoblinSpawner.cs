using UnityEngine;
using System.Collections;

public class GoblinSpawner : MonoBehaviour
{
    // 각 층에 배치될 고블린 프리팹
    public GameObject goblinPrefabFloor1;
    public GameObject goblinPrefabFloor2;
    public GameObject goblinPrefabFloor3;

    // 1층, 2층, 3층의 스폰 위치
    public Transform[] spawnPoints;  // 0: 1층, 1: 2층, 2: 3층

    public float spawnTime = 5f;  // 고블린 스폰 시간 간격

    private bool[] hasGoblin = new bool[3];  // 각 층에 고블린이 있는지 여부 추적

    void Start()
    {
        StartCoroutine(SpawnGoblin());
    }

    IEnumerator SpawnGoblin()
    {
        while (true)
        {
            // 고블린이 없는 층을 찾음
            int randomFloor = GetRandomEmptyFloor();

            if (randomFloor != -1)  // 고블린이 없는 층이 있을 때만 스폰
            {
                Transform spawnLocation = spawnPoints[randomFloor];

                // 해당 층에 맞는 고블린 프리팹 선택
                GameObject goblinPrefab = GetGoblinPrefab(randomFloor);

                // 고블린 생성
                GameObject goblin = Instantiate(goblinPrefab, spawnLocation.position, Quaternion.Euler(0, 180, 0));

                // 해당 층에 고블린이 존재한다고 상태 변경
                hasGoblin[randomFloor] = true;

                // 고블린이 파괴되면 해당 층 상태를 비워둠
                GoblinController goblinController = goblin.GetComponent<GoblinController>();
                goblinController.OnGoblinDestroyed += () => hasGoblin[randomFloor] = false;
            }

            // 스폰 대기 시간
            yield return new WaitForSeconds(spawnTime);
        }
    }

    // 고블린이 없는 층을 랜덤하게 선택하는 함수
    int GetRandomEmptyFloor()
    {
        // 고블린이 없는 층을 찾음
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

        // 고블린이 없는 층이 없다면 -1 반환
        if (emptyCount == 0) return -1;

        // 고블린이 없는 층 중 하나를 랜덤하게 선택
        return emptyFloors[Random.Range(0, emptyCount)];
    }

    // 층에 맞는 고블린 프리팹 반환
    GameObject GetGoblinPrefab(int floorIndex)
    {
        switch (floorIndex)
        {
            case 0: return goblinPrefabFloor1;  // 1층
            case 1: return goblinPrefabFloor2;  // 2층
            case 2: return goblinPrefabFloor3;  // 3층
            default: return null;
        }
    }
}
