using UnityEngine;
using TMPro;
using UnityEngine.UI;  // UI ��� ����� ���� �߰�
using System.Collections;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public Transform spawnPoint1F;  // 1�� ���� ��ġ
    public Transform spawnPoint2F;  // 2�� ���� ��ġ
    public float minSpeed = 10f;    // �ּ� �ӵ�
    public float maxSpeed = 15f;    // �ִ� �ӵ�
    public float acceleration = 1f;  // ���ӵ�
    private float currentSpeed;
    private Vector3 targetPosition;
    private bool isMoving = true;
    private bool isVictorious = false;
    private bool isAttackable = false;  // ���� ���� ���� ����

    private Rigidbody2D rb;
    private Animator animator;

    public TextMeshProUGUI attackText;  // ���� �ؽ�Ʈ UI
    private int correctAnswer;  // �÷��̾ ����� �� ����
    public HeroKnight player;   // �÷��̾� ����

    // ���� ü�� ���� ����
    public Slider bossHpBar; // ���� HP ��
    public int maxHealth = 10;   // ������ �ִ� ü��
    private float currentHealth;

    public float atktimeDuration = 2f;  // atktime �ִϸ��̼� ��� �ð�

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SpawnBoss();  // ���� ó�� ����

        currentHealth = maxHealth; // ���� ü���� �ִ� ü������ ����
        bossHpBar.maxValue = maxHealth; // HP ���� �ִ밪 ����
        bossHpBar.value = currentHealth; // ���� ü���� HP �ٿ� �ݿ�
    }

    // �÷��̾�� �浹 ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HeroKnight player = collision.gameObject.GetComponent<HeroKnight>();
            if (player != null)
            {
                player.TakeDamage(1);  // �÷��̾�� ������ ����
                StartCoroutine(TemporarilyIgnoreCollision(player.gameObject.GetComponent<Collider2D>()));
            }
        }
    }

    // ���� �ð� ���� �浹�� �����ϴ� �ڷ�ƾ
    private IEnumerator TemporarilyIgnoreCollision(Collider2D playerCollider)
    {
        Collider2D bossCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(bossCollider, playerCollider, true);  // �浹 ����
        yield return new WaitForSeconds(2f);  // 2�� ���� �浹 ����
        Physics2D.IgnoreCollision(bossCollider, playerCollider, false);  // �ٽ� �浹 ����
    }

    // �÷��̾ �׿��� �� ȣ��Ǵ� �Լ�
    public void OnPlayerDeath()
    {
        isVictorious = true;
        rb.velocity = Vector2.zero;  // ������ �ӵ��� 0���� ����
        animator.SetBool("isVictorious", true);  // Victory �ִϸ��̼� ����
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
           // Debug.Log("���� �ӵ�: " + currentSpeed);
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
                isMoving = false;  // �ӵ��� �ִ뿡 �����ϸ� ����
                TriggerBossStop();
            }
        }

        if (isVictorious)
        {
            rb.velocity = Vector2.zero;  // Victory ������ �� ������ ����
        }

        if (isAttackable && Input.GetKeyDown(KeyCode.Return))  // ���� Ű�� ����
        {
            CheckAnswer();  // �÷��̾ ������ ������� Ȯ��
        }
    }

    // ������ ������ ���� ����
    void SpawnBoss()
    {
        int randomFloor = Random.Range(0, 2);
        targetPosition = (randomFloor == 0) ? spawnPoint1F.position : spawnPoint2F.position;
        transform.position = new Vector3(10f, targetPosition.y, targetPosition.z);  // ������ ������ ����

        // �ӵ��� �ʱ�ȭ���� �ʰ� ���� �ӵ��� �����ϵ��� ����
        if (currentSpeed < minSpeed)
        {
            currentSpeed = minSpeed;  // ó�� ������ ���� �ּ� �ӵ��� ����
        }

        isMoving = true;
        animator.SetTrigger("isMoving");
    }


    // ������ ������ �� ���� �̺�Ʈ�� Ʈ����
    void TriggerBossStop()
    {
        Debug.Log("������ ������ϴ�. �÷��̾ ������ �� �ֽ��ϴ�.");
        StartCoroutine(PlayAtktimeThenResume());  // atktime �ִϸ��̼� ��� �� �̵� �簳
        isAttackable = true;  // ���� ���� ���·� ����
        animator.SetBool("isMoving", false);  // ������ ���߹Ƿ� �̵� �ִϸ��̼� ����
    }

    // atktime �ִϸ��̼��� �����ϰ�, ���� �ð� �Ŀ� �ٽ� �̵� ����
    IEnumerator PlayAtktimeThenResume()
    {
        // �ִϸ��̼� ����
        animator.SetTrigger("isAttackable");

        // ���� �ؽ�Ʈ�� ǥ���ϰ�
        ShowAttackText();

        // ���� �ð� ���
        yield return new WaitForSeconds(atktimeDuration);

        // ���� �ؽ�Ʈ ����
        HideAttackText();

        isAttackable = false;  // ���� ���� ���� ����

        // currentSpeed�� maxSpeed�� ����
        currentSpeed = minSpeed;  // �ӵ��� �ʱ�ȭ

        isMoving = true;

        animator.SetTrigger("isMoving");  // �̵� �ִϸ��̼� �����
    }

    // ���� �ؽ�Ʈ�� ǥ���ϰ� ������ ����
    void ShowAttackText()
    {
        if (attackText != null)
        {
            int num1 = 0, num2 = 0;  // �⺻������ �ʱ�ȭ
            correctAnswer = 0;
            string operation = "";

            // �� �ڸ� ���� ���� ������ ���� ����
            while (correctAnswer < 1 || correctAnswer > 9)
            {
                num1 = Random.Range(1, 10);  // 1~9 ������ ��
                num2 = Random.Range(1, 10);  // 1~9 ������ ��

                // �����ϰ� ������ ���� (���ϱ�, ����, ���ϱ�, ������)
                int operationIndex = Random.Range(0, 4);
                switch (operationIndex)
                {
                    case 0:  // ���ϱ�
                        correctAnswer = num1 + num2;
                        operation = "+";
                        break;
                    case 1:  // ����
                        correctAnswer = num1 - num2;
                        operation = "-";
                        break;
                    case 2:  // ���ϱ�
                        correctAnswer = num1 * num2;
                        operation = "*";
                        break;
                    case 3:  // ������ (��Ȯ�� ����������� ��츸)
                        if (num2 != 0 && num1 % num2 == 0)  // num2�� 0�� �ƴϰ� ��������� ��츸
                        {
                            correctAnswer = num1 / num2;
                            operation = "/";
                        }
                        break;
                }
            }

            // ���� �ؽ�Ʈ ����
            attackText.text = $"{num1} {operation} {num2} = ?";
            attackText.gameObject.SetActive(true);  // �ؽ�Ʈ ���̰� ��
        }
    }

    // ���� �ؽ�Ʈ�� ����� �Լ�
    void HideAttackText()
    {
        if (attackText != null)
        {
            attackText.gameObject.SetActive(false);  // �ؽ�Ʈ ����
        }
    }

    // �÷��̾ �Է��� ���� Ȯ��
    void CheckAnswer()
    {
        string playerInput = "";  
        if (int.TryParse(playerInput, out int playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                Debug.Log("����! �������� �������� ����");
                TakeDamage(10);  
            }
        }
    }
    public bool CheckPlayerInput(int inputNumber)
    {
        if (inputNumber == correctAnswer)
        {
            TakeDamage(1);  // �����̸� �������� ������
            return true;
        }
        return false;
    }

    // ������ �������� �Դ� �Լ�
    void TakeDamage(int damage)
    {
        currentHealth -= damage;  // ������ �ޱ�
        Debug.Log($"���� ü��: {currentHealth}/{maxHealth}");  // ü�� ����� �α� ���
        bossHpBar.value = currentHealth;  // HP �� ������Ʈ

        if (currentHealth <= 0)
        {
            Die();  // ü���� 0 ���ϰ� �Ǹ� ��� ó��
        }
        else
        {
            animator.SetTrigger("TakeDamage");  // ���� �ǰ� �ִϸ��̼� ����
        }
    }

    // ������ ����ϴ� �Լ�
    void Die()
    {
        Debug.Log("���� ���");
        // ��� �ִϸ��̼� ����
        animator.SetTrigger("Die");
        Destroy(gameObject);
        SceneManager.LoadScene("GameClear");
    }
}
