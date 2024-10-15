using UnityEngine;
using System;
using TMPro;
using System.Collections;

public class BatController : MonoBehaviour
{
    public event Action OnMonsterDestroyed;
    public TextMeshProUGUI mathProblemText;  // 박쥐 머리 위에 표시될 수학 문제 텍스트
    private int correctAnswer;  // 수학 문제의 정답
    private Animator animator;  // 박쥐 애니메이터
    private bool isDead = false;  // 박쥐가 죽었는지 여부

    void Start()
    {
        animator = GetComponent<Animator>();
        GenerateMathProblem();  // 수학 문제 생성
        UpdateProblemText();  // 머리 위에 수학 문제 표시
    }

    // 수학 문제 생성 (덧셈 또는 뺄셈 문제) - 한 자리 수 범위 내에서 결과가 나오도록 처리
    void GenerateMathProblem()
    {
        bool isAddition = UnityEngine.Random.Range(0, 2) == 0;  // 덧셈 또는 뺄셈 결정
        int number1, number2;

        if (isAddition)
        {
            number1 = UnityEngine.Random.Range(1, 9);  // 첫 번째 숫자는 1~8까지 선택
            number2 = UnityEngine.Random.Range(1, 10 - number1);  // 두 번째 숫자는 첫 번째 숫자와 합해서 9를 넘지 않도록 설정
            correctAnswer = number1 + number2;  // 덧셈 정답
            mathProblemText.text = $"{number1} + {number2} = ?";
        }
        else
        {
            number1 = UnityEngine.Random.Range(1, 10);  // 첫 번째 숫자는 1~9까지 선택
            number2 = UnityEngine.Random.Range(0, number1);  // 두 번째 숫자는 첫 번째 숫자보다 크지 않도록 설정
            correctAnswer = number1 - number2;  // 뺄셈 정답
            mathProblemText.text = $"{number1} - {number2} = ?";
        }
    }

    // 수학 문제 텍스트를 업데이트하는 함수
    void UpdateProblemText()
    {
        if (mathProblemText != null)
        {
            mathProblemText.text = mathProblemText.text;
        }
    }

    // 플레이어가 입력한 답을 확인하는 함수
    public bool CheckPlayerInput(int inputNumber)
    {
        if (inputNumber == correctAnswer && !isDead)
        {
            Die();  // 정답일 경우 박쥐 죽임
            return true;
        }
        return false;
    }

    // 박쥐 사망 처리
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

    // 사망 애니메이션 후 박쥐 제거
    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
