using UnityEngine;
using TMPro;
using System;
using System.Collections;
public class MushroomController : MonoBehaviour
{
    public TextMeshProUGUI numberText;  // 머리 위에 표시될 숫자 텍스트
    private int assignedNumber;  // 몬스터에게 할당된 랜덤 숫자
    private Animator animator;  // 애니메이터 참조
    private bool isDead = false;  // 사망 상태를 추적

    public event Action OnMushroomDestroyed;  // 버섯 몬스터 파괴 이벤트

    void Start()
    {
        animator = GetComponent<Animator>();
        AssignRandomNumber();
        UpdateNumberText();
    }

    // 랜덤 숫자를 할당하는 함수
    public void AssignRandomNumber()
    {
        assignedNumber = UnityEngine.Random.Range(1, 10);
    }

    // 머리 위에 숫자를 표시하는 함수
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
            Die();
        }
    }

    // 몬스터 사망 처리
    void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        OnMushroomDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
