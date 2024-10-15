using UnityEngine;
using System;
using TMPro;
using System.Collections;

public class SkeletonController : MonoBehaviour
{
    public event Action OnMonsterDestroyed;
    public TextMeshProUGUI mathProblemText;  // ���̷��� �Ӹ� ���� ǥ�õ� ���� ���� �ؽ�Ʈ
    private int correctAnswer;  // ���� ������ ����
    private Animator animator;  // ���̷��� �ִϸ�����
    private bool isDead = false;  // ���̷����� �׾����� ����

    void Start()
    {
        animator = GetComponent<Animator>();
        GenerateMathProblem();
        UpdateProblemText();
    }

    void GenerateMathProblem()
    {
        bool isAddition = UnityEngine.Random.Range(0, 2) == 0;
        int number1, number2;

        if (isAddition)
        {
            number1 = UnityEngine.Random.Range(1, 9);
            number2 = UnityEngine.Random.Range(1, 10 - number1);
            correctAnswer = number1 + number2;
            mathProblemText.text = $"{number1} + {number2} = ?";
        }
        else
        {
            number1 = UnityEngine.Random.Range(1, 10);
            number2 = UnityEngine.Random.Range(0, number1);
            correctAnswer = number1 - number2;
            mathProblemText.text = $"{number1} - {number2} = ?";
        }
    }

    void UpdateProblemText()
    {
        if (mathProblemText != null)
        {
            mathProblemText.text = mathProblemText.text;
        }
    }

    public bool CheckPlayerInput(int inputNumber)
    {
        if (inputNumber == correctAnswer && !isDead)
        {
            Die();
            return true;
        }
        return false;
    }

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

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
