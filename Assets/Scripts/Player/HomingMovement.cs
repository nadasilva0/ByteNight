using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HomingMovement : MonoBehaviour
{
    public BulletScript bulletScript;
    [SerializeField] private GameObject Turret;
    [SerializeField] private TurretScript turretScript;
    public Rigidbody2D rb;
    [SerializeField] private float homingStrength;
    private Transform Target;
    private Transform bulletTransform;

    private void Start()
    {
        Turret = GameObject.FindWithTag("Turret");
        turretScript = Turret.GetComponent<TurretScript>();
        homingStrength = turretScript.homingStrength;
        bulletTransform = this.transform;
    }
    private void FixedUpdate()
    {
        Target = turretScript.Target.GetComponent<Transform>();
        // Thanks Brackeys :)
        Vector2 direction = (Vector2)Target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, bulletTransform.up).z;
        rb.angularVelocity = rotateAmount * homingStrength;
        rb.velocity = transform.up * 10;
    }
}
