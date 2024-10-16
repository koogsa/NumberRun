using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    // Exit ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void OnExitButton()
    {
        Debug.Log("���� ���� ��ư�� ���Ƚ��ϴ�.");
        Application.Quit();  // ���� ���� (���� �Ŀ��� �۵�)
    }

    // Replay ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void OnReplayButton()
    {
        Debug.Log("���÷��� ��ư�� ���Ƚ��ϴ�.");
        SceneManager.LoadScene("Stage1");  // Stage1 ������ ���ư��� ���� �����
    }

}
