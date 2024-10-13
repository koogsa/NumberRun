using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;   // 스폰할 고블린 프리팹
    public GameObject mushroomPrefab; // 스폰할 버섯 몬스터 프리팹

    public Transform spawnPoint1F;    // 1층의 스폰 위치
    public Transform spawnPoint2F;    // 2층의 스폰 위치
    public Transform spawnPoint3F;    // 3층의 스폰 위치

    public float goblinMinSpawnInterval = 5f;
    public float goblinMaxSpawnInterval = 10f;

    public float mushroomMinSpawnInterval = 7f;
    public float mushroomMaxSpawnInterval = 12f;

    private bool hasGoblin = false;
    private bool hasMushroom = false;

    void Start()
    {
        float firstGoblinSpawnTime = Random.Range(goblinMinSpawnInterval, goblinMaxSpawnInterval);
        Invoke("SpawnGoblin", firstGoblinSpawnTime);

        float firstMushroomSpawnTime = Random.Range(mushroomMinSpawnInterval, mushroomMaxSpawnInterval);
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
                goblinController.OnGoblinDestroyed += () => hasGoblin = false;
            }
        }

        float nextGoblinSpawnTime = Random.Range(goblinMinSpawnInterval, goblinMaxSpawnInterval);
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
                mushroomController.OnMushroomDestroyed += () => hasMushroom = false;
            }
        }

        float nextMushroomSpawnTime = Random.Range(mushroomMinSpawnInterval, mushroomMaxSpawnInterval);
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
