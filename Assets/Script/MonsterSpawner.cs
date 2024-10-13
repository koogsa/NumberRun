using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;   // ������ ��� ������
    public GameObject mushroomPrefab; // ������ ���� ���� ������

    public Transform spawnPoint1F;    // 1���� ���� ��ġ
    public Transform spawnPoint2F;    // 2���� ���� ��ġ
    public Transform spawnPoint3F;    // 3���� ���� ��ġ

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

    // ���Ͱ� �ı��� �� ȣ��Ǵ� �޼���
    public void OnMonsterDestroyed(string monsterType)
    {
        if (monsterType == "Goblin")
        {
            hasGoblin = false;  // ��� �ı���
        }
        else if (monsterType == "Mushroom")
        {
            hasMushroom = false;  // ���� �ı���
        }
    }
}
