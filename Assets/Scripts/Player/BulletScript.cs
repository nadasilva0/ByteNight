using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform Target;
    private float timeCreated;
    [SerializeField] private TurretScript turretScript;

    // Bullet's stats
    private float shotSpeed;
    private int damage;
    public int pierce;
    public float lifetime;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        turretScript = GameObject.FindWithTag("Turret").GetComponent<TurretScript>();
    }

    void Update()
    {
        if (Time.time - timeCreated >= lifetime)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.angularVelocity = 10;
        rb.velocity = transform.up * shotSpeed;
    }

    public void SpawnBullet(float newDir, float newShotSpeed, int newDamage, float newLifetime, int newBulletPierce)
    {
        // Sets direction
        Vector3 aimDirection = new Vector3(0, 0, newDir - 90);
        transform.eulerAngles = aimDirection;
        //Debug.Log(newDir);

        // Sets Size (currently unused
        //this.transform.localScale = new Vector3(newBulletSize, newBulletSize, newBulletSize) * 0.16f;

        // Sets stats
        shotSpeed = newShotSpeed;
        damage = newDamage;
        lifetime = newLifetime;
        pierce = newBulletPierce;

        // Sets sprite
            //TODO

        // Sets other stuff
        timeCreated = Time.time;
    }

    public void SpawnBullet(Transform target)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pierce <= 0)
            return;
        if (collision.GetComponent<Collider2D>().tag == "Enemy")
        {
            //Debug.Log("Enemy hit");
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
