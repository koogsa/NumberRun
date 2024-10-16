using UnityEngine;
using TMPro;
using System.Collections;

public class BossController : MonoBehaviour
{
    public Transform spawnPoint1F;
    public Transform spawnPoint2F;
    public float minSpeed = 10f;
    public float maxSpeed = 20f;
    public float acceleration = 1f;
    private float currentSpeed;
    private Vector3 targetPosition;
    private bool isMoving = true;
    private bool isVictorious = false;
    private bool isAttackable = false;

    private Rigidbody2D rb;
    private Animator animator;

    public TextMeshProUGUI attackText;
    private int correctAnswer;
    public HeroKnight player;

    public float atktimeDuration = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SpawnBoss();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HeroKnight player = collision.gameObject.GetComponent<HeroKnight>();
            if (player != null)
            {
                player.TakeDamage(1);
                StartCoroutine(TemporarilyIgnoreCollision(player.gameObject.GetComponent<Collider2D>()));
            }
        }
    }

    private IEnumerator TemporarilyIgnoreCollision(Collider2D playerCollider)
    {
        Collider2D bossCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(bossCollider, playerCollider, true);
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreCollision(bossCollider, playerCollider, false);
    }

    public void OnPlayerDeath()
    {
        isVictorious = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("isVictorious", true);
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
                isMoving = false;
                TriggerBossStop();
            }
        }

        if (isVictorious)
        {
            rb.velocity = Vector2.zero;
        }

        if (isAttackable && Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer();
        }
    }

    void SpawnBoss()
    {
        int randomFloor = Random.Range(0, 2);
        targetPosition = (randomFloor == 0) ? spawnPoint1F.position : spawnPoint2F.position;
        transform.position = new Vector3(10f, targetPosition.y, targetPosition.z);

        if (currentSpeed < minSpeed)
        {
            currentSpeed = minSpeed;
        }

        isMoving = true;
        animator.SetBool("isMoving", true);  // 이동 애니메이션 실행
    }

    void TriggerBossStop()
    {
        Debug.Log("보스가 멈췄습니다. 플레이어가 공격할 수 있습니다.");
        StartCoroutine(PlayAtktimeThenResume());
        isAttackable = true;
        animator.SetBool("isMoving", false);
    }

    IEnumerator PlayAtktimeThenResume()
    {
        animator.SetTrigger("isAttackable");  // 트리거 사용

        yield return new WaitForSeconds(atktimeDuration);

        ShowAttackText();
        isAttackable = false;

        currentSpeed = minSpeed;

        isMoving = true;

        animator.SetBool("isMoving", true);
    }

    void ShowAttackText()
    {
        if (attackText != null)
        {
            int num1 = Random.Range(1, 10);
            int num2 = Random.Range(1, 10);
            correctAnswer = num1 + num2;
            attackText.text = $"{num1} + {num2} = ?";
            attackText.gameObject.SetActive(true);
        }
    }

    void CheckAnswer()
    {
        string playerInput = "";
        if (int.TryParse(playerInput, out int playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                Debug.Log("정답! 보스에게 데미지를 입힘");
                TakeDamage();
            }
            else
            {
                Debug.Log("오답! 다시 시도하세요.");
            }
        }
    }

    void TakeDamage()
    {
        animator.SetTrigger("TakeDamage");
        attackText.gameObject.SetActive(false);
        isAttackable = false;
        currentSpeed = minSpeed;
        isMoving = true;

        animator.SetBool("isMoving", true);
    }
}
