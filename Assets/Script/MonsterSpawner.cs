using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;   // ������ ��� ������
    public GameObject mushroomPrefab; // ������ ���� ���� ������

    public Transform spawnPoint1F;    // 1���� ���� ��ġ
    public Transform spawnPoint2F;    // 2���� ���� ��ġ
    public Transform spawnPoint3F;    // 3���� ���� ��ġ

    public float minSpawnInterval = 5f;  // �ּ� ���� �ð� ����
    public float maxSpawnInterval = 10f; // �ִ� ���� �ð� ����

    private bool hasGoblin = false;   // 1���� ����� �ִ��� ����
    private bool hasMushroom = false; // 2���� ���� ���Ͱ� �ִ��� ����

    void Start()
    {
        // ù ������ ������ �ð� �Ŀ� �߻��ϵ��� ����
        float firstSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnMonster", firstSpawnTime);
    }

    void SpawnMonster()
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

        // ���� ���� �ð� ���� (5~10�� ������ ������ ��)
        float nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnMonster", nextSpawnTime);  // ������ �ð� �� �ٽ� ����
    }
}
