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
                Debug.Log("숫자 " + i + " 입력됨!");

                bool isMatched = CheckInputForAllMonsters(i);

                if (isMatched)
                {
                    player.TriggerAttack();  // 공격 애니메이션 실행
                }
                else
                {
                    player.TriggerStun();  // 스턴 애니메이션 실행
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

        // 보스가 있는 경우에도 확인
        BossController boss = FindObjectOfType<BossController>();
        if (boss != null)
        {
            if (boss.CheckPlayerInput(inputNumber))  // 보스와 연관된 입력 처리 추가
            {
                isMatched = true;
            }
        }

        return isMatched;
    }
}
