using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public HeroKnight player;  // �÷��̾� ĳ���� ����

    void Update()
    {
        // 1���� 9���� ���� Ű �Է��� ����
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))  // �ش� ���� Ű�� ������ ��
            {
                Debug.Log("���� " + i + " �Էµ�!");  // �Էµ� ���ڸ� �α׿� ���

                // ��� ������ �Է��� Ȯ��
                bool isMatched = CheckInputForAllMonsters(i);

                // �Է��� ������ ����, Ʋ���� ���� ��� ����
                if (isMatched)
                {
                    player.TriggerAttack();  // �÷��̾� ���� �ִϸ��̼� ����
                }
                else
                {
                    player.TriggerStun();  // �÷��̾� ���� �ִϸ��̼� ����
                }
            }
        }
    }

    // ���� �� ��� ���Ϳ��� �÷��̾� �Է��� �����ϰ�, ��ġ ���θ� ��ȯ�ϴ� �Լ�
    bool CheckInputForAllMonsters(int inputNumber)
    {
        bool isMatched = false;  // �ϳ��� ���ڰ� ������ true�� ����

        // ��� ����� ã�Ƽ� �Է� Ȯ��
        GoblinController[] allGoblins = FindObjectsOfType<GoblinController>();
        foreach (GoblinController goblin in allGoblins)
        {
            if (goblin.CheckPlayerInput(inputNumber))  // ����� �Է°� ��
            {
                isMatched = true;  // ��ġ�ϸ� true�� ����
            }
        }

        // ��� ������ ã�Ƽ� �Է� Ȯ��
        MushroomController[] allMushrooms = FindObjectsOfType<MushroomController>();
        foreach (MushroomController mushroom in allMushrooms)
        {
            if (mushroom.CheckPlayerInput(inputNumber))  // ������ �Է°� ��
            {
                isMatched = true;  // ��ġ�ϸ� true�� ����
            }
        }

        // ��� ���㸦 ã�Ƽ� �Է� Ȯ��
        BatController[] allBats = FindObjectsOfType<BatController>();
        foreach (BatController bat in allBats)
        {
            if (bat.CheckPlayerInput(inputNumber))  // ������ �Է°� ��
            {
                isMatched = true;  // ��ġ�ϸ� true�� ����
            }
        }

        // ��� ���̷����� ã�Ƽ� �Է� Ȯ��
        SkeletonController[] allSkeletons = FindObjectsOfType<SkeletonController>();
        foreach (SkeletonController skeleton in allSkeletons)
        {
            if (skeleton.CheckPlayerInput(inputNumber))  // ���̷����� �Է°� ��
            {
                isMatched = true;  // ��ġ�ϸ� true�� ����
            }
        }

        // ������ �ִ� ��쿡�� �Է��� Ȯ��
        BossController boss = FindObjectOfType<BossController>();
        if (boss != null)
        {
            if (boss.CheckPlayerInput(inputNumber))  // ������ �Է°� ��
            {
                isMatched = true;  // ��ġ�ϸ� true�� ����
            }
        }

        return isMatched;  // �ϳ��� ������ true ��ȯ
    }
}
