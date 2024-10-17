using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector3 direction = Vector3.down;
    private float shotSpeed = 20.0f;
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //transform.eulerAngles = turretScript.aimDirection;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction.Normalize();
        rb.velocity = transform.up * shotSpeed;
    }

    public void SpawnBullet(Vector3 newDir, float newShotSpeed )
    {
        Debug.Log(newDir);
        direction = newDir;
        Debug.Log(direction);
        shotSpeed = newShotSpeed;
        transform.eulerAngles = direction;
    }

    public void SpawnBullet(Transform target)
    {

    }

}
