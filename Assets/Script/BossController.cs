using UnityEngine;
using System.Collections;
public class BossController : MonoBehaviour
{
    public Transform spawnPoint1F;  // 1�� ���� ��ġ
    public Transform spawnPoint2F;  // 2�� ���� ��ġ
    public float minSpeed = 2f;     // �ּ� �ӵ�
    public float maxSpeed = 10f;    // �ִ� �ӵ�
    public float acceleration = 0.1f;  // ���ӵ�
    private float currentSpeed;
    private Vector3 targetPosition;
    private bool isMoving = true;
    private bool isVictorious = false;
    private Rigidbody2D rb;
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // ������ ù ��° ��ġ���� ����
        SpawnBoss();
    }
    // �÷��̾�� �浹 ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� �������� ����
            HeroKnight player = collision.gameObject.GetComponent<HeroKnight>();
            if (player != null)
            {
                player.TakeDamage(1);  // �÷��̾�� ������ ����
                // �浹 �� ���� �ð� ���� �浹�� ����
                StartCoroutine(TemporarilyIgnoreCollision(player.gameObject.GetComponent<Collider2D>()));
            }
        }
    }
    // ���� �ð� ���� �浹�� �����ϴ� �ڷ�ƾ
    private IEnumerator TemporarilyIgnoreCollision(Collider2D playerCollider)
    {
        Collider2D bossCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(bossCollider, playerCollider, true);  // �浹 ����
        yield return new WaitForSeconds(2f);  // 1�� ���� �浹 ����
        Physics2D.IgnoreCollision(bossCollider, playerCollider, false);  // �ٽ� �浹 ����
    }
    // �÷��̾ �׿��� �� ȣ��Ǵ� �Լ�
    public void OnPlayerDeath()
    {
        // ������ �������� ���߰� Victory �ִϸ��̼� ����
        isVictorious = true;
        rb.velocity = Vector2.zero;  // ������ �ӵ��� 0���� �����Ͽ� ������ ���߱�
        animator.SetBool("isVictorious", true);  // Victory �ִϸ��̼� ����
    }


    void Update()
    {
        if (isMoving)
        {
            // ���� �̵�
            transform.position += Vector3.left * currentSpeed * Time.deltaTime;

            // ������ ȭ�� ������ ����� �ٽ� ����
            if (transform.position.x < -10f)  // ȭ�� ������ ������
            {
                SpawnBoss();
            }

            // ���� �ӵ� ����
            currentSpeed += acceleration * Time.deltaTime;

            // �ӵ��� �ִ� �ӵ��� ���� �ʰ� ����
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
                isMoving = false;  // �ӵ��� �ִ뿡 �����ϸ� ����
                TriggerBossStop(); // ���� ���� �� ���� �̺�Ʈ
            }
        }
        if (isVictorious)
        {
            // Victory ������ �� ������ ��� �� �ڸ��� ���� ����
            rb.velocity = Vector2.zero;
        }
    }

    // ������ ������ ���� ����
    void SpawnBoss()
    {
        int randomFloor = Random.Range(0, 2);
        targetPosition = (randomFloor == 0) ? spawnPoint1F.position : spawnPoint2F.position;
        transform.position = new Vector3(10f, targetPosition.y, targetPosition.z);  // ������ ������ ����
        currentSpeed = minSpeed;
        isMoving = true;
    }

    // ������ ������ �� ���� �̺�Ʈ�� Ʈ����
    void TriggerBossStop()
    {
        Debug.Log("������ ������ϴ�. �÷��̾ ������ �� �ֽ��ϴ�.");
        // ���⿡ ���� ������ �ؽ�Ʈ ������ ���� ��� �߰�
        ShowAttackText();
    }

    void ShowAttackText()
    {
        // ���� ���� ���� �ؽ�Ʈ�� ��� (��: ���� ����)
        // �ؽ�Ʈ UI�� ����ȴٸ� ������ ǥ��
    }
}
