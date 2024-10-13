using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;   // ������ ��� ������
    public GameObject mushroomPrefab; // ������ ���� ���� ������

    public Transform spawnPoint1F;    // 1���� ���� ��ġ
    public Transform spawnPoint2F;    // 2���� ���� ��ġ
    public Transform spawnPoint3F;    // 3���� ���� ��ġ

    // ��� ���� �ð� ����
    public float goblinMinSpawnInterval = 5f;  // ��� �ּ� ���� �ð�
    public float goblinMaxSpawnInterval = 10f; // ��� �ִ� ���� �ð�

    // ���� ���� ���� �ð� ����
    public float mushroomMinSpawnInterval = 7f;  // ���� ���� �ּ� ���� �ð�
    public float mushroomMaxSpawnInterval = 12f; // ���� ���� �ִ� ���� �ð�

    private bool hasGoblin = false;   // 1���� ����� �ִ��� ����
    private bool hasMushroom = false; // 2���� ���� ���Ͱ� �ִ��� ����

    void Start()
    {
        // ù ��° ��� ����
        float firstGoblinSpawnTime = Random.Range(goblinMinSpawnInterval, goblinMaxSpawnInterval);
        Invoke("SpawnGoblin", firstGoblinSpawnTime);

        // ù ��° ���� ���� ����
        float firstMushroomSpawnTime = Random.Range(mushroomMinSpawnInterval, mushroomMaxSpawnInterval);
        Invoke("SpawnMushroom", firstMushroomSpawnTime);
    }

    void SpawnGoblin()
    {
        // 1���� ����� ������ ��� ����
        if (!hasGoblin)
        {
            GameObject newGoblin = Instantiate(goblinPrefab, spawnPoint1F.position, Quaternion.Euler(0, 180, 0));
            hasGoblin = true;

            GoblinController goblinController = newGoblin.GetComponent<GoblinController>();
            if (goblinController != null)
            {
                goblinController.OnGoblinDestroyed += () => hasGoblin = false; // ��� �ı� �� ���� �����ϵ��� ���� ����
            }
        }

        // ���� ��� ���� �ð� ����
        float nextGoblinSpawnTime = Random.Range(goblinMinSpawnInterval, goblinMaxSpawnInterval);
        Invoke("SpawnGoblin", nextGoblinSpawnTime);  // ������ �ð� �� �ٽ� ����
    }

    void SpawnMushroom()
    {
        // 2���� ���� ���Ͱ� ������ ���� ���� ����
        if (!hasMushroom)
        {
            GameObject newMushroom = Instantiate(mushroomPrefab, spawnPoint2F.position, Quaternion.Euler(0, 180, 0));
            hasMushroom = true;

            MushroomController mushroomController = newMushroom.GetComponent<MushroomController>();
            if (mushroomController != null)
            {
                mushroomController.OnMushroomDestroyed += () => hasMushroom = false; // ���� ���� �ı� �� ���� �����ϵ��� ���� ����
            }
        }

        // ���� ���� ���� ���� �ð� ����
        float nextMushroomSpawnTime = Random.Range(mushroomMinSpawnInterval, mushroomMaxSpawnInterval);
        Invoke("SpawnMushroom", nextMushroomSpawnTime);  // ������ �ð� �� �ٽ� ����
    }
}
