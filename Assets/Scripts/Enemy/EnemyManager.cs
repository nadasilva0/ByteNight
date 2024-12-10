using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    // Wave spawning stuff
    public Wave[] waves;

    private Wave currentWave;
    private int waveCounter = 0;

    private float timeBtwnWaves;
    private int i = 0;

    private bool waveActive = false;

    // Enemy prefabs
    public GameObject[] EnemyPrefabs;

    public AudioSource monsterDeath;
    public AudioSource damageSound;
    public AudioSource immuneSound;

    GameObject Conveyor;
    ModuleMaker inventoryScript;

    // List that contains spawned enemies
    [SerializeField] public List<GameObject> enemiesList = new List<GameObject>();
    // Other
    private GameObject coolFuckingText;
    private void Awake()
    {
        currentWave = waves[i];
    }

    void Start()
    {
        coolFuckingText = GameObject.Find("extremely fucking cool bug text");
        inventoryScript = FindObjectOfType<ModuleMaker>();
    }

    //inventoryScript.CreateStatModule(0, new List<int> { 0, 1, 1, 1, 2, 2, 3, 3, 4, 4, 5 });

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && waveActive == false && enemiesList.Count == 0)
        {
            Debug.Log("Spawning Wave");
            StartCoroutine(SpawnWave());
            IncWave();
            waveCounter++;
            if (waveCounter == 1)
            {
                SpawnCoolBug();
            }
            Debug.Log(waveCounter);
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

    IEnumerator SpawnWave()
    {
        waveActive = true;

        for (int j = 0; j < currentWave.EnemiesInWave.Length; j++)
        {
            yield return new WaitForSeconds(currentWave.TimeBeforeWaveStart[j]);
            StartCoroutine(waitToSpawn(currentWave.TimeBetweenSpawns[j], currentWave.EnemiesInWave[j], j));
        }
        waveActive = false;
    }
    private void IncWave()
    {
        if (i + 1 < waves.Length)
        {
            i++;
            currentWave = waves[i];
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

    IEnumerator waitToSpawn(float timeToWait, GameObject enemy, int j)
    {
        for (int i = 0; i < currentWave.NumberToSpawn[j]; i++)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(timeToWait);
        }
    }
}
