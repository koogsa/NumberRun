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

                // ���, ����, ����, ���̷����� ���� �Ǵ� ���� ��� Ȯ��
                bool isMatched = CheckInputForAllMonsters(i);

                // ���� �� �ϳ��� ������ ���� ��� ����, ��� Ʋ���� ���� ��� ����
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

    // ���� ���� ��� ���, ����, ����, ���̷��濡�� �÷��̾��� �Է��� �����ϰ�, ��ġ ���θ� ��ȯ�ϴ� �Լ�
    bool CheckInputForAllMonsters(int inputNumber)
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

        // ���� ��� ���㸦 ã��
        BatController[] allBats = FindObjectsOfType<BatController>();
        foreach (BatController bat in allBats)
        {
            if (bat.CheckPlayerInput(inputNumber))  // ������ ���� ���� ��� ���Ͽ� ��ġ�ϸ�
            {
                isMatched = true;  // ��ġ�ϸ� true�� ����
            }
        }

        // ���� ��� ���̷����� ã��
        SkeletonController[] allSkeletons = FindObjectsOfType<SkeletonController>();
        foreach (SkeletonController skeleton in allSkeletons)
        {
            if (skeleton.CheckPlayerInput(inputNumber))  // ���̷����� ���� ���� ��� ���Ͽ� ��ġ�ϸ�
            {
                isMatched = true;  // ��ġ�ϸ� true�� ����
            }
        }

        return isMatched;  // �ϳ��� ���� �Ǵ� ���� ������ true ��ȯ
    }
}
