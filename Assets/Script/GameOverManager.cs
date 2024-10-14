using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Update()
    {
        // R키를 누르면 Stage1 씬으로 전환
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    // 게임을 다시 시작하는 함수
    public void RestartGame()
    {
        SceneManager.LoadScene("Stage1");  // Stage1 씬을 다시 로드하여 게임 재시작
    }

    // 메인 메뉴로 돌아가는 함수
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  // 메인 메뉴 씬으로 이동
    }
}
