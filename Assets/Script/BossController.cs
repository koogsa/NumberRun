using UnityEngine;
using TMPro;
using System.Collections;

public class BossController : MonoBehaviour
{
    public Transform spawnPoint1F;  // 1층 스폰 위치
    public Transform spawnPoint2F;  // 2층 스폰 위치
    public float minSpeed = 10f;    // 최소 속도
    public float maxSpeed = 20f;    // 최대 속도
    public float acceleration = 1f;  // 가속도
    private float currentSpeed;
    private Vector3 targetPosition;
    private bool isMoving = true;
    private bool isVictorious = false;
    private bool isAttackable = false;  // 보스 공격 가능 상태

    private Rigidbody2D rb;
    private Animator animator;

    public TextMeshProUGUI attackText;  // 공격 텍스트 UI
    private int correctAnswer;  // 플레이어가 맞춰야 할 정답
    public HeroKnight player;   // 플레이어 참조

    public float atktimeDuration = 2f;  // atktime 애니메이션 재생 시간

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SpawnBoss();  // 보스 처음 스폰
    }

    // 플레이어와 충돌 감지
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HeroKnight player = collision.gameObject.GetComponent<HeroKnight>();
            if (player != null)
            {
                player.TakeDamage(1);  // 플레이어에게 데미지 입힘
                StartCoroutine(TemporarilyIgnoreCollision(player.gameObject.GetComponent<Collider2D>()));
            }
        }
    }

    // 일정 시간 동안 충돌을 무시하는 코루틴
    private IEnumerator TemporarilyIgnoreCollision(Collider2D playerCollider)
    {
        Collider2D bossCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(bossCollider, playerCollider, true);  // 충돌 무시
        yield return new WaitForSeconds(2f);  // 2초 동안 충돌 무시
        Physics2D.IgnoreCollision(bossCollider, playerCollider, false);  // 다시 충돌 감지
    }

    // 플레이어를 죽였을 때 호출되는 함수
    public void OnPlayerDeath()
    {
        isVictorious = true;
        rb.velocity = Vector2.zero;  // 보스의 속도를 0으로 설정
        animator.SetBool("isVictorious", true);  // Victory 애니메이션 실행
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += Vector3.left * currentSpeed * Time.deltaTime;

            if (transform.position.x < -10f)
            {
                SpawnBoss();
            }

            currentSpeed += acceleration * Time.deltaTime;
            Debug.Log("현재 속도: " + currentSpeed);
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
                isMoving = false;  // 속도가 최대에 도달하면 멈춤
                TriggerBossStop();
            }
        }

        if (isVictorious)
        {
            rb.velocity = Vector2.zero;  // Victory 상태일 때 보스는 멈춤
        }

        if (isAttackable && Input.GetKeyDown(KeyCode.Return))  // 엔터 키로 공격
        {
            CheckAnswer();  // 플레이어가 문제를 맞췄는지 확인
        }
    }

    // 보스를 랜덤한 층에 스폰
    void SpawnBoss()
    {
        int randomFloor = Random.Range(0, 2);
        targetPosition = (randomFloor == 0) ? spawnPoint1F.position : spawnPoint2F.position;
        transform.position = new Vector3(10f, targetPosition.y, targetPosition.z);  // 오른쪽 끝에서 등장

        // 속도를 초기화하지 않고 현재 속도를 유지하도록 수정
        if (currentSpeed < minSpeed)
        {
            currentSpeed = minSpeed;  // 처음 스폰될 때만 최소 속도로 설정
        }

        isMoving = true;
        animator.SetTrigger("isMoving");
    }


    // 보스가 멈췄을 때 공격 이벤트를 트리거
    void TriggerBossStop()
    {
        Debug.Log("보스가 멈췄습니다. 플레이어가 공격할 수 있습니다.");
        StartCoroutine(PlayAtktimeThenResume());  // atktime 애니메이션 재생 후 이동 재개
        isAttackable = true;  // 공격 가능 상태로 변경
        animator.SetBool("isMoving", false);  // 보스가 멈추므로 이동 애니메이션 중지
    }

    // atktime 애니메이션을 실행하고, 일정 시간 후에 다시 이동 시작
    IEnumerator PlayAtktimeThenResume()
    {
        // 애니메이션 실행
        animator.SetTrigger("isAttackable");

        // 일정 시간 대기
        yield return new WaitForSeconds(atktimeDuration);

        // 문제 텍스트를 표시하고, 보스가 다시 이동 시작
        ShowAttackText();
        isAttackable = false;  // 공격 가능 상태 종료

        // currentSpeed를 maxSpeed로 유지
        currentSpeed = minSpeed;  // 속도를 최대 속도로 유지 (필요에 따라 수정 가능)

        isMoving = true;

        animator.SetTrigger("isMoving");  // 이동 애니메이션 재실행
    }
    // 문제 텍스트를 표시하고 정답을 설정
    void ShowAttackText()
    {
        if (attackText != null)
        {
            int num1 = Random.Range(1, 10);
            int num2 = Random.Range(1, 10);
            correctAnswer = num1 + num2;  // 문제는 덧셈 문제
            attackText.text = $"{num1} + {num2} = ?";  // 텍스트에 문제 표시
            attackText.gameObject.SetActive(true);  // 텍스트 보이게 함
        }
    }

    // 플레이어가 입력한 답을 확인
    void CheckAnswer()
    {
        string playerInput = "";  // 플레이어가 입력한 값을 받아와야 함 (키보드 입력 또는 UI 입력 방식 구현 필요)
        if (int.TryParse(playerInput, out int playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                Debug.Log("정답! 보스에게 데미지를 입힘");
                TakeDamage();  // 보스에게 데미지 입힘
            }
            else
            {
                Debug.Log("오답! 다시 시도하세요.");
            }
        }
    }

    // 보스가 데미지를 입는 함수
    void TakeDamage()
    {
        animator.SetTrigger("TakeDamage");  // 보스 피격 애니메이션 실행
        attackText.gameObject.SetActive(false);  // 문제 텍스트 숨김
        isAttackable = false;  // 다시 이동 시작하도록 상태 변경
        currentSpeed = minSpeed;  // 속도 초기화
        isMoving = true;  // 이동 다시 시작

        animator.SetBool("isMoving", true);  // 보스 이동 애니메이션 실행
    }
}
