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
        if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetTrigger("Jump");  // 점프 애니메이션 실행
            m_grounded = false;
            m_animator.SetBool("IsGrounded", false);  // 점프 시 IsGrounded를 false로 설정
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
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
}
