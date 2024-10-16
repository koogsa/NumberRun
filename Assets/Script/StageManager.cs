using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public int currentStage = 1;  // ���� ���������� �����ϴ� ���� (�ʱⰪ�� 1)
    private static StageManager instance;  // �̱��� ������ ���� �ν��Ͻ�

    void Awake()
    {
        // �̱��� ����: StageManager�� ������ �ν��Ͻ��� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // ���������� �ٲ� ������Ʈ�� �ı����� �ʰ� ��
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���������� Ŭ������ �� ���� ���������� �̵��ϴ� �Լ�
    public void AdvanceToNextStage()
    {
        if (currentStage == 1)
        {
            LoadStage(2);  // �������� 2�� �̵�
        }
        else if (currentStage == 2)
        {
            LoadStage(3);  // �������� 3�� �̵�
        }
        else
        {
            Debug.Log("��� ���������� Ŭ�����߽��ϴ�.");
            // �ʿ��� ��� ���� �� ������ �̵��� �� ����
        }
    }

    // Ư�� ���������� �̵��ϴ� �Լ�
    public void LoadStage(int stageNumber)
    {
        currentStage = stageNumber;
        string sceneName = "Stage" + stageNumber;  // �� �̸��� Stage1, Stage2, Stage3 ������ ����
        Debug.Log("�������� " + stageNumber + "���� �̵��մϴ�.");
        SceneManager.LoadScene(sceneName);  // �ش� ���� �ε�
    }
    // ���ӿ��� ������ �ٽ� �������� 1�� �̵��� �� ���� �ʱ�ȭ
    public void RestartGame()
    {
        currentStage = 1;  // �������� ��ȣ�� 1�� �ʱ�ȭ
        LoadStage(1);      // �������� 1�� �̵�
    }
}
