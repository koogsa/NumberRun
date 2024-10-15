using UnityEngine;

public class MonsterSpawnerStage2 : MonoBehaviour
{
    public GameObject batPrefab;       // 박쥐 프리팹
    public GameObject skeletonPrefab;  // 스켈레톤 프리팹

    public Transform spawnPoint1F;  // 1층의 스폰 위치
    public Transform spawnPoint2F;  // 2층의 스폰 위치
    public Transform spawnPoint3F;  // 3층의 스폰 위치

    public float batMinSpawnInterval = 5f;  // 박쥐 최소 스폰 시간
    public float batMaxSpawnInterval = 10f; // 박쥐 최대 스폰 시간
    public float skeletonMinSpawnInterval = 7f;  // 스켈레톤 최소 스폰 시간
    public float skeletonMaxSpawnInterval = 12f; // 스켈레톤 최대 스폰 시간

    private bool hasBat = false;      // 박쥐가 스폰되었는지 여부
    private bool hasSkeleton = false; // 스켈레톤이 스폰되었는지 여부

    void Start()
    {
        // 첫 번째 박쥐 스폰
        float firstBatSpawnTime = Random.Range(batMinSpawnInterval, batMaxSpawnInterval);
        Invoke("SpawnBat", firstBatSpawnTime);

        // 첫 번째 스켈레톤 스폰
        float firstSkeletonSpawnTime = Random.Range(skeletonMinSpawnInterval, skeletonMaxSpawnInterval);
        Invoke("SpawnSkeleton", firstSkeletonSpawnTime);
    }

    void SpawnBat()
    {
        if (!hasBat)
        {
            // 2층에 박쥐 스폰
            GameObject newBat = Instantiate(batPrefab, spawnPoint2F.position, Quaternion.Euler(0, 180, 0));
            hasBat = true;

            // 박쥐가 죽으면 다시 스폰 가능하도록 설정
            BatController batController = newBat.GetComponent<BatController>();
            if (batController != null)
            {
                batController.OnMonsterDestroyed += () => hasBat = false;
            }
        }

        // 다음 박쥐 스폰 시간 설정
        float nextBatSpawnTime = Random.Range(batMinSpawnInterval, batMaxSpawnInterval);
        Invoke("SpawnBat", nextBatSpawnTime);
    }

    void SpawnSkeleton()
    {
        if (!hasSkeleton)
        {
            // 3층에 스켈레톤 스폰
            GameObject newSkeleton = Instantiate(skeletonPrefab, spawnPoint3F.position, Quaternion.Euler(0, 180, 0));
            hasSkeleton = true;

            // 스켈레톤이 죽으면 다시 스폰 가능하도록 설정
            SkeletonController skeletonController = newSkeleton.GetComponent<SkeletonController>();
            if (skeletonController != null)
            {
                skeletonController.OnMonsterDestroyed += () => hasSkeleton = false;
            }
        }

        // 다음 스켈레톤 스폰 시간 설정
        float nextSkeletonSpawnTime = Random.Range(skeletonMinSpawnInterval, skeletonMaxSpawnInterval);
        Invoke("SpawnSkeleton", nextSkeletonSpawnTime);
    }

    // 몬스터가 파괴될 때 호출되는 메서드
    public void OnMonsterDestroyed(string monsterType)
    {
        if (monsterType == "Bat")
        {
            hasBat = false;  // 박쥐 파괴됨
        }
        else if (monsterType == "Skeleton")
        {
            hasSkeleton = false;  // 스켈레톤 파괴됨
        }
    }
}
