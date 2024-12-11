using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    //Other
    private EnemyManager enemyManager;

    [SerializeField] private SimpleFlash flashEffect;

    [Header("Health Stats")]
    public int maxHealth;
    public int health;
    public int damageResist;
    public int pierceResist;

    [Header("Status")]
    public bool isBurning;
    public int weaknessAmount;

    [SerializeField] private ParticleSystem metalSparks;

    public void Init(EnemyManager _enemyManager)
    {
        enemyManager = _enemyManager;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        enemyManager.enemiesList.Remove(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("taken damage");
        int totalDamage = 0;
        totalDamage = (damage + weaknessAmount) - damageResist;
        if (totalDamage <= 0)
        {
            totalDamage = 0;
            metalSparks.Play();
            enemyManager.immuneSound.pitch = Random.Range(0.6f, 1.4f);
            enemyManager.immuneSound.Play();
        }
        else
        {
            flashEffect.Flash();
            enemyManager.damageSound.pitch = Random.Range(0.8f, 1.2f);
            enemyManager.damageSound.Play();
        }
        
        health -= totalDamage;
        if (health <= 0)
        {
            Debug.Log("Killed");
            enemyManager.monsterDeath.pitch = Random.Range(0.7f, 1.3f);
            enemyManager.monsterDeath.Play();
            Destroy(gameObject);
        }
    }
}
