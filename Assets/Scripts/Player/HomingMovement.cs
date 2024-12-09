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
    [SerializeField] private GameObject Target;
    private Transform bulletTransform;
    private EnemyManager enemyManager;
    private void Start()
    {
        Turret = GameObject.FindWithTag("Turret");
        turretScript = Turret.GetComponent<TurretScript>();
        homingStrength = turretScript.homingStrength;
        enemyManager = FindFirstObjectByType<EnemyManager>();
    }
    private void FixedUpdate()
    {
        //Debug.Log("Homing script is running");
        // Thanks Brackeys :)
        Target = enemyManager.findClosest(rb.position, 600);
        Vector2 direction = (Vector2)Target.transform.position - rb.position;
        direction.Normalize();
        Debug.Log(Target);
        float rotateAmount = Vector3.Cross(direction, -1 * transform.up).z;
        Debug.Log(rotateAmount);
        rb.angularVelocity = rotateAmount * homingStrength;
    }
}
