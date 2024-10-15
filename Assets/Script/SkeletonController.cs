using UnityEngine;
using System;
using TMPro;
using System.Collections;

public class SkeletonController : MonoBehaviour
{
    public event Action OnMonsterDestroyed;
    public TextMeshProUGUI mathProblemText;  // 스켈레톤 머리 위에 표시될 수학 문제 텍스트
    private int correctAnswer;  // 수학 문제의 정답
    private Animator animator;  // 스켈레톤 애니메이터
    private bool isDead = false;  // 스켈레톤이 죽었는지 여부

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

        // 1.5초 후 파괴
        Destroy(gameObject, 1.5f);

        // 몬스터가 파괴되었을 때 스폰이 가능하도록 이벤트 호출
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
