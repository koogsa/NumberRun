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
    private MonsterSpawner monsterSpawner;  // MonsterSpawner ����

    public event Action OnGoblinDestroyed;  // ��� �ı� �̺�Ʈ

    void Start()
    {
        animator = GetComponent<Animator>();  // ��� �ִϸ����� ��������
        numberText = GetComponentInChildren<TextMeshProUGUI>(); // ����� �ڽ� ������Ʈ���� TextMeshProUGUI ������Ʈ�� ã��
        AssignRandomNumber(); 
        UpdateNumberText();

        // �� ������ HeroKnight�� �ڵ����� ã�Ƽ� �Ҵ�
        player = FindObjectOfType<HeroKnight>();
        if (player == null)
        {
            Debug.LogError("�÷��̾ ���� �������� �ʽ��ϴ�.");
        }
        // MonsterSpawner�� ã��
        monsterSpawner = FindObjectOfType<MonsterSpawner>();
        if (monsterSpawner == null)
        {
            Debug.LogError("MonsterSpawner�� ã�� �� �����ϴ�!");
        }


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
        isDead = true;
        animator.SetTrigger("isDead");

        // �÷��̾��� ������ ����
        if (player != null)
        {
            player.IncreaseGauge();
        }

        // ���� �ı�
        Destroy(gameObject, 1.5f);

        // MonsterSpawner�� ��� �ı��� �˸�
        if (monsterSpawner != null)
        {
            monsterSpawner.OnMonsterDestroyed("Goblin");
        }
    }


    // ��� �ִϸ��̼� ��� �� ����� �ı��ϴ� �ڷ�ƾ
    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.5f);  // �ִϸ��̼� �ð��� ���� ������ ���
        OnGoblinDestroyed?.Invoke();  // �ı� �̺�Ʈ ȣ��
        Destroy(gameObject);  // ��� ������Ʈ ����
    }
}
