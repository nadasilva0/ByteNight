using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SimpleSceneTransition : MonoBehaviour
{
    public Image fadeImage; // Assign the black full-screen Image in the Inspector
    public float fadeDuration = 1.0f; // Time for fade effect

    private bool isFading = false; // Prevent multiple clicks during transitions

    private void Start()
    {
        fadeImage.raycastTarget = false; // Prevent blocking input initially
        StartCoroutine(FadeIn());
    }

    public void ChangeScene(string sceneName)
    {
        if (isFading) return; // Block additional input during transition
        isFading = true; // Set flag to indicate transition in progress
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

        fadeImage.raycastTarget = false; // Allow input after fade-in
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

        SceneManager.LoadScene(sceneName);
    }
}
