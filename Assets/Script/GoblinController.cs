using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class GoblinController : MonoBehaviour
{
    public TextMeshProUGUI numberText;  // ��� �Ӹ� ���� ǥ�õ� ���� �ؽ�Ʈ
    private int assignedNumber;  // ������� �Ҵ�� ���� ����
    private Animator animator;  // ��� �ִϸ�����
    public Animator playerAnimator;  // �÷��̾��� �ִϸ�����
    private bool isDead = false;  // ��� ���¸� ����

    public event Action OnGoblinDestroyed;  // ��� �ı� �̺�Ʈ

    void Start()
    {
        animator = GetComponent<Animator>();  // ��� �ִϸ����� ��������
        AssignRandomNumber();
        UpdateNumberText();
    }

    // ������� ������ ���ڸ� �Ҵ��ϴ� �Լ�
    public void AssignRandomNumber()
    {
        assignedNumber = UnityEngine.Random.Range(1, 10);
    }

    // ��� �Ӹ� ���� ���ڸ� ǥ���ϴ� �Լ�
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
            Debug.Log("���ڰ� ��ġ�մϴ�!");
            TriggerPlayerAttack();  // �÷��̾� ���� �ִϸ��̼� ����
            Die();  // ���ڰ� ��ġ�ϰ� ���� ���� ���� ���¸� ��� ó��
        }
    }

    // �÷��̾ �����ϴ� �ִϸ��̼��� �����ϴ� �Լ�
    void TriggerPlayerAttack()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Attack");  // Attack �ִϸ��̼� Ʈ���� ����
        }
    }

    // ��� ��� ó��
    void Die()
    {
        isDead = true;  // ��� ���·� ����
        animator.SetBool("isDead", true);  // ��� �ִϸ��̼� ���

        // �ִϸ��̼� ��� �� �ı�
        StartCoroutine(DeathRoutine());
    }

    // ��� �ִϸ��̼� ��� �� ����� �ı��ϴ� �ڷ�ƾ
    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.5f);  // �ִϸ��̼� �ð��� ���� ������ ��� (�ð��� �ִϸ��̼� ���̿� ���� ����)
        OnGoblinDestroyed?.Invoke();  // �ı� �̺�Ʈ ȣ��
        Destroy(gameObject);  // ��� ������Ʈ ����
    }
}
