using UnityEngine;
using System;
using TMPro;
using System.Collections;

public class SkeletonController : MonoBehaviour
{
    private HeroKnight player;  // 플레이어 참조
    public event Action OnMonsterDestroyed;  // 몬스터가 파괴되었을 때 호출되는 이벤트
    public TextMeshProUGUI mathProblemText;  // 스켈레톤 머리 위에 표시될 수학 문제 텍스트
    private int correctAnswer;  // 수학 문제의 정답
    private Animator animator;  // 스켈레톤의 애니메이터
    private bool isDead = false;  // 스켈레톤이 죽었는지 여부를 추적

    void Start()
    {
        player = FindObjectOfType<HeroKnight>();  // 씬에서 HeroKnight 플레이어를 찾음
        animator = GetComponent<Animator>();  // 스켈레톤 애니메이터 가져오기
        GenerateMathProblem();  // 수학 문제 생성
        UpdateProblemText();  // 문제 텍스트 업데이트
    }

    // 수학 문제를 생성하는 함수
    void GenerateMathProblem()
    {
        bool isAddition = UnityEngine.Random.Range(0, 2) == 0;  // 덧셈 문제인지 뺄셈 문제인지 랜덤으로 결정
        int number1, number2;

        if (isAddition)  // 덧셈 문제인 경우
        {
            number1 = UnityEngine.Random.Range(1, 9);  // 첫 번째 숫자 설정 (1~9)
            number2 = UnityEngine.Random.Range(1, 10 - number1);  // 두 번째 숫자 설정 (정답이 10을 넘지 않도록)
            correctAnswer = number1 + number2;  // 정답 계산
            mathProblemText.text = $"{number1} + {number2} = ?";  // 문제 텍스트 설정
        }
        else  // 뺄셈 문제인 경우
        {
            number1 = UnityEngine.Random.Range(1, 10);  // 첫 번째 숫자 설정 (1~10)
            number2 = UnityEngine.Random.Range(0, number1);  // 두 번째 숫자 설정 (첫 번째 숫자보다 작거나 같아야 함)
            correctAnswer = number1 - number2;  // 정답 계산
            mathProblemText.text = $"{number1} - {number2} = ?";  // 문제 텍스트 설정
        }
    }

    // 문제 텍스트를 업데이트하는 함수
    void UpdateProblemText()
    {
        if (mathProblemText != null)
        {
            mathProblemText.text = mathProblemText.text;  // 문제 텍스트 업데이트
        }
    }

    // 플레이어가 입력한 답이 맞는지 확인하는 함수
    public bool CheckPlayerInput(int inputNumber)
    {
        if (inputNumber == correctAnswer && !isDead)  // 입력이 정답이고 스켈레톤이 아직 살아있다면
        {
            Die();  // 스켈레톤 사망 처리
            return true;  // 정답이면 true 반환
        }
        return false;  // 정답이 아니면 false 반환
    }

    // 스켈레톤 사망 처리
    public void Die()
    {
        if (!isDead)  // 스켈레톤이 아직 죽지 않았을 때만 처리
        {
            isDead = true;  // 죽은 상태로 설정
            animator.SetTrigger("isDead");  // 사망 애니메이션 트리거

            // 플레이어의 게이지 증가 처리
            if (player != null)
            {
                player.IncreaseGauge();  // 플레이어 게이지 증가
            }

            StartCoroutine(DeathRoutine());  // 일정 시간이 지난 후 스켈레톤 파괴

            // 몬스터 파괴 이벤트 호출
            if (OnMonsterDestroyed != null)
            {
                OnMonsterDestroyed.Invoke();  // OnMonsterDestroyed 이벤트 호출
            }
        }
    }

    // 사망 애니메이션이 완료된 후 스켈레톤을 파괴하는 코루틴
    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.5f);  // 1.5초 대기 (애니메이션 시간)
        Destroy(gameObject);  // 스켈레톤 오브젝트 파괴
    }
}
