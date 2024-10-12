using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class GoblinController : MonoBehaviour
{
    public TextMeshProUGUI numberText;  // 고블린 머리 위에 표시될 숫자 텍스트
    private int assignedNumber;  // 고블린에게 할당된 랜덤 숫자
    private Animator animator;  // 애니메이터 참조
    private bool isDead = false;  // 사망 상태를 추적

    public event Action OnGoblinDestroyed;  // 고블린 파괴 이벤트

    void Start()
    {
        animator = GetComponent<Animator>();  // 애니메이터 가져오기
        AssignRandomNumber();
        UpdateNumberText();
    }

    // 고블린에게 랜덤한 숫자를 할당하는 함수
    public void AssignRandomNumber()
    {
        assignedNumber = UnityEngine.Random.Range(1, 10);
    }

    // 고블린 머리 위에 숫자를 표시하는 함수
    void UpdateNumberText()
    {
        if (numberText != null)
        {
            numberText.text = assignedNumber.ToString();
        }
    }

    // 플레이어가 입력한 숫자를 확인하는 함수
    public void CheckPlayerInput(int inputNumber)
    {
        if (inputNumber == assignedNumber && !isDead)
        {
            Die();  // 숫자가 일치하고 아직 죽지 않은 상태면 사망 처리
        }
    }

    // 고블린 사망 처리
    void Die()
    {
        isDead = true;  // 사망 상태로 변경
        animator.SetBool("isDead", true);  // 사망 애니메이션 재생

        // 애니메이션 재생 후 파괴
        StartCoroutine(DeathRoutine());
    }

    // 사망 애니메이션 재생 후 고블린을 파괴하는 코루틴
    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.5f);  // 애니메이션 시간이 끝날 때까지 대기 (시간은 애니메이션 길이에 맞춰 조정)
        OnGoblinDestroyed?.Invoke();  // 파괴 이벤트 호출
        Destroy(gameObject);  // 고블린 오브젝트 삭제
    }
}
