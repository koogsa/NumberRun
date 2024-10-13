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
                Debug.Log("���� " + i + " �Էµ�!");

                // ����� ������ ���ڸ� Ȯ��
                bool isMatched = CheckInputForAllGoblinsAndMushrooms(i);

                // ����� ���� �� �ϳ��� ������ ���� ��� ����, ��� Ʋ���� ���� ��� ����
                if (isMatched)
                {
                    player.TriggerAttack();  // ���� �ִϸ��̼� ����
                }
                else
                {
                    player.TriggerStun();  // ���� �ִϸ��̼� ����
                }
            }
        }
    }

    // ���� ���� ��� ����� �������� �÷��̾��� �Է��� �����ϰ�, ��ġ ���θ� ��ȯ�ϴ� �Լ�
    bool CheckInputForAllGoblinsAndMushrooms(int inputNumber)
    {
        bool isMatched = false;  // �ϳ��� ���ڰ� ������ true�� ����

        // ���� ��� ����� ã��
        GoblinController[] allGoblins = FindObjectsOfType<GoblinController>();
        foreach (GoblinController goblin in allGoblins)
        {
            if (goblin.CheckPlayerInput(inputNumber))  // ����� ���ڿ� ���Ͽ� ��ġ�ϸ�
            {
                isMatched = true;  // ��ġ�ϸ� true�� ����
            }
        }

        // ���� ��� ������ ã��
        MushroomController[] allMushrooms = FindObjectsOfType<MushroomController>();
        foreach (MushroomController mushroom in allMushrooms)
        {
            if (mushroom.CheckPlayerInput(inputNumber))  // ������ ���ڿ� ���Ͽ� ��ġ�ϸ�
            {
                isMatched = true;  // ��ġ�ϸ� true�� ����
            }
        }

        return isMatched;  // �ϳ��� ���ڰ� ������ true ��ȯ
    }
}
