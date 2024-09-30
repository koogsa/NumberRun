using UnityEngine;

public class HeroKnight : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float floorHeight = 3.0f; // 층 높이

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private bool m_grounded = false;
    private float m_delayToIdle = 0.0f;

    // 초기화
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
    }

    // 매 프레임마다 호출
    void Update()
    {
        // -- 캐릭터 이동 처리 --
        float inputX = Input.GetAxis("Horizontal");

        // 캐릭터가 걷는 애니메이션 실행
        if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);  // 걷는 모션 실행

            // 캐릭터 이동
            transform.position += new Vector3(inputX * m_speed * Time.deltaTime, 0, 0);
        }
        else
        {
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
            {
                m_animator.SetInteger("AnimState", 0);  // 대기 모션 실행
            }
        }

        // 점프 처리
        if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetTrigger("Jump");  // 점프 애니메이션 실행
            m_grounded = false;
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        }

        // 층 이동 - 위층으로 점프 (W키)와 아래층으로 이동 (S키)
        if (Input.GetKeyDown(KeyCode.W) && m_grounded)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + floorHeight);
        }
        if (Input.GetKeyDown(KeyCode.S) && m_grounded)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - floorHeight);
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
