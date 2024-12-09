using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    // Enemy prefabs
    public GameObject[] EnemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 1;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 0f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    GameObject Conveyor;
    ModuleMaker inventoryScript;

    // List that contains spawned enemies
    [SerializeField] public List<GameObject> enemiesList = new List<GameObject>();
    // Other
    private GameObject coolFuckingText;
    // Start is called before the first frame update
    void Start()
    {
        coolFuckingText = GameObject.Find("extremely fucking cool bug text");
        enemiesLeftToSpawn = baseEnemies;
        inventoryScript = FindObjectOfType<ModuleMaker>();
    }

    private void StartWave()
    {
        isSpawning = true;
        timeSinceLastSpawn = 0;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        currentWave++;
        isSpawning = false;
        inventoryScript.CreateStatModule(0, new List<int> { 0, 1, 1, 1, 2, 2, 3, 3, 4, 4, 5 });
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSpawning)
        {
            StartWave();
        }
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy(EnemyPrefabs[Random.Range(0,3)]);
            enemiesPerSecond = Random.Range(0.1f,3.0f);
            enemiesLeftToSpawn--;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesLeftToSpawn == 0 && enemiesList.Count <= 0)
        {
            EndWave();
        }

        // Debug spawning
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnEnemy(EnemyPrefabs[0]);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnEnemy(EnemyPrefabs[1]);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SpawnEnemy(EnemyPrefabs[2]);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnEnemy(EnemyPrefabs[3]);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnEnemy(EnemyPrefabs[4]);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnEnemy(EnemyPrefabs[5]);
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
        GameObject bug = Instantiate(EnemyPrefabs[0], transform.position, Quaternion.identity);
        coolFuckingText.SetActive(true);
        coolFuckingText.GetComponent<CoolBugTextMovement>().isCoolBugOnScreen = true;
    }

    // Searches through each enemy in the list to find the one closest to a certain point in the world
    public GameObject findClosest(Vector3 pos, float _range)
    {
        float closestDist = _range;
        GameObject closestEnemy = null;
        foreach (GameObject newEnemy in enemiesList)
        {
            float dist = Vector3.Distance(pos, newEnemy.transform.position);
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

    // Searches through each enemy in the list to find the one furthest along the track
    public GameObject findFurthest()
    {
        float furthestDist = 0;
        GameObject closestEnemy = null;
        foreach (GameObject newEnemy in enemiesList)
        {
            float dist = newEnemy.GetComponent<BasicEnemyMovement>().distanceTraveled;
            if (dist > furthestDist)
            {
                furthestDist = dist;
                closestEnemy = newEnemy;
            }
            //Debug.Log(dist);
        }
        return closestEnemy;
        //Debug.Log(closestEnemy);
    }
}
