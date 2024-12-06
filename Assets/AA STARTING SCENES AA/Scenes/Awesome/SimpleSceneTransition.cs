using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; // Needed for IEnumerator and coroutines

public class SimpleSceneTransition : MonoBehaviour
{
    public Image fadeImage; // Assign the black full-screen Image in the Inspector
    public float fadeDuration = 1.0f; // Time for fade effect

    private void Start()
    {
        // Start the fade-in effect when the scene loads
        StartCoroutine(FadeIn());
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(FadeOutAndChangeScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float timer = fadeDuration;
        Color fadeColor = fadeImage.color;

        // Fade-in effect (from black to transparent)
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(timer / fadeDuration);
            fadeImage.color = fadeColor;
            yield return null;
        }

        // Disable raycast after fade-in
        fadeImage.raycastTarget = false;
    }

    private IEnumerator FadeOutAndChangeScene(string sceneName)
    {
        fadeImage.raycastTarget = true; // Block input during fade-out
        float timer = 0;
        Color fadeColor = fadeImage.color;

        // Fade-out effect (from transparent to black)
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(timer / fadeDuration);
            fadeImage.color = fadeColor;
            yield return null;
        }

        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }
}
