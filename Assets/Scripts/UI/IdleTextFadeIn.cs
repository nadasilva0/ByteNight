using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IdleTextFadeIn : MonoBehaviour
{
    //https://ryanjmccoach.medium.com/unity-detecting-idle-player-d0384e490f3b <---- god bless

    private float idleTime = 0f;
    private float timeToIdle = 6f;
    private bool isIdle = false;
    public EnemyManager enemyManager;
    public TurretInventory turretInventory;
    public ScrapInventory ScrapInvMenu;

    public TMP_Text textDisplay;

    // Start is called before the first frame update
    void Start()
    {
        textDisplay.color = new Color(textDisplay.color.r, textDisplay.color.g, textDisplay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (turretInventory.menuOn || ScrapInvMenu.menuOn || enemyManager.numEnemiesLeft != 0)
        {
            idleTime = 0f;
            if (isIdle)
            {
         
                isIdle = false;
                StartCoroutine(FadeOut());
            }
        }
        else
        {
            idleTime += Time.deltaTime;

            if (idleTime >= timeToIdle && !isIdle)
            {
                isIdle = true;
                StartCoroutine(FadeIn());
            }
        }
        //Debug.Log(idleTime);
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
