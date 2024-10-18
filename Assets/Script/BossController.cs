using UnityEngine;
using TMPro;
using UnityEngine.UI;  // UI 요소 사용을 위해 추가
using System.Collections;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public Transform spawnPoint1F;  // 1층 스폰 위치
    public Transform spawnPoint2F;  // 2층 스폰 위치
    public float minSpeed = 10f;    // 최소 속도
    public float maxSpeed = 15f;    // 최대 속도
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

    // 보스 체력 관련 변수
    public Slider bossHpBar; // 보스 HP 바
    public int maxHealth = 10;   // 보스의 최대 체력
    private float currentHealth;

    public float atktimeDuration = 2f;  // atktime 애니메이션 재생 시간

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SpawnBoss();  // 보스 처음 스폰

        currentHealth = maxHealth; // 보스 체력을 최대 체력으로 설정
        bossHpBar.maxValue = maxHealth; // HP 바의 최대값 설정
        bossHpBar.value = currentHealth; // 현재 체력을 HP 바에 반영
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
           // Debug.Log("현재 속도: " + currentSpeed);
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

        // 문제 텍스트를 표시하고
        ShowAttackText();

        // 일정 시간 대기
        yield return new WaitForSeconds(atktimeDuration);

        // 문제 텍스트 숨김
        HideAttackText();

        isAttackable = false;  // 공격 가능 상태 종료

        // currentSpeed를 maxSpeed로 유지
        currentSpeed = minSpeed;  // 속도를 초기화

        isMoving = true;

        animator.SetTrigger("isMoving");  // 이동 애니메이션 재실행
    }

    // 문제 텍스트를 표시하고 정답을 설정
    void ShowAttackText()
    {
        if (attackText != null)
        {
            int num1 = 0, num2 = 0;  // 기본값으로 초기화
            correctAnswer = 0;
            string operation = "";

            // 한 자리 수가 나올 때까지 문제 생성
            while (correctAnswer < 1 || correctAnswer > 9)
            {
                num1 = Random.Range(1, 10);  // 1~9 범위의 수
                num2 = Random.Range(1, 10);  // 1~9 범위의 수

                // 랜덤하게 연산자 선택 (더하기, 빼기, 곱하기, 나누기)
                int operationIndex = Random.Range(0, 4);
                switch (operationIndex)
                {
                    case 0:  // 더하기
                        correctAnswer = num1 + num2;
                        operation = "+";
                        break;
                    case 1:  // 빼기
                        correctAnswer = num1 - num2;
                        operation = "-";
                        break;
                    case 2:  // 곱하기
                        correctAnswer = num1 * num2;
                        operation = "*";
                        break;
                    case 3:  // 나누기 (정확히 나누어떨어지는 경우만)
                        if (num2 != 0 && num1 % num2 == 0)  // num2가 0이 아니고 나누어떨어질 경우만
                        {
                            correctAnswer = num1 / num2;
                            operation = "/";
                        }
                        break;
                }
            }

            // 문제 텍스트 설정
            attackText.text = $"{num1} {operation} {num2} = ?";
            attackText.gameObject.SetActive(true);  // 텍스트 보이게 함
        }
    }

    // 문제 텍스트를 숨기는 함수
    void HideAttackText()
    {
        if (attackText != null)
        {
            attackText.gameObject.SetActive(false);  // 텍스트 숨김
        }
    }

    // 플레이어가 입력한 답을 확인
    void CheckAnswer()
    {
        string playerInput = "";  
        if (int.TryParse(playerInput, out int playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                Debug.Log("정답! 보스에게 데미지를 입힘");
                TakeDamage(10);  
            }
        }
    }
    public bool CheckPlayerInput(int inputNumber)
    {
        if (inputNumber == correctAnswer)
        {
            TakeDamage(1);  // 정답이면 보스에게 데미지
            return true;
        }
        return false;
    }

    // 보스가 데미지를 입는 함수
    void TakeDamage(int damage)
    {
        currentHealth -= damage;  // 데미지 받기
        Debug.Log($"보스 체력: {currentHealth}/{maxHealth}");  // 체력 디버그 로그 출력
        bossHpBar.value = currentHealth;  // HP 바 업데이트

        if (currentHealth <= 0)
        {
            Die();  // 체력이 0 이하가 되면 사망 처리
        }
        else
        {
            animator.SetTrigger("TakeDamage");  // 보스 피격 애니메이션 실행
        }
    }

    // 보스가 사망하는 함수
    void Die()
    {
        Debug.Log("보스 사망");
        // 사망 애니메이션 실행
        animator.SetTrigger("Die");
        Destroy(gameObject);
        SceneManager.LoadScene("GameClear");
    }
}
