using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    //Other
    private EnemyManager enemyManager;

    [Header("Health Stats")]
    public int maxHealth;
    public int health;
    public int damageResist;
    public int pierceResist;

    [Header("Status")]
    public bool isBurning;
    public int weaknessAmount;

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
        int totalDamage = 0;
        totalDamage = (damage + weaknessAmount) - damageResist;
        if (totalDamage < 0)
            totalDamage = 0;
        
        health -= totalDamage;
        if (health <= 0)
        {
            // Keep in mind to change this "Destroy" to disable gameobject if you decide to use object pooling for enemies
            enemyManager.enemiesList.Remove(this.gameObject);
            Destroy(gameObject);
        }
    }
}
