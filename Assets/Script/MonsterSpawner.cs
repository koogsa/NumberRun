using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;   // 스폰할 고블린 프리팹
    public GameObject mushroomPrefab; // 스폰할 버섯 몬스터 프리팹

    public Transform spawnPoint1F;    // 1층의 스폰 위치
    public Transform spawnPoint2F;    // 2층의 스폰 위치
    public Transform spawnPoint3F;    // 3층의 스폰 위치

    // 고블린 스폰 시간 설정
    public float goblinMinSpawnInterval = 5f;  // 고블린 최소 스폰 시간
    public float goblinMaxSpawnInterval = 10f; // 고블린 최대 스폰 시간

    // 버섯 몬스터 스폰 시간 설정
    public float mushroomMinSpawnInterval = 7f;  // 버섯 몬스터 최소 스폰 시간
    public float mushroomMaxSpawnInterval = 12f; // 버섯 몬스터 최대 스폰 시간

    private bool hasGoblin = false;   // 1층에 고블린이 있는지 여부
    private bool hasMushroom = false; // 2층에 버섯 몬스터가 있는지 여부

    void Start()
    {
        // 첫 번째 고블린 스폰
        float firstGoblinSpawnTime = Random.Range(goblinMinSpawnInterval, goblinMaxSpawnInterval);
        Invoke("SpawnGoblin", firstGoblinSpawnTime);

        // 첫 번째 버섯 몬스터 스폰
        float firstMushroomSpawnTime = Random.Range(mushroomMinSpawnInterval, mushroomMaxSpawnInterval);
        Invoke("SpawnMushroom", firstMushroomSpawnTime);
    }

    void SpawnGoblin()
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

        // 다음 고블린 스폰 시간 설정
        float nextGoblinSpawnTime = Random.Range(goblinMinSpawnInterval, goblinMaxSpawnInterval);
        Invoke("SpawnGoblin", nextGoblinSpawnTime);  // 랜덤한 시간 후 다시 스폰
    }

    void SpawnMushroom()
    {
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

        // 다음 버섯 몬스터 스폰 시간 설정
        float nextMushroomSpawnTime = Random.Range(mushroomMinSpawnInterval, mushroomMaxSpawnInterval);
        Invoke("SpawnMushroom", nextMushroomSpawnTime);  // 랜덤한 시간 후 다시 스폰
    }
}
