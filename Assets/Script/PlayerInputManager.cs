using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public HeroKnight player;  // 플레이어 캐릭터 참조

    void Update()
    {
        // 1부터 9까지 숫자 키 입력을 감지
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))  // 해당 숫자 키가 눌렸을 때
            {
                Debug.Log("숫자 " + i + " 입력됨!");  // 입력된 숫자를 로그에 출력

                // 모든 몬스터의 입력을 확인
                bool isMatched = CheckInputForAllMonsters(i);

                // 입력이 맞으면 공격, 틀리면 스턴 모션 실행
                if (isMatched)
                {
                    player.TriggerAttack();  // 플레이어 공격 애니메이션 실행
                }
                else
                {
                    player.TriggerStun();  // 플레이어 스턴 애니메이션 실행
                }
            }
        }
    }

    // 게임 내 모든 몬스터에게 플레이어 입력을 전달하고, 일치 여부를 반환하는 함수
    bool CheckInputForAllMonsters(int inputNumber)
    {
        bool isMatched = false;  // 하나라도 숫자가 맞으면 true로 설정

        // 모든 고블린을 찾아서 입력 확인
        GoblinController[] allGoblins = FindObjectsOfType<GoblinController>();
        foreach (GoblinController goblin in allGoblins)
        {
            if (goblin.CheckPlayerInput(inputNumber))  // 고블린의 입력과 비교
            {
                isMatched = true;  // 일치하면 true로 설정
            }
        }

        // 모든 버섯을 찾아서 입력 확인
        MushroomController[] allMushrooms = FindObjectsOfType<MushroomController>();
        foreach (MushroomController mushroom in allMushrooms)
        {
            if (mushroom.CheckPlayerInput(inputNumber))  // 버섯의 입력과 비교
            {
                isMatched = true;  // 일치하면 true로 설정
            }
        }

        // 모든 박쥐를 찾아서 입력 확인
        BatController[] allBats = FindObjectsOfType<BatController>();
        foreach (BatController bat in allBats)
        {
            if (bat.CheckPlayerInput(inputNumber))  // 박쥐의 입력과 비교
            {
                isMatched = true;  // 일치하면 true로 설정
            }
        }

        // 모든 스켈레톤을 찾아서 입력 확인
        SkeletonController[] allSkeletons = FindObjectsOfType<SkeletonController>();
        foreach (SkeletonController skeleton in allSkeletons)
        {
            if (skeleton.CheckPlayerInput(inputNumber))  // 스켈레톤의 입력과 비교
            {
                isMatched = true;  // 일치하면 true로 설정
            }
        }

        // 보스가 있는 경우에도 입력을 확인
        BossController boss = FindObjectOfType<BossController>();
        if (boss != null)
        {
            if (boss.CheckPlayerInput(inputNumber))  // 보스의 입력과 비교
            {
                isMatched = true;  // 일치하면 true로 설정
            }
        }

        return isMatched;  // 하나라도 맞으면 true 반환
    }
}
