using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject Target;
    //public TurretScript turretScript;

    public Vector2 direction = Vector2.down;
    public float shotSpeed = 20.0f;
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

}
