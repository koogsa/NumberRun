using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class MushroomController : MonoBehaviour
{
    private TextMeshProUGUI numberText;  // ���� �Ӹ� ���� ǥ�õ� ���� �ؽ�Ʈ
    private int assignedNumber;  // �������� �Ҵ�� ���� ����
    private Animator animator;  // ���� �ִϸ�����
    private HeroKnight player;  // �÷��̾� ����
    private bool isDead = false;  // ��� ���¸� ����

    public event Action OnMushroomDestroyed;  // ���� �ı� �̺�Ʈ

    void Start()
    {
        animator = GetComponent<Animator>();  // ���� �ִϸ����� ��������

        // �ڽ� ������Ʈ���� TextMeshProUGUI ������Ʈ�� ã��
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

    // �������� ������ ���ڸ� �Ҵ��ϴ� �Լ�
    public void AssignRandomNumber()
    {
        assignedNumber = UnityEngine.Random.Range(1, 10);
    }

    // ���� �Ӹ� ���� ���ڸ� ǥ���ϴ� �Լ�
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
            Die();  // ���ڰ� ��ġ�ϸ� ������ ����
            return true;  // ���ڰ� ��ġ�ϸ� true ��ȯ
        }
        else if (!isDead)  // ���ڰ� ��ġ���� ������ ����
        {
            player.TriggerStun();  // �÷��̾� ���� �ִϸ��̼� ����
        }

        return false;
    }

    // ���� ��� ó��
    void Die()
    {
        isDead = true;
        animator.SetTrigger("isDead");

        // �÷��̾��� ������ ����
        if (player != null)
        {
            player.IncreaseGauge();  // �÷��̾��� ������ ����
            Debug.Log("IncreaseGauge ȣ���");  // ����� �α� �߰�
        }

        // ���� �ı�
        Destroy(gameObject, 1.5f);  // ���� �ð� �� ���� �ı�
    }

    // ��� �ִϸ��̼� ��� �� ������ �ı��ϴ� �ڷ�ƾ
    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.5f);  // �ִϸ��̼� �ð��� ���� ������ ���
        OnMushroomDestroyed?.Invoke();  // �ı� �̺�Ʈ ȣ��
        Destroy(gameObject);  // ���� ������Ʈ ����
    }
}
