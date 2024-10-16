using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public AudioSource audioSource;  // ��ư Ŭ�� �� ����� ����� �ҽ�
    public string nextSceneName = "stage1";  // �̵��� �� �̸�

    public void OnPlayButtonClicked()
    {
        audioSource.Play();  // ���� ���
        Invoke("LoadNextScene", audioSource.clip.length);  // ���尡 ���� �� �� ��ȯ
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);  // ������ ������ ��ȯ
    }
}
