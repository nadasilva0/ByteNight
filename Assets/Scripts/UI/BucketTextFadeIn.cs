using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BucketTextFadeIn : MonoBehaviour
{
    private bool IsShowing;
    public EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if ((!enemyManager.waveActive && enemyManager.numEnemiesLeft == 0) && enemyManager.waveCounter == 9)
        {
            if (!IsShowing)
            {
                this.transform.localScale = new Vector3(3.758582f, 1.093145f, 1.285028f);
                IsShowing = true;
            }
            
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.S))
            {
                this.transform.localScale = Vector3.zero;
            }
        }
        else
        {
            this.transform.localScale = Vector3.zero;
        }
    }

}
