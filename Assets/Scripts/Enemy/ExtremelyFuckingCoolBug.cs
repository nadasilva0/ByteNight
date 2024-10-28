using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExtremelyFuckingCoolBug : MonoBehaviour
{
    private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - spawnTime >= 8)
        {
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0.05f, 0);
    }
}
