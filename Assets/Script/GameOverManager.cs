using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private StageManager stageManager;
    void Start()
    {
        stageManager = FindObjectOfType<StageManager>();  // StageManager ã��
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            stageManager.RestartGame();  // RŰ�� ������ ���� �����
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
