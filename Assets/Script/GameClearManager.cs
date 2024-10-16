using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    // Exit 버튼이 눌렸을 때 호출되는 함수
    public void OnExitButton()
    {
        Debug.Log("게임 종료 버튼이 눌렸습니다.");
        Application.Quit();  // 게임 종료 (빌드 후에만 작동)
    }

    // Replay 버튼이 눌렸을 때 호출되는 함수
    public void OnReplayButton()
    {
        Debug.Log("리플레이 버튼이 눌렸습니다.");
        SceneManager.LoadScene("Stage1");  // Stage1 씬으로 돌아가서 게임 재시작
    }

}
