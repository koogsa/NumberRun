using UnityEngine;

public class GoblinSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;  // 스폰할 고블린 프리팹
    public Transform spawnPoint1F;   // 1층의 스폰 위치
    public float spawnInterval = 5f;  // 고블린이 스폰되는 시간 간격

    private bool hasGoblin = false;  // 1층에 고블린이 있는지 여부를 추적

    void Start()
    {
        // 일정 시간 간격으로 고블린을 스폰
        InvokeRepeating("SpawnGoblin", spawnInterval, spawnInterval);
    }

    void SpawnGoblin()
    {
        // 1층에 고블린이 이미 있으면 더 이상 스폰하지 않음
        if (!hasGoblin)
        {
            // 1층 스폰 포인트에서 고블린 스폰 (y축으로 180도 회전)
            GameObject newGoblin = Instantiate(goblinPrefab, spawnPoint1F.position, Quaternion.Euler(0, 180, 0));
            
            // 고블린이 스폰되었음을 표시
            hasGoblin = true;

            // 고블린이 파괴되면 다시 스폰할 수 있도록 설정
            GoblinController goblinController = newGoblin.GetComponent<GoblinController>();
            if (goblinController != null)
            {
                goblinController.OnGoblinDestroyed += OnGoblinDestroyed;
            }
        }
    }

    // 고블린이 파괴되었을 때 호출되는 함수
    void OnGoblinDestroyed()
    {
        hasGoblin = false;  // 고블린이 파괴되면 다시 스폰할 수 있도록 상태 변경
    }
}
