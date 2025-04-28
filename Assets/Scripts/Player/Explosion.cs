using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public BulletScript bulletScript;
    public int pierce;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            EnemyHealthController enemyHealth = collision.gameObject.GetComponent<EnemyHealthController>();
            enemyHealth.TakeDamage(damage);
            pierce = pierce - (enemyHealth.pierceResist + 1);
            if (pierce <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
