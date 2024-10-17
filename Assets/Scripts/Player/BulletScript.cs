using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform Target;

    // Bullet's stats
    private float shotSpeed;
    private float damage;
    private float size;

    // Bullet's effects
    private bool isHoming;
    private float homingStrength;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    public void SpawnBullet(Vector3 newDir, float newShotSpeed, int newDamage)
    {
        // Sets direction
        transform.eulerAngles = newDir;
        Debug.Log(newDir);

        // Sets stats
        shotSpeed = newShotSpeed;
        damage = newDamage;
    }

    public void SpawnBullet(Transform target)
    {

    }

    private void UseHoming()
    {
        // Thanxx Brackeys :)
        Vector2 direction = (Vector2)Target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = rotateAmount * homingStrength;
    }

}
