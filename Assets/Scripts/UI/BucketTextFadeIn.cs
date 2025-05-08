using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BucketTextFadeIn : MonoBehaviour
{
    private bool IsFadingIn;
    public TMP_Text textDisplay;
    public EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        textDisplay.color = new Color(textDisplay.color.r, textDisplay.color.g, textDisplay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsFadingIn && !enemyManager.waveActive && enemyManager.waveCounter == 9)
        {
            IsFadingIn = true;
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeOut()
    {
          float duration = 0.9f; 
          float currentTime = 0f;
          while (currentTime < duration)
          {
              currentTime += Time.deltaTime;
              float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
              textDisplay.color = new Color(textDisplay.color.r, textDisplay.color.g, textDisplay.color.b, alpha);
              yield return null;
          }
        yield break;
    }

    private IEnumerator FadeIn()
    {
        float duration = 0.9f; //Fade in over 2 seconds.
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / duration);
            textDisplay.color = new Color(textDisplay.color.r, textDisplay.color.g, textDisplay.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;

        }
        yield break;
    }

}
