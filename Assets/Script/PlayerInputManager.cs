using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public HeroKnight player;  // �÷��̾� ĳ���� ���� (Inspector���� �Ҵ�)
    void Update()
    {
        // Ű���� ���� �Է� (1~9) ����
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))  // ���� Ű�� ������ ��
            {
                CheckInputForAllGoblins(i);  // ���� ���ڿ� ����� ���ڸ� ��
                Debug.Log("���� " + i + " �Էµ�!");
                player.TriggerAttack();  // �÷��̾� ���� �ִϸ��̼� ����

            }
        }
    }

    // ���� ���� ��� ������� �÷��̾��� �Է��� �����ϴ� �Լ�
    void CheckInputForAllGoblins(int inputNumber)
    {
        GoblinController[] allGoblins = FindObjectsOfType<GoblinController>();  // ���� ��� ����� ã��

        // �� ������� �Էµ� ���ڸ� ����
        foreach (GoblinController goblin in allGoblins)
        {
            goblin.CheckPlayerInput(inputNumber);  // ����� ���ڿ� ��
        }
    }
}
