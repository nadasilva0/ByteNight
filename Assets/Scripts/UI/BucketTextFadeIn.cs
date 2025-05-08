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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyManager.waveActive && enemyManager.waveCounter == 9)
        {
            this.transform.localScale = Vector3.one;
        }
        else
        {
            this.transform.localScale = Vector3.zero;
        }
    }

}
