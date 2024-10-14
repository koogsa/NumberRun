using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;  // 페이드 아웃용 검은 이미지
    public float fadeDuration = 1f;  // 페이드 아웃 지속 시간

    private void Start()
    {
        // 게임 시작 시 페이드 이미지를 투명하게 설정
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
    }

    // 페이드 아웃 효과 시작
    public void StartFadeOut(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    // 페이드 아웃 코루틴
    IEnumerator FadeOut(string sceneName)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        // 시간이 지남에 따라 Alpha 값을 0에서 1로 증가시킴
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);  // Alpha 값 증가
            fadeImage.color = color;
            yield return null;
        }

        // 페이드 아웃 완료 후 씬 전환
        SceneManager.LoadScene(sceneName);
    }
}
