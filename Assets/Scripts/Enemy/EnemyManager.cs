using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    // Wave spawning stuff
    public Wave[] waves;

    private Wave currentWave;
    public int waveCounter = 0;

    private int i = 0;

    public bool waveActive = false;
    public bool endOfRoundModuleGiven = true;

    public int numEnemiesLeft;

    public TMP_Text roundDisplay;

    // Enemy prefabs
    public GameObject[] EnemyPrefabs;

    public AudioSource monsterDeath;
    public AudioSource damageSound;
    public AudioSource immuneSound;

    public bool IsWaveActiveBandaidFix = false;

    public PlayerLivesController playerLivesController;

    GameObject Conveyor;
    ModuleMaker inventoryScript;

    // List that contains spawned enemies
    [SerializeField] public List<GameObject> enemiesList = new List<GameObject>();
    // Other
    private GameObject coolFuckingText;
    private void Awake()
    {
        currentWave = waves[i];
        Debug.Log("Enemymanager Active");
    }

    void Start()
    {
        IsWaveActiveBandaidFix = false;
        coolFuckingText = GameObject.Find("extremely fucking cool bug text");
        inventoryScript = FindObjectOfType<ModuleMaker>();
        playerLivesController = FindObjectOfType<PlayerLivesController>();
    }

    //inventoryScript.CreateStatModule(0, new List<int> { 0, 1, 1, 1, 2, 2, 3, 3, 4, 4, 5 });

    // Update is called once per frame
    void Update()
    {
        numEnemiesLeft = enemiesList.Count;
        //Debug.Log(waveCounter);
        if (Input.GetKeyDown(KeyCode.Space) && waveActive == false && enemiesList.Count == 0)
        {
            endOfRoundModuleGiven = false;
            Debug.Log("Spawning Wave");
            StartCoroutine(SpawnWave());
            IncWave();
            waveCounter++;
            roundDisplay.text = $"Round {waveCounter}\n";
            if (waveCounter == 999)
            {
                SpawnCoolBug();
            }
            Debug.Log(waveCounter);
        }

        //End of wave
        if (enemiesList.Count == 0 && waveActive == false && endOfRoundModuleGiven == false)
        {
            if (playerLivesController.health < 10 && playerLivesController.hasLostLives == false)
            {
                playerLivesController.health++;
            }
            else
            {
                playerLivesController.hasLostLives = false;
            }
            spawnModuleOnRoundEnd();
            endOfRoundModuleGiven = true;
        }

        // Debug spawning
        /*
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SpawnEnemy(EnemyPrefabs[0]);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SpawnEnemy(EnemyPrefabs[1]);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnEnemy(EnemyPrefabs[2]);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SpawnEnemy(EnemyPrefabs[3]);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnEnemy(EnemyPrefabs[4]);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SpawnEnemy(EnemyPrefabs[5]);
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            IncWave();
            waveCounter++;
            roundDisplay.text = $"Round {waveCounter}\n";
        }
        */
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
            if (currentWave.isRandomWave)
            {
                currentWave = generateRandomWave(currentWave);
            }
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
    private void spawnModuleOnRoundEnd()
    {
        int roundDiv10 = Mathf.FloorToInt(waveCounter / 10f);
        if (waveCounter % 5 == 0 && waveCounter > 20)
        {
            inventoryScript.CreateStatModule(Random.Range(roundDiv10 + 1, roundDiv10 + 3), new List<int> { 0, 0, 1, 2, 3, 5, 0, 1, 2, 3, 5, 0, 1, 2, 3, 5, 7});
        }
        else if (waveCounter < 20)
        {
            inventoryScript.CreateStatModule(Random.Range(roundDiv10, roundDiv10 + 2), new List<int> {1, 1, 2, 2, 3, 3, 4, 4, 6 });
        }
        else if (waveCounter == 4)
        {
            inventoryScript.CreateStatModule(Random.Range(roundDiv10 + 1, roundDiv10 + 3), new List<int> { 5 });
        }
        else if (waveCounter == 9)
        {
            inventoryScript.CreateStatModule(Random.Range(roundDiv10 + 1, roundDiv10 + 3), new List<int> { 0 });
        }
        else if (waveCounter == 14)
        {
            inventoryScript.CreateStatModule(Random.Range(roundDiv10 + 1, roundDiv10 + 3), new List<int> { 7 });
        }
        else
        {
            inventoryScript.CreateStatModule(Random.Range(roundDiv10, roundDiv10 + 2), new List<int> { 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 6 });
        }
    }

    private Wave generateRandomWave(Wave _wave)
    {
        int scaleFactor = Mathf.FloorToInt(Mathf.Pow(1.1f, waveCounter / 2));
        Debug.Log(scaleFactor);
        int enemyTypeCount = 1 + scaleFactor;
        int enemyCount = 0;
        
        for (int r = 0; r < _wave.EnemiesInWave.Length; r++)
        {
            if (enemyTypeCount <= 0)
            {
                _wave.TimeBeforeWaveStart[r] = 0;
                _wave.NumberToSpawn[r] = 0;
                _wave.TimeBetweenSpawns[r] = 0;
            }
            else
            {
                enemyTypeCount--;

                //Sets the types of enemies that will appear
                _wave.EnemiesInWave[r] = EnemyPrefabs[Random.Range(0, 4)];

                //Sets the time until the wave appears
                if (r == 0)
                {
                    _wave.TimeBeforeWaveStart[r] = 0;
                }
                else
                {
                    _wave.TimeBeforeWaveStart[r] = Random.Range(0, 6);
                }

                //Sets the amount of this enemy to spawn
                if (waveCounter <= 50)
                {
                    _wave.NumberToSpawn[r] = (Random.Range(1, 3 + (scaleFactor / 3)));
                }
                else
                {
                    _wave.NumberToSpawn[r] = (Random.Range(1, 10) * ((scaleFactor + 1) / 4));
                }

                //Sets the spacing between these enemies
                _wave.TimeBetweenSpawns[r] = (Random.Range(1f, 6f)) / ((scaleFactor / 4) + 1);
            }
        }
        return _wave;
    }
}
