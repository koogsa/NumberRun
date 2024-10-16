using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public HeroKnight player;

    void Update()
    {
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                Debug.Log("���� " + i + " �Էµ�!");

                bool isMatched = CheckInputForAllMonsters(i);

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

    bool CheckInputForAllMonsters(int inputNumber)
    {
        bool isMatched = false;

        GoblinController[] allGoblins = FindObjectsOfType<GoblinController>();
        foreach (GoblinController goblin in allGoblins)
        {
            if (goblin.CheckPlayerInput(inputNumber))
            {
                isMatched = true;
            }
        }

        MushroomController[] allMushrooms = FindObjectsOfType<MushroomController>();
        foreach (MushroomController mushroom in allMushrooms)
        {
            if (mushroom.CheckPlayerInput(inputNumber))
            {
                isMatched = true;
            }
        }

        BatController[] allBats = FindObjectsOfType<BatController>();
        foreach (BatController bat in allBats)
        {
            if (bat.CheckPlayerInput(inputNumber))
            {
                isMatched = true;
            }
        }

        SkeletonController[] allSkeletons = FindObjectsOfType<SkeletonController>();
        foreach (SkeletonController skeleton in allSkeletons)
        {
            if (skeleton.CheckPlayerInput(inputNumber))
            {
                isMatched = true;
            }
        }

        // ������ �ִ� ��쿡�� Ȯ��
        BossController boss = FindObjectOfType<BossController>();
        if (boss != null)
        {
            if (boss.CheckPlayerInput(inputNumber))  // ������ ������ �Է� ó�� �߰�
            {
                isMatched = true;
            }
        }

        return isMatched;
    }
}
