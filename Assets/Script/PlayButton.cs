using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public AudioSource audioSource;  // 버튼 클릭 시 재생할 오디오 소스
    public string nextSceneName = "stage1";  // 이동할 씬 이름

    public void OnPlayButtonClicked()
    {
        audioSource.Play();  // 사운드 재생
        Invoke("LoadNextScene", audioSource.clip.length);  // 사운드가 끝난 후 씬 전환
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);  // 지정된 씬으로 전환
    }
}
