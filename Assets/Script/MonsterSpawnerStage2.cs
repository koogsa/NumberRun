using UnityEngine;

public class MonsterSpawnerStage2 : MonoBehaviour
{
    public GameObject batPrefab;       // ���� ������
    public GameObject skeletonPrefab;  // ���̷��� ������

    public Transform spawnPoint1F;  // 1���� ���� ��ġ
    public Transform spawnPoint2F;  // 2���� ���� ��ġ
    public Transform spawnPoint3F;  // 3���� ���� ��ġ

    public float batMinSpawnInterval = 5f;  // ���� �ּ� ���� �ð�
    public float batMaxSpawnInterval = 10f; // ���� �ִ� ���� �ð�
    public float skeletonMinSpawnInterval = 7f;  // ���̷��� �ּ� ���� �ð�
    public float skeletonMaxSpawnInterval = 12f; // ���̷��� �ִ� ���� �ð�

    private bool hasBat = false;      // ���㰡 �����Ǿ����� ����
    private bool hasSkeleton = false; // ���̷����� �����Ǿ����� ����

    void Start()
    {
        // ù ��° ���� ����
        float firstBatSpawnTime = Random.Range(batMinSpawnInterval, batMaxSpawnInterval);
        Invoke("SpawnBat", firstBatSpawnTime);

        // ù ��° ���̷��� ����
        float firstSkeletonSpawnTime = Random.Range(skeletonMinSpawnInterval, skeletonMaxSpawnInterval);
        Invoke("SpawnSkeleton", firstSkeletonSpawnTime);
    }

    void SpawnBat()
    {
        if (!hasBat)
        {
            // 2���� ���� ����
            GameObject newBat = Instantiate(batPrefab, spawnPoint2F.position, Quaternion.Euler(0, 180, 0));
            hasBat = true;

            // ���㰡 ������ �ٽ� ���� �����ϵ��� ����
            BatController batController = newBat.GetComponent<BatController>();
            if (batController != null)
            {
                batController.OnMonsterDestroyed += () => hasBat = false;
            }
        }

        // ���� ���� ���� �ð� ����
        float nextBatSpawnTime = Random.Range(batMinSpawnInterval, batMaxSpawnInterval);
        Invoke("SpawnBat", nextBatSpawnTime);
    }

    void SpawnSkeleton()
    {
        if (!hasSkeleton)
        {
            // 3���� ���̷��� ����
            GameObject newSkeleton = Instantiate(skeletonPrefab, spawnPoint3F.position, Quaternion.Euler(0, 180, 0));
            hasSkeleton = true;

            // ���̷����� ������ �ٽ� ���� �����ϵ��� ����
            SkeletonController skeletonController = newSkeleton.GetComponent<SkeletonController>();
            if (skeletonController != null)
            {
                skeletonController.OnMonsterDestroyed += () => hasSkeleton = false;
            }
        }

        // ���� ���̷��� ���� �ð� ����
        float nextSkeletonSpawnTime = Random.Range(skeletonMinSpawnInterval, skeletonMaxSpawnInterval);
        Invoke("SpawnSkeleton", nextSkeletonSpawnTime);
    }

    // ���Ͱ� �ı��� �� ȣ��Ǵ� �޼���
    public void OnMonsterDestroyed(string monsterType)
    {
        if (monsterType == "Bat")
        {
            hasBat = false;  // ���� �ı���
        }
        else if (monsterType == "Skeleton")
        {
            hasSkeleton = false;  // ���̷��� �ı���
        }
    }
}
