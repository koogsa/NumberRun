using UnityEngine;
using TMPro;
using System;
using System.Collections;
public class MushroomController : MonoBehaviour
{
    public TextMeshProUGUI numberText;  // �Ӹ� ���� ǥ�õ� ���� �ؽ�Ʈ
    private int assignedNumber;  // ���Ϳ��� �Ҵ�� ���� ����
    private Animator animator;  // �ִϸ����� ����
    private bool isDead = false;  // ��� ���¸� ����

    public event Action OnMushroomDestroyed;  // ���� ���� �ı� �̺�Ʈ

    void Start()
    {
        animator = GetComponent<Animator>();
        AssignRandomNumber();
        UpdateNumberText();
    }

    // ���� ���ڸ� �Ҵ��ϴ� �Լ�
    public void AssignRandomNumber()
    {
        assignedNumber = UnityEngine.Random.Range(1, 10);
    }

    // �Ӹ� ���� ���ڸ� ǥ���ϴ� �Լ�
    void UpdateNumberText()
    {
        if (numberText != null)
        {
            numberText.text = assignedNumber.ToString();
        }
    }

    // �÷��̾ �Է��� ���ڸ� Ȯ���ϴ� �Լ�
    public void CheckPlayerInput(int inputNumber)
    {
        if (inputNumber == assignedNumber && !isDead)
        {
            Die();
        }
    }

    // ���� ��� ó��
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
