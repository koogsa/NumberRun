using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Update()
    {
        // RŰ�� ������ Stage1 ������ ��ȯ
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    // ������ �ٽ� �����ϴ� �Լ�
    public void RestartGame()
    {
        SceneManager.LoadScene("Stage1");  // Stage1 ���� �ٽ� �ε��Ͽ� ���� �����
    }

    // ���� �޴��� ���ư��� �Լ�
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  // ���� �޴� ������ �̵�
    }
}
