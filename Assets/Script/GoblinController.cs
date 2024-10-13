using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class GoblinController : MonoBehaviour
{
    private TextMeshProUGUI numberText;  // 고블린 머리 위에 표시될 숫자 텍스트
    private int assignedNumber;  // 고블린에게 할당된 랜덤 숫자
    private Animator animator;  // 고블린 애니메이터
    private HeroKnight player;  // 플레이어 참조
    private bool isDead = false;  // 사망 상태를 추적

    public event Action OnGoblinDestroyed;  // 고블린 파괴 이벤트

    void Start()
    {
        animator = GetComponent<Animator>();  // 고블린 애니메이터 가져오기

        // 고블린의 자식 오브젝트에서 TextMeshProUGUI 컴포넌트를 찾음
        numberText = GetComponentInChildren<TextMeshProUGUI>();
        if (numberText == null)
        {
            Debug.LogError("TextMeshProUGUI가 할당되지 않았습니다.");
        }

        // 씬 내에서 HeroKnight를 자동으로 찾아서 할당
        player = FindObjectOfType<HeroKnight>();
        if (player == null)
        {
            Debug.LogError("플레이어가 씬에 존재하지 않습니다.");
        }

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
    public bool CheckPlayerInput(int inputNumber)
    {
        if (inputNumber == assignedNumber && !isDead)
        {
            Die();  // 숫자가 일치하면 고블린을 죽임
            return true;  // 숫자가 일치하면 true 반환
        }
        return false;  // 숫자가 일치하지 않으면 false 반환
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
        yield return new WaitForSeconds(1.5f);  // 애니메이션 시간이 끝날 때까지 대기
        OnGoblinDestroyed?.Invoke();  // 파괴 이벤트 호출
        Destroy(gameObject);  // 고블린 오브젝트 삭제
    }
}
