using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform Target;
    private float timeCreated;

    // Bullet's stats
    private float shotSpeed;
    private int damage;
    public int pierce;
    public float lifetime;

    // Bullet's effects
    private bool isHoming;
    private float homingStrength;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        // Homing bullet turning
        if (isHoming)
        {
            UseHoming();
        }
        
        // Code that actually moves bullet. Place this last in the FixedUpdate order.
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

    private void UseHoming()
    {
        // Thanks Brackeys :)
        Vector2 direction = (Vector2)Target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = rotateAmount * homingStrength;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            collision.gameObject.GetComponent<EnemyHealthController>().TakeDamage(damage);
            pierce--;
            if (pierce <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
