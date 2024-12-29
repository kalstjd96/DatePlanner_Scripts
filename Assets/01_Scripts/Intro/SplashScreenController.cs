using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace DateApp.Platform.Intro
{
    public class SplashScreenController : MonoBehaviour
    {
        public Image splashImage;
        public float fadeSpeed = 1.0f;  // 페이드 속도
        public float displayTime = 2.0f; // 스플래시 화면 표시 시간
        public SceneDefine.SceneName nextScene;

        private void Start()
        {
            StartCoroutine(FadeSplashScreen());
        }

        private IEnumerator FadeSplashScreen()
        {
            yield return FadeIn();
            yield return new WaitForSeconds(displayTime);
            yield return FadeOut();

            if (SceneDefine.SceneNames.TryGetValue(nextScene, out string sceneName))
                SceneManager.LoadScene(sceneName);
            else
                Debug.LogError("지정한 씬이 없습니다.");
        }

        private IEnumerator FadeIn()
        {
            float alpha = 0.0f;
            Color color = splashImage.color;

            while (alpha < 1.0f)
            {
                alpha += Time.deltaTime * fadeSpeed;
                splashImage.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }
        }

        private IEnumerator FadeOut()
        {
            float alpha = 1.0f;
            Color color = splashImage.color;

            while (alpha > 0.0f)
            {
                alpha -= Time.deltaTime * fadeSpeed;
                splashImage.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }
        }
    }
}
