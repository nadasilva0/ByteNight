using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    // List that contains enemies
    [SerializeField] public List<GameObject> enemiesList = new List<GameObject>();
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
            SpawnEnemy(CoolBug);
        }
    }

    public void SpawnEnemy(GameObject enemy)
    {
        GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        newEnemy.GetComponent<EnemyHealthController>().Init(this);
        enemiesList.Add(newEnemy);
    }

    // Use this method for the easter egg bug (enables text)
    public void SpawnCoolBug()
    {
        GameObject bug = Instantiate(CoolBug, transform.position, Quaternion.identity);
        coolFuckingText.SetActive(true);
        coolFuckingText.GetComponent<CoolBugTextMovement>().isCoolBugOnScreen = true;
    }

    // Searches through each enemy in the list to find the one closest to a certain point in the world
    // Edit this so that it targets the first enemy along the track
    public GameObject findClosest(Vector3 turretPos, float _range)
    {
        float closestDist = _range;
        GameObject closestEnemy = null;
        foreach (GameObject newEnemy in enemiesList)
        {
            float dist = Vector3.Distance(turretPos, newEnemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestEnemy = newEnemy;
            }
            //Debug.Log(dist);
        }
        return closestEnemy;
        //Debug.Log(closestEnemy);
    }
}
