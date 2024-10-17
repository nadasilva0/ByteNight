using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int maxHealth;
    public int health;

    // Status Effects
    public bool isBurning;
    public bool isWeakened;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            // Keep in mind to change this "Destroy" to disable gameobject if you decide to use object pooling for enemies
            Destroy(gameObject);
        }
    }
}
