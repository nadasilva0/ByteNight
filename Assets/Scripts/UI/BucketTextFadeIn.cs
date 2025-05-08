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
            this.transform.localScale = new Vector3(9.02139f, 2.62378f, 3.08434f);
        }
        else
        {
            this.transform.localScale = Vector3.zero;
        }
    }

}
