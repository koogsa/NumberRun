using UnityEngine;
using System.Collections;
public class BossController : MonoBehaviour
{
    public Transform spawnPoint1F;  // 1층 스폰 위치
    public Transform spawnPoint2F;  // 2층 스폰 위치
    public float minSpeed = 2f;     // 최소 속도
    public float maxSpeed = 10f;    // 최대 속도
    public float acceleration = 0.1f;  // 가속도
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
        // 보스를 첫 번째 위치에서 스폰
        SpawnBoss();
    }
    // 플레이어와 충돌 감지
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어에게 데미지를 입힘
            HeroKnight player = collision.gameObject.GetComponent<HeroKnight>();
            if (player != null)
            {
                player.TakeDamage(1);  // 플레이어에게 데미지 입힘
                // 충돌 후 일정 시간 동안 충돌을 무시
                StartCoroutine(TemporarilyIgnoreCollision(player.gameObject.GetComponent<Collider2D>()));
            }
        }
    }
    // 일정 시간 동안 충돌을 무시하는 코루틴
    private IEnumerator TemporarilyIgnoreCollision(Collider2D playerCollider)
    {
        Collider2D bossCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(bossCollider, playerCollider, true);  // 충돌 무시
        yield return new WaitForSeconds(2f);  // 1초 동안 충돌 무시
        Physics2D.IgnoreCollision(bossCollider, playerCollider, false);  // 다시 충돌 감지
    }
    // 플레이어를 죽였을 때 호출되는 함수
    public void OnPlayerDeath()
    {
        // 보스의 움직임을 멈추고 Victory 애니메이션 실행
        isVictorious = true;
        rb.velocity = Vector2.zero;  // 보스의 속도를 0으로 설정하여 움직임 멈추기
        animator.SetBool("isVictorious", true);  // Victory 애니메이션 실행
    }


    void Update()
    {
        if (isMoving)
        {
            // 보스 이동
            transform.position += Vector3.left * currentSpeed * Time.deltaTime;

            // 보스가 화면 왼쪽을 벗어나면 다시 스폰
            if (transform.position.x < -10f)  // 화면 밖으로 나가면
            {
                SpawnBoss();
            }

            // 보스 속도 증가
            currentSpeed += acceleration * Time.deltaTime;

            // 속도가 최대 속도를 넘지 않게 제한
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
                isMoving = false;  // 속도가 최대에 도달하면 멈춤
                TriggerBossStop(); // 보스 멈춤 후 공격 이벤트
            }
        }
        if (isVictorious)
        {
            // Victory 상태일 때 보스는 계속 그 자리에 멈춰 있음
            rb.velocity = Vector2.zero;
        }
    }

    // 보스를 랜덤한 층에 스폰
    void SpawnBoss()
    {
        int randomFloor = Random.Range(0, 2);
        targetPosition = (randomFloor == 0) ? spawnPoint1F.position : spawnPoint2F.position;
        transform.position = new Vector3(10f, targetPosition.y, targetPosition.z);  // 오른쪽 끝에서 등장
        currentSpeed = minSpeed;
        isMoving = true;
    }

    // 보스가 멈췄을 때 공격 이벤트를 트리거
    void TriggerBossStop()
    {
        Debug.Log("보스가 멈췄습니다. 플레이어가 공격할 수 있습니다.");
        // 여기에 공격 가능한 텍스트 문제를 띄우는 기능 추가
        ShowAttackText();
    }

    void ShowAttackText()
    {
        // 보스 위에 문제 텍스트를 띄움 (예: 수학 문제)
        // 텍스트 UI가 연결된다면 문제를 표시
    }
}
