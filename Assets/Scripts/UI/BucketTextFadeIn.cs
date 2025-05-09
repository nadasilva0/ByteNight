using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BucketTextFadeIn : MonoBehaviour
{
    private bool IsFadingIn;
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
            this.transform.localScale = new Vector3(3.758582f, 1.093145f, 1.285028f);
        }
        else
        {
            this.transform.localScale = Vector3.zero;
        }
    }

}
