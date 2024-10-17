using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawnController : MonoBehaviour
{
    // Enemies
    public GameObject CoolBug;

    // Other
    private GameObject coolFuckingText;
    // Start is called before the first frame update
    void Start()
    {
        coolFuckingText = GameObject.Find("extremely fucking cool bug text");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnCoolBug();
        }
    }

    public void SpawnEnemy()
    {
        
    }

    public void SpawnCoolBug()
    {
        GameObject bug = Instantiate(CoolBug, transform.position, Quaternion.identity);
        coolFuckingText.SetActive(true);
        coolFuckingText.GetComponent<CoolBugTextMovement>().isCoolBugOnScreen = true;
    }
}
