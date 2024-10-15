using UnityEngine;
using System;
using TMPro;
using System.Collections;

public class BatController : MonoBehaviour
{
    public event Action OnMonsterDestroyed;
    public TextMeshProUGUI mathProblemText;  // ���� �Ӹ� ���� ǥ�õ� ���� ���� �ؽ�Ʈ
    private int correctAnswer;  // ���� ������ ����
    private Animator animator;  // ���� �ִϸ�����
    private bool isDead = false;  // ���㰡 �׾����� ����

    void Start()
    {
        animator = GetComponent<Animator>();
        GenerateMathProblem();  // ���� ���� ����
        UpdateProblemText();  // �Ӹ� ���� ���� ���� ǥ��
    }

    // ���� ���� ���� (���� �Ǵ� ���� ����) - �� �ڸ� �� ���� ������ ����� �������� ó��
    void GenerateMathProblem()
    {
        bool isAddition = UnityEngine.Random.Range(0, 2) == 0;  // ���� �Ǵ� ���� ����
        int number1, number2;

        if (isAddition)
        {
            number1 = UnityEngine.Random.Range(1, 9);  // ù ��° ���ڴ� 1~8���� ����
            number2 = UnityEngine.Random.Range(1, 10 - number1);  // �� ��° ���ڴ� ù ��° ���ڿ� ���ؼ� 9�� ���� �ʵ��� ����
            correctAnswer = number1 + number2;  // ���� ����
            mathProblemText.text = $"{number1} + {number2} = ?";
        }
        else
        {
            number1 = UnityEngine.Random.Range(1, 10);  // ù ��° ���ڴ� 1~9���� ����
            number2 = UnityEngine.Random.Range(0, number1);  // �� ��° ���ڴ� ù ��° ���ں��� ũ�� �ʵ��� ����
            correctAnswer = number1 - number2;  // ���� ����
            mathProblemText.text = $"{number1} - {number2} = ?";
        }
    }

    // ���� ���� �ؽ�Ʈ�� ������Ʈ�ϴ� �Լ�
    void UpdateProblemText()
    {
        if (mathProblemText != null)
        {
            mathProblemText.text = mathProblemText.text;
        }
    }

    // �÷��̾ �Է��� ���� Ȯ���ϴ� �Լ�
    public bool CheckPlayerInput(int inputNumber)
    {
        if (inputNumber == correctAnswer && !isDead)
        {
            Die();  // ������ ��� ���� ����
            return true;
        }
        return false;
    }

    // ���� ��� ó��
    void Die()
    {
        isDead = true;
        animator.SetTrigger("isDead");

        // 1.5�� �� �ı�
        Destroy(gameObject, 1.5f);

        // ���Ͱ� �ı��Ǿ��� �� ������ �����ϵ��� �̺�Ʈ ȣ��
        if (OnMonsterDestroyed != null)
        {
            OnMonsterDestroyed();
        }
    }

    // ��� �ִϸ��̼� �� ���� ����
    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
