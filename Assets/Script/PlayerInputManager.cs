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

                // 고블린과 버섯의 숫자를 확인
                bool isMatched = CheckInputForAllGoblinsAndMushrooms(i);

                // 고블린과 버섯 중 하나라도 맞으면 공격 모션 실행, 모두 틀리면 스턴 모션 실행
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

    // 게임 내의 모든 고블린과 버섯에게 플레이어의 입력을 전달하고, 일치 여부를 반환하는 함수
    bool CheckInputForAllGoblinsAndMushrooms(int inputNumber)
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

        return isMatched;  // 하나라도 숫자가 맞으면 true 반환
    }
}
