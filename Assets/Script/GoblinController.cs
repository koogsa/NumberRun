using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class GoblinController : MonoBehaviour
{
    private TextMeshProUGUI numberText;  // ��� �Ӹ� ���� ǥ�õ� ���� �ؽ�Ʈ
    private int assignedNumber;  // ������� �Ҵ�� ���� ����
    private Animator animator;  // ��� �ִϸ�����
    private HeroKnight player;  // �÷��̾� ����
    private bool isDead = false;  // ��� ���¸� ����

    public event Action OnGoblinDestroyed;  // ��� �ı� �̺�Ʈ

    void Start()
    {
        animator = GetComponent<Animator>();  // ��� �ִϸ����� ��������

        // ����� �ڽ� ������Ʈ���� TextMeshProUGUI ������Ʈ�� ã��
        numberText = GetComponentInChildren<TextMeshProUGUI>();
        if (numberText == null)
        {
            Debug.LogError("TextMeshProUGUI�� �Ҵ���� �ʾҽ��ϴ�.");
        }

        // �� ������ HeroKnight�� �ڵ����� ã�Ƽ� �Ҵ�
        player = FindObjectOfType<HeroKnight>();
        if (player == null)
        {
            Debug.LogError("�÷��̾ ���� �������� �ʽ��ϴ�.");
        }

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
    public bool CheckPlayerInput(int inputNumber)
    {
        if (inputNumber == assignedNumber && !isDead)
        {
            Die();  // ���ڰ� ��ġ�ϸ� ����� ����
            return true;  // ���ڰ� ��ġ�ϸ� true ��ȯ
        }
        return false;  // ���ڰ� ��ġ���� ������ false ��ȯ
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
        yield return new WaitForSeconds(1.5f);  // �ִϸ��̼� �ð��� ���� ������ ���
        OnGoblinDestroyed?.Invoke();  // �ı� �̺�Ʈ ȣ��
        Destroy(gameObject);  // ��� ������Ʈ ����
    }
}
