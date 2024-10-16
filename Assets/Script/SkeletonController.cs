using UnityEngine;
using System;
using TMPro;
using System.Collections;

public class SkeletonController : MonoBehaviour
{
    private HeroKnight player;  // �÷��̾� ����
    public event Action OnMonsterDestroyed;  // ���Ͱ� �ı��Ǿ��� �� ȣ��Ǵ� �̺�Ʈ
    public TextMeshProUGUI mathProblemText;  // ���̷��� �Ӹ� ���� ǥ�õ� ���� ���� �ؽ�Ʈ
    private int correctAnswer;  // ���� ������ ����
    private Animator animator;  // ���̷����� �ִϸ�����
    private bool isDead = false;  // ���̷����� �׾����� ���θ� ����

    void Start()
    {
        player = FindObjectOfType<HeroKnight>();  // ������ HeroKnight �÷��̾ ã��
        animator = GetComponent<Animator>();  // ���̷��� �ִϸ����� ��������
        GenerateMathProblem();  // ���� ���� ����
        UpdateProblemText();  // ���� �ؽ�Ʈ ������Ʈ
    }

    // ���� ������ �����ϴ� �Լ�
    void GenerateMathProblem()
    {
        bool isAddition = UnityEngine.Random.Range(0, 2) == 0;  // ���� �������� ���� �������� �������� ����
        int number1, number2;

        if (isAddition)  // ���� ������ ���
        {
            number1 = UnityEngine.Random.Range(1, 9);  // ù ��° ���� ���� (1~9)
            number2 = UnityEngine.Random.Range(1, 10 - number1);  // �� ��° ���� ���� (������ 10�� ���� �ʵ���)
            correctAnswer = number1 + number2;  // ���� ���
            mathProblemText.text = $"{number1} + {number2} = ?";  // ���� �ؽ�Ʈ ����
        }
        else  // ���� ������ ���
        {
            number1 = UnityEngine.Random.Range(1, 10);  // ù ��° ���� ���� (1~10)
            number2 = UnityEngine.Random.Range(0, number1);  // �� ��° ���� ���� (ù ��° ���ں��� �۰ų� ���ƾ� ��)
            correctAnswer = number1 - number2;  // ���� ���
            mathProblemText.text = $"{number1} - {number2} = ?";  // ���� �ؽ�Ʈ ����
        }
    }

    // ���� �ؽ�Ʈ�� ������Ʈ�ϴ� �Լ�
    void UpdateProblemText()
    {
        if (mathProblemText != null)
        {
            mathProblemText.text = mathProblemText.text;  // ���� �ؽ�Ʈ ������Ʈ
        }
    }

    // �÷��̾ �Է��� ���� �´��� Ȯ���ϴ� �Լ�
    public bool CheckPlayerInput(int inputNumber)
    {
        if (inputNumber == correctAnswer && !isDead)  // �Է��� �����̰� ���̷����� ���� ����ִٸ�
        {
            Die();  // ���̷��� ��� ó��
            return true;  // �����̸� true ��ȯ
        }
        return false;  // ������ �ƴϸ� false ��ȯ
    }

    // ���̷��� ��� ó��
    public void Die()
    {
        if (!isDead)  // ���̷����� ���� ���� �ʾ��� ���� ó��
        {
            isDead = true;  // ���� ���·� ����
            animator.SetTrigger("isDead");  // ��� �ִϸ��̼� Ʈ����

            // �÷��̾��� ������ ���� ó��
            if (player != null)
            {
                player.IncreaseGauge();  // �÷��̾� ������ ����
            }

            StartCoroutine(DeathRoutine());  // ���� �ð��� ���� �� ���̷��� �ı�

            // ���� �ı� �̺�Ʈ ȣ��
            if (OnMonsterDestroyed != null)
            {
                OnMonsterDestroyed.Invoke();  // OnMonsterDestroyed �̺�Ʈ ȣ��
            }
        }
    }

    // ��� �ִϸ��̼��� �Ϸ�� �� ���̷����� �ı��ϴ� �ڷ�ƾ
    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.5f);  // 1.5�� ��� (�ִϸ��̼� �ð�)
        Destroy(gameObject);  // ���̷��� ������Ʈ �ı�
    }
}
