using UnityEngine;

public class GoblinSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;  // ������ ��� ������
    public Transform spawnPoint1F;   // 1���� ���� ��ġ
    public float spawnInterval = 5f;  // ����� �����Ǵ� �ð� ����

    private bool hasGoblin = false;  // 1���� ����� �ִ��� ���θ� ����

    void Start()
    {
        // ���� �ð� �������� ����� ����
        InvokeRepeating("SpawnGoblin", spawnInterval, spawnInterval);
    }

    void SpawnGoblin()
    {
        // 1���� ����� �̹� ������ �� �̻� �������� ����
        if (!hasGoblin)
        {
            // 1�� ���� ����Ʈ���� ��� ���� (y������ 180�� ȸ��)
            GameObject newGoblin = Instantiate(goblinPrefab, spawnPoint1F.position, Quaternion.Euler(0, 180, 0));
            
            // ����� �����Ǿ����� ǥ��
            hasGoblin = true;

            // ����� �ı��Ǹ� �ٽ� ������ �� �ֵ��� ����
            GoblinController goblinController = newGoblin.GetComponent<GoblinController>();
            if (goblinController != null)
            {
                goblinController.OnGoblinDestroyed += OnGoblinDestroyed;
            }
        }
    }

    // ����� �ı��Ǿ��� �� ȣ��Ǵ� �Լ�
    void OnGoblinDestroyed()
    {
        hasGoblin = false;  // ����� �ı��Ǹ� �ٽ� ������ �� �ֵ��� ���� ����
    }
}
