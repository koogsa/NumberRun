using UnityEngine;
using System.Collections;

public class GoblinSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;  // 고블린 프리팹
    public Transform[] spawnPoints;  // 1, 2, 3층의 스폰 위치
    public float minSpawnTime = 2f;  // 최소 스폰 시간 간격
    public float maxSpawnTime = 5f;  // 최대 스폰 시간 간격

    private bool[] hasGoblin;  // 각 층에 고블린이 있는지 여부를 추적

    // 스테이지가 시작되면 스포닝을 시작
    void Start()
    {
        hasGoblin = new bool[spawnPoints.Length];  // 각 층의 고블린 상태 초기화
        StartCoroutine(SpawnGoblins());
    }

    // 고블린을 일정 시간 간격으로 랜덤하게 스폰
    IEnumerator SpawnGoblins()
    {
        while (true)  // 스테이지가 끝날 때까지 반복
        {
            // 랜덤한 시간 대기
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            // 고블린이 없는 층을 찾음
            int randomIndex = GetRandomEmptyFloor();

            // 고블린이 없는 층이 있을 경우에만 고블린을 생성
            if (randomIndex != -1)
            {
                Transform spawnLocation = spawnPoints[randomIndex];

                // 고블린 생성 (y축으로 180도 회전)
                GameObject goblin = Instantiate(goblinPrefab, spawnLocation.position, Quaternion.Euler(0, 180, 0));

                // 고블린이 생성된 층 상태 업데이트
                hasGoblin[randomIndex] = true;

                // 고블린이 파괴될 때 호출될 이벤트 설정
                goblin.GetComponent<GoblinController>().OnGoblinDestroyed += () => hasGoblin[randomIndex] = false;
            }
        }
    }

    // 고블린이 없는 랜덤한 층을 선택하는 함수
    int GetRandomEmptyFloor()
    {
        // 고블린이 없는 층을 찾음
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

        // 고블린이 없는 층이 없다면 -1 반환
        if (emptyCount == 0) return -1;

        // 고블린이 없는 층 중에서 랜덤하게 선택
        return emptyFloors[Random.Range(0, emptyCount)];
    }
}
