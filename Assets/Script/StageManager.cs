using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public int currentStage = 1;  // 현재 스테이지를 추적하는 변수 (초기값은 1)
    private static StageManager instance;  // 싱글턴 패턴을 위한 인스턴스

    void Awake()
    {
        // 싱글턴 패턴: StageManager의 유일한 인스턴스를 유지
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 스테이지가 바뀌어도 오브젝트가 파괴되지 않게 함
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 스테이지를 클리어한 후 다음 스테이지로 이동하는 함수
    public void AdvanceToNextStage()
    {
        if (currentStage == 1)
        {
            LoadStage(2);  // 스테이지 2로 이동
        }
        else if (currentStage == 2)
        {
            LoadStage(3);  // 스테이지 3로 이동
        }
        else
        {
            Debug.Log("모든 스테이지를 클리어했습니다.");
            // 필요한 경우 엔딩 씬 등으로 이동할 수 있음
        }
    }

    // 특정 스테이지로 이동하는 함수
    public void LoadStage(int stageNumber)
    {
        currentStage = stageNumber;
        string sceneName = "Stage" + stageNumber;  // 씬 이름을 Stage1, Stage2, Stage3 등으로 구성
        Debug.Log("스테이지 " + stageNumber + "으로 이동합니다.");
        SceneManager.LoadScene(sceneName);  // 해당 씬을 로드
    }
    // 게임오버 씬에서 다시 스테이지 1로 이동할 때 상태 초기화
    public void RestartGame()
    {
        currentStage = 1;  // 스테이지 번호를 1로 초기화
        LoadStage(1);      // 스테이지 1로 이동
    }
}
