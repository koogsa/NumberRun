using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour
{
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float ignoreCollisionTime = 0.5f;  // 충돌을 무시할 시간 (초)

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private bool m_grounded = false;
    private Collider2D m_collider;  // 캐릭터의 충돌체
    public Collider2D platformCollider2;  // 2층의 충돌체
    public Collider2D platformCollider3;  // 3층의 충돌체

    // 초기화
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();  // 캐릭터의 충돌체 가져오기

        // platformCollider2 또는 platformCollider3이 할당되지 않은 경우 오류 메시지 출력
        if (platformCollider2 == null || platformCollider3 == null)
        {
            Debug.LogError("2층 또는 3층의 Collider가 Inspector에 할당되지 않았습니다.");
        }
    }

    // 매 프레임마다 호출
    void Update()
    {
        // 점프 처리 (space 키)
        if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetTrigger("Jump");  // 점프 애니메이션 실행
            m_grounded = false;
            m_animator.SetBool("IsGrounded", false);  // 점프 시 IsGrounded를 false로 설정
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);

            // 점프 중에도 2층 및 3층과의 충돌을 일정 시간 동안 무시
            StartCoroutine(IgnoreCollisionsForTime(ignoreCollisionTime));
        }

        // 아래층으로 내려가는 처리 (S키)
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(IgnoreCollisionsForTime(ignoreCollisionTime));  // 일정 시간 동안 2층, 3층 충돌 무시
        }
    }

    // 충돌 감지: 캐릭터가 플랫폼에 착지했을 때
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))  // 플랫폼에 충돌 시
        {
            m_grounded = true;
            m_animator.SetBool("IsGrounded", true);  // 착지 시 IsGrounded를 true로 설정
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
