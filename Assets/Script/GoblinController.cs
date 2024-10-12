using UnityEngine;
using TMPro;
using System;  // Action 이벤트 사용을 위해 추가

public class GoblinController : MonoBehaviour
{
    public TextMeshProUGUI numberText;  // 고블린 머리 위에 표시될 숫자 텍스트
    private int assignedNumber;  // 고블린에 할당된 랜덤 숫자

    // 고블린이 파괴될 때 호출될 이벤트
    public Action OnGoblinDestroyed;

    void Start()
    {
        // 고블린에게 랜덤한 숫자 할당
        assignedNumber = UnityEngine.Random.Range(1, 10);
        UpdateNumberText();
    }

    // 고블린 머리 위에 숫자를 표시하는 함수
    void UpdateNumberText()
    {
        if (numberText != null)
        {
            numberText.text = assignedNumber.ToString();
        }
    }

    // 플레이어가 입력한 숫자를 확인하고 고블린을 죽이는 함수
    public void CheckPlayerInput(int inputNumber)
    {
        if (inputNumber == assignedNumber)
        {
            Die();  // 숫자가 일치하면 고블린 사망 처리
        }
    }

    // 고블린 사망 처리
    void Die()
    {
        // 고블린 파괴 이벤트 호출
        OnGoblinDestroyed?.Invoke();

        // 고블린 파괴
        Destroy(gameObject);
    }
}
