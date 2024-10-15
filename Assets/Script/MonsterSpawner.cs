using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;   // 스폰할 고블린 프리팹
    public GameObject mushroomPrefab; // 스폰할 버섯 몬스터 프리팹

    public Transform spawnPoint1F;    // 1층의 스폰 위치
    public Transform spawnPoint2F;    // 2층의 스폰 위치

    public float minSpawnInterval = 5f;  // 최소 스폰 시간 간격
    public float maxSpawnInterval = 10f; // 최대 스폰 시간 간격

    private bool hasGoblin = false;    // 1층 고블린 존재 여부
    private bool hasMushroom = false;  // 2층 버섯 존재 여부

    void Start()
    {
        // 고블린 스폰 설정
        float firstGoblinSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnGoblin", firstGoblinSpawnTime);

        // 버섯 스폰 설정
        float firstMushroomSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnMushroom", firstMushroomSpawnTime);
    }

    void SpawnGoblin()
    {
        if (!hasGoblin)
        {
            GameObject newGoblin = Instantiate(goblinPrefab, spawnPoint1F.position, Quaternion.Euler(0, 180, 0));
            hasGoblin = true;

            GoblinController goblinController = newGoblin.GetComponent<GoblinController>();
            if (goblinController != null)
            {
                goblinController.OnGoblinDestroyed += () => hasGoblin = false;  // 고블린이 파괴될 때 hasGoblin을 false로 설정
            }
        }

        float nextGoblinSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnGoblin", nextGoblinSpawnTime);
    }

    void SpawnMushroom()
    {
        if (!hasMushroom)
        {
            GameObject newMushroom = Instantiate(mushroomPrefab, spawnPoint2F.position, Quaternion.Euler(0, 180, 0));
            hasMushroom = true;

            MushroomController mushroomController = newMushroom.GetComponent<MushroomController>();
            if (mushroomController != null)
            {
                mushroomController.OnMushroomDestroyed += () => hasMushroom = false;  // 버섯이 파괴될 때 hasMushroom을 false로 설정
            }
        }

        float nextMushroomSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnMushroom", nextMushroomSpawnTime);
    }

    // 몬스터가 파괴될 때 호출되는 메서드
    public void OnMonsterDestroyed(string monsterType)
    {
        if (monsterType == "Goblin")
        {
            hasGoblin = false;  // 고블린 파괴됨
        }
        else if (monsterType == "Mushroom")
        {
            hasMushroom = false;  // 버섯 파괴됨
        }
    }
}
