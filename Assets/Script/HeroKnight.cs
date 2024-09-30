using UnityEngine;

public class HeroKnight : MonoBehaviour
{
    [SerializeField] float m_jumpForce = 7.5f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private bool m_grounded = false;

    // 초기화
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
    }

    // 매 프레임마다 호출
    void Update()
    {
        // 점프 처리
        if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetTrigger("Jump");  // 점프 애니메이션 실행
            m_grounded = false;
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        }

        // 피격 애니메이션
        if (Input.GetKeyDown("q"))
        {
            m_animator.SetTrigger("Hurt");
        }

        // 죽음 애니메이션
        if (Input.GetKeyDown("e"))
        {
            m_animator.SetTrigger("Death");
        }

        // 공격 모션 (마우스 왼쪽 클릭)
        if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Attack1");
        }
    }
    // 충돌 감지: 캐릭터가 플랫폼에 착지했을 때
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))  // 플랫폼에 충돌 시
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", true);
        }
    }
}
