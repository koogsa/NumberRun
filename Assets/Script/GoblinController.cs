using UnityEngine;
using TMPro;
using System;  // Action �̺�Ʈ ����� ���� �߰�

public class GoblinController : MonoBehaviour
{
    public TextMeshProUGUI numberText;  // ��� �Ӹ� ���� ǥ�õ� ���� �ؽ�Ʈ
    private int assignedNumber;  // ����� �Ҵ�� ���� ����

    // ����� �ı��� �� ȣ��� �̺�Ʈ
    public Action OnGoblinDestroyed;

    void Start()
    {
        // ������� ������ ���� �Ҵ�
        assignedNumber = UnityEngine.Random.Range(1, 10);
        UpdateNumberText();
    }

    // ��� �Ӹ� ���� ���ڸ� ǥ���ϴ� �Լ�
    void UpdateNumberText()
    {
        if (numberText != null)
        {
            numberText.text = assignedNumber.ToString();
        }
    }

    // �÷��̾ �Է��� ���ڸ� Ȯ���ϰ� ����� ���̴� �Լ�
    public void CheckPlayerInput(int inputNumber)
    {
        if (inputNumber == assignedNumber)
        {
            Die();  // ���ڰ� ��ġ�ϸ� ��� ��� ó��
        }
    }

    // ��� ��� ó��
    void Die()
    {
        // ��� �ı� �̺�Ʈ ȣ��
        OnGoblinDestroyed?.Invoke();

        // ��� �ı�
        Destroy(gameObject);
    }
}
