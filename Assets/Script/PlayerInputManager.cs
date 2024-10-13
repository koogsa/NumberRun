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
                CheckInputForAllGoblins(i);  // 눌린 숫자와 고블린의 숫자를 비교
                Debug.Log("숫자 " + i + " 입력됨!");
                player.TriggerAttack();  // 플레이어 공격 애니메이션 실행

            }
        }
    }

    // 게임 내의 모든 고블린에게 플레이어의 입력을 전달하는 함수
    void CheckInputForAllGoblins(int inputNumber)
    {
        GoblinController[] allGoblins = FindObjectsOfType<GoblinController>();  // 현재 모든 고블린을 찾음

        // 각 고블린에게 입력된 숫자를 전달
        foreach (GoblinController goblin in allGoblins)
        {
            goblin.CheckPlayerInput(inputNumber);  // 고블린의 숫자와 비교
        }
    }
}
