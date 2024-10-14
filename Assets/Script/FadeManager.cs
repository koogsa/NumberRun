using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;  // ���̵� �ƿ��� ���� �̹���
    public float fadeDuration = 1f;  // ���̵� �ƿ� ���� �ð�

    private void Start()
    {
        // ���� ���� �� ���̵� �̹����� �����ϰ� ����
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
    }

    // ���̵� �ƿ� ȿ�� ����
    public void StartFadeOut(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    // ���̵� �ƿ� �ڷ�ƾ
    IEnumerator FadeOut(string sceneName)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        // �ð��� ������ ���� Alpha ���� 0���� 1�� ������Ŵ
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);  // Alpha �� ����
            fadeImage.color = color;
            yield return null;
        }

        // ���̵� �ƿ� �Ϸ� �� �� ��ȯ
        SceneManager.LoadScene(sceneName);
    }
}
