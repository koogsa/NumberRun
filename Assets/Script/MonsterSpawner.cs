using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;   // ������ ��� ������
    public GameObject mushroomPrefab; // ������ ���� ���� ������

    public Transform spawnPoint1F;    // 1���� ���� ��ġ
    public Transform spawnPoint2F;    // 2���� ���� ��ġ

    public float minSpawnInterval = 5f;  // �ּ� ���� �ð� ����
    public float maxSpawnInterval = 10f; // �ִ� ���� �ð� ����

    private bool hasGoblin = false;    // 1�� ��� ���� ����
    private bool hasMushroom = false;  // 2�� ���� ���� ����

    void Start()
    {
        // ��� ���� ����
        float firstGoblinSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnGoblin", firstGoblinSpawnTime);

        // ���� ���� ����
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
                goblinController.OnGoblinDestroyed += () => hasGoblin = false;  // ����� �ı��� �� hasGoblin�� false�� ����
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
                mushroomController.OnMushroomDestroyed += () => hasMushroom = false;  // ������ �ı��� �� hasMushroom�� false�� ����
            }
        }

        float nextMushroomSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
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
