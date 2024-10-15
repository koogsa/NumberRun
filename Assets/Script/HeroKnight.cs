using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroKnight : MonoBehaviour
{
    [SerializeField] float m_jumpForce = 8f;
    [SerializeField] float ignoreCollisionTime = 0.5f;  // 충돌을 무시할 시간 (초)
    [SerializeField] float inputCooldown = 0.5f;  // 점프나 S키를 연속으로 누를 수 없게 하는 쿨다운 시간 (초)
    [SerializeField] int maxHealth = 3;  // 캐릭터의 최대 체력 (3)
    private int currentHealth;  // 현재 체력
    public float stunDuration = 2.0f;  // 스턴 상태 지속 시간

    public GameObject[] healthImages;  // 4개의 체력 이미지 배열 (체력 3, 2, 1, 0일 때)

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private bool m_grounded = false;
    private Collider2D m_collider;  // 캐릭터의 충돌체
    private float lastInputTime = -Mathf.Infinity;  // 마지막 입력 시간 기록
    private bool isStunned = false;  // 플레이어가 스턴 상태인지 여부
    public Collider2D platformCollider2;  // 2층의 충돌체
    public Collider2D platformCollider3;  // 3층의 충돌체
    public Image fillImage;  // Fill 영역의 이미지 (Inspector에서 할당)
    public Slider gaugeBar;  // 사용자 정의 스프라이트가 적용된 UI 슬라이더 참조
    private float currentGauge = 0f;  // 현재 게이지 값
    private float maxGauge = 100f;  // 게이지 최대 값
    public float gaugePerMonster = 20f;  // 몬스터 하나당 추가될 게이지 값
    public FadeManager fadeManager;

    // 초기화
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();

        // 체력을 최대값으로 설정
        currentHealth = maxHealth;

        // 체력 이미지 초기화
        UpdateHealthImages();

        // Fill 이미지 처음에는 안보이게 설정
        if (fillImage != null)
        {
            fillImage.enabled = false;  // Fill 이미지 비활성화
        }

        // 게이지바 초기화
        if (gaugeBar != null)
        {
            gaugeBar.maxValue = maxGauge;
            gaugeBar.value = currentGauge;
        }
    }

    // 체력 이미지 업데이트 함수
    void UpdateHealthImages()
    {
        // 현재 체력 상태에 따라 이미지 활성화/비활성화
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (i < currentHealth)
            {
                healthImages[i].SetActive(true);  // 남아있는 체력에 해당하는 이미지는 활성화
            }
            else
            {
                healthImages[i].SetActive(false);  // 감소한 체력에 해당하는 이미지는 비활성화
            }
        }
    }

    // 몬스터를 잡을 때 게이지 증가
    public void IncreaseGauge()
    {
        currentGauge += gaugePerMonster;  // 몬스터를 잡을 때마다 게이지 증가
        Debug.Log("현재 게이지: " + currentGauge);  // 게이지 값 출력

        if (gaugeBar != null)
        {
            gaugeBar.value = currentGauge;  // UI 슬라이더 업데이트
            Debug.Log("UI 슬라이더 값: " + gaugeBar.value);  // 슬라이더 값 출력
        }
        // 게이지가 증가하면 Fill 이미지 활성화
        if (currentGauge > 0 && fillImage != null)
        {
            fillImage.enabled = true;  // Fill 이미지 활성화
            Debug.Log("이미지 활성화~");
        }

        // 게이지가 최대에 도달했을 때 이벤트 발생 (예: 다음 스테이지로 이동)
        if (currentGauge >= maxGauge)
        {
            Debug.Log("게이지가 최대치에 도달했습니다. 다음 스테이지로 이동합니다!");
            LoadNextStage();
            // 다음 스테이지로 이동하는 코드 추가
        }
        if (currentGauge >= maxGauge)
        {
            LoadStage3();
        }
        // Stage 3로 씬 전환 함수
        void LoadStage3()
        {
            Debug.Log("게이지가 최대치에 도달했습니다. Stage3로 이동합니다!");
            SceneManager.LoadScene("Stage3");  // Stage3 씬으로 전환
        }

    }

    // 다음 스테이지로 이동하는 함수
    void LoadNextStage()
    {
        SceneManager.LoadScene("Stage2");  // Stage 2 씬으로 전환
    }


    // 매 프레임마다 호출
    void Update()
    {
        // 스턴 상태일 경우 조작 불가
        if (isStunned)
        {
            return;
        }

        // 쿨다운 시간이 지나지 않았으면 입력을 무시
        if (Time.time - lastInputTime < inputCooldown)
        {
            return;
        }

        // 점프 처리 (space 키)
        if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetTrigger("Jump");  // 점프 애니메이션 실행
            m_grounded = false;
            m_animator.SetBool("IsGrounded", false);  // 점프 시 IsGrounded를 false로 설정
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);

            // 점프 중에도 2층 및 3층과의 충돌을 일정 시간 동안 무시
            StartCoroutine(IgnoreCollisionsForTime(ignoreCollisionTime));

            // 마지막 입력 시간 갱신
            lastInputTime = Time.time;
        }

        // 아래층으로 내려가는 처리 (S키)
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(IgnoreCollisionsForTime(ignoreCollisionTime));  // 일정 시간 동안 2층, 3층 충돌 무시

            // 마지막 입력 시간 갱신
            lastInputTime = Time.time;
        }
    }

    // 공격 애니메이션을 트리거하는 함수
    public void TriggerAttack()
    {
        Debug.Log("공격 애니메이션 트리거 실행");  // 디버그 메시지 출력
        m_animator.SetTrigger("Attack");  // Attack 애니메이션 트리거 실행
    }

    // 스턴 애니메이션 실행 및 조작 제한
    public void TriggerStun()
    {
        isStunned = true;  // 스턴 상태로 설정
        m_animator.SetTrigger("Stun");  // 스턴 애니메이션 실행
    }

    // 애니메이션 이벤트를 사용하여 스턴 종료 시 호출 (애니메이션에서 이벤트 추가)
    public void EndStun()
    {
        isStunned = false;  // 스턴 상태 해제
    }

    // 캐릭터가 피해를 받을 때 호출되는 함수
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // 체력 감소
        m_animator.SetTrigger("Hurt");  // 피격 애니메이션 실행

        // 체력 이미지 업데이트
        UpdateHealthImages();

        // 체력이 0 이하일 경우 사망 처리
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 캐릭터가 사망했을 때 호출되는 함수
    void Die()
    {
        Debug.Log("캐릭터 사망");
        m_animator.SetTrigger("Death");  // 사망 애니메이션 실행
                                         // 모든 몬스터의 숫자를 숨김
        HideAllMonsterNumbers();
        Invoke("TriggerGameOverFade", 2f);

        // 2초 후 게임 오버 씬으로 전환
        Invoke("GameOver", 2f);
    }
    // 몬스터 숫자를 숨기는 함수
    void HideAllMonsterNumbers()
    {
        GoblinController[] allGoblins = FindObjectsOfType<GoblinController>();
        MushroomController[] allMushrooms = FindObjectsOfType<MushroomController>();

        foreach (var goblin in allGoblins)
        {
            goblin.HideNumber();  // 숫자 숨기기
        }

        foreach (var mushroom in allMushrooms)
        {
            mushroom.HideNumber();  // 숫자 숨기기
        }
    }

    // 게임 오버 씬으로 전환하는 함수
    void TriggerGameOverFade()
    {
        fadeManager.StartFadeOut("GameOver");  // GameOver 씬으로 페이드 아웃
    }

    // 충돌 감지: 캐릭터가 플랫폼에 착지했을 때
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))  // 플랫폼에 충돌 시
        {
            m_grounded = true;
            m_animator.SetBool("IsGrounded", true);  // 착지 시 IsGrounded를 true로 설정
        }

        // 장애물과 충돌했을 때
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1);  // 장애물과 충돌 시 1의 데미지
        }
    }

    // 일정 시간 동안 2층 및 3층과의 충돌을 무시하는 코루틴
    IEnumerator IgnoreCollisionsForTime(float duration)
    {
        // 2층과 3층과의 충돌 무시
        Physics2D.IgnoreCollision(m_collider, platformCollider2, true);
        Physics2D.IgnoreCollision(m_collider, platformCollider3, true);

        // 일정 시간 동안 대기 (충돌 무시 상태)
        yield return new WaitForSeconds(duration);

        // 일정 시간이 지나면 2층 및 3층과의 충돌 다시 활성화
        Physics2D.IgnoreCollision(m_collider, platformCollider2, false);
        Physics2D.IgnoreCollision(m_collider, platformCollider3, false);
    }
}
