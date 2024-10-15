using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public HeroKnight player;  // 플레이어 캐릭터 참조 (Inspector에서 할당)

    void Update()
    {
        // 키보드 숫자 입력 (1~9) 감지
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))  // 숫자 키를 눌렀을 때
            {
                Debug.Log("숫자 " + i + " 입력됨!");

                // 고블린, 버섯, 박쥐, 스켈레톤의 숫자 또는 연산 결과 확인
                bool isMatched = CheckInputForAllMonsters(i);

                // 몬스터 중 하나라도 맞으면 공격 모션 실행, 모두 틀리면 스턴 모션 실행
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

    // 게임 내의 모든 고블린, 버섯, 박쥐, 스켈레톤에게 플레이어의 입력을 전달하고, 일치 여부를 반환하는 함수
    bool CheckInputForAllMonsters(int inputNumber)
    {
        bool isMatched = false;  // 하나라도 숫자가 맞으면 true로 설정

        // 현재 모든 고블린을 찾음
        GoblinController[] allGoblins = FindObjectsOfType<GoblinController>();
        foreach (GoblinController goblin in allGoblins)
        {
            if (goblin.CheckPlayerInput(inputNumber))  // 고블린의 숫자와 비교하여 일치하면
            {
                isMatched = true;  // 일치하면 true로 설정
            }
        }

        // 현재 모든 버섯을 찾음
        MushroomController[] allMushrooms = FindObjectsOfType<MushroomController>();
        foreach (MushroomController mushroom in allMushrooms)
        {
            if (mushroom.CheckPlayerInput(inputNumber))  // 버섯의 숫자와 비교하여 일치하면
            {
                isMatched = true;  // 일치하면 true로 설정
            }
        }

        // 현재 모든 박쥐를 찾음
        BatController[] allBats = FindObjectsOfType<BatController>();
        foreach (BatController bat in allBats)
        {
            if (bat.CheckPlayerInput(inputNumber))  // 박쥐의 수학 문제 답과 비교하여 일치하면
            {
                isMatched = true;  // 일치하면 true로 설정
            }
        }

        // 현재 모든 스켈레톤을 찾음
        SkeletonController[] allSkeletons = FindObjectsOfType<SkeletonController>();
        foreach (SkeletonController skeleton in allSkeletons)
        {
            if (skeleton.CheckPlayerInput(inputNumber))  // 스켈레톤의 수학 문제 답과 비교하여 일치하면
            {
                isMatched = true;  // 일치하면 true로 설정
            }
        }

        return isMatched;  // 하나라도 숫자 또는 답이 맞으면 true 반환
    }
}
