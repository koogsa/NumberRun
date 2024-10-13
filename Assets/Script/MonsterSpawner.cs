using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;   // 스폰할 고블린 프리팹
    public GameObject mushroomPrefab; // 스폰할 버섯 몬스터 프리팹

    public Transform spawnPoint1F;    // 1층의 스폰 위치
    public Transform spawnPoint2F;    // 2층의 스폰 위치
    public Transform spawnPoint3F;    // 3층의 스폰 위치

    public float minSpawnInterval = 5f;  // 최소 스폰 시간 간격
    public float maxSpawnInterval = 10f; // 최대 스폰 시간 간격

    private bool hasGoblin = false;   // 1층에 고블린이 있는지 여부
    private bool hasMushroom = false; // 2층에 버섯 몬스터가 있는지 여부

    void Start()
    {
        // 첫 스폰도 랜덤한 시간 후에 발생하도록 설정
        float firstSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnMonster", firstSpawnTime);
    }

    void SpawnMonster()
    {
        // 1층에 고블린이 없으면 고블린 스폰
        if (!hasGoblin)
        {
            GameObject newGoblin = Instantiate(goblinPrefab, spawnPoint1F.position, Quaternion.Euler(0, 180, 0));
            hasGoblin = true;

            GoblinController goblinController = newGoblin.GetComponent<GoblinController>();
            if (goblinController != null)
            {
                goblinController.OnGoblinDestroyed += () => hasGoblin = false; // 고블린 파괴 시 스폰 가능하도록 상태 변경
            }
        }

        // 2층에 버섯 몬스터가 없으면 버섯 몬스터 스폰
        if (!hasMushroom)
        {
            GameObject newMushroom = Instantiate(mushroomPrefab, spawnPoint2F.position, Quaternion.Euler(0, 180, 0));
            hasMushroom = true;

            MushroomController mushroomController = newMushroom.GetComponent<MushroomController>();
            if (mushroomController != null)
            {
                mushroomController.OnMushroomDestroyed += () => hasMushroom = false; // 버섯 몬스터 파괴 시 스폰 가능하도록 상태 변경
            }
        }

        // 다음 스폰 시간 설정 (5~10초 사이의 랜덤한 값)
        float nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnMonster", nextSpawnTime);  // 랜덤한 시간 후 다시 스폰
    }
}
