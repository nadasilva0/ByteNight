using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class TurretScript : MonoBehaviour
{
    public InventoryObject inventory;
    // References to parts of the Turret sprite
    public GameObject TurretBody;
    public GameObject TurretLegs;
    public GameObject TurretNozzle;
    public GameObject TurretExhaust;
    public Sprite[] BodySprites;
    public Sprite[] NozzleSprites;

    // Variables used by turret for calculations and other
    public GameObject Bullet;
    private GameObject Target;
    public Vector3 aimDirection;
    float timeLastFired;
    float angle;
    private EnemyManager enemyManager;

    [Header("Turret Stats")]
    public float range = 6.0f;
    public float fireDelay = 0.1f;
    public int bulletCount = 1;
    public float spreadAngle = 15;

    [Header("Bullet Stats")]
    public float shotSpeed = 20.0f;
    public int damage = 1;
    public int pierce = 1;
    public float bulletLifetime = 1f;
    // Special, module-exclusive stats (Still passed to bullet)
    public bool isExplosive;
    public bool isHoming;
    public float homingStrength;
    public bool givesBurn;
    public float burnTime = 2.0f;

    // Temporary module equipping system
    public void OnTriggerEnter(Collider other)
    {
        
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item);
            Destroy(other.gameObject);
        }
        
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

    public void Init(EnemyManager _enemyManager)
    {
        enemyManager = _enemyManager;
    }

    // Start is called before the first frame update
    void Start()
    {
        // temporary
        enemyManager = FindFirstObjectByType<EnemyManager>();
    }

    void Update()
    {
        //Handles aiming and shooting
        if (enemyManager.findClosest(transform.position, range))
        {
            // Aims at target
            Target = enemyManager.findClosest(transform.position, range);
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            aimDirection = new Vector3(0, 0, angle - 90);
            transform.eulerAngles = aimDirection;

            // Shoots at target
            if (Time.time - timeLastFired >= fireDelay)
            {
                timeLastFired = Time.time;
                for (int i = 0; i < bulletCount; i++)
                {
                    shootBullet();
                }
            }
        }
    }

    void FixedUpdate()
    {

    }

    public void shootBullet()
    {
        GameObject bul = Instantiate(Bullet, transform.position, Quaternion.identity);
        bul.GetComponent<BulletScript>().SpawnBullet(angle + Random.Range(spreadAngle, spreadAngle * -1), shotSpeed, damage, bulletLifetime, pierce);
    }

    public void changeCostume()
    {
        //Damage
        switch (damage)
        {
           case int i when i < 1:
                TurretBody.GetComponent<SpriteRenderer>().sprite = BodySprites[0];
                break;
           case int i when i > 1 && i <= 2:
                TurretBody.GetComponent<SpriteRenderer>().sprite = BodySprites[1];
                break;
           case int i when i > 3 && i <= 6:
                TurretBody.GetComponent<SpriteRenderer>().sprite = BodySprites[2];
                break;
           case int i when i > 6:
                TurretBody.GetComponent<SpriteRenderer>().sprite = BodySprites[3];
                break;
        }

        //Accuracy
        switch (spreadAngle)
        {
            case float i when i <= 8:
                TurretNozzle.GetComponent<SpriteRenderer>().sprite = NozzleSprites[0];
                break;
            case float i when i > 9 && i <= 16:
                TurretNozzle.GetComponent<SpriteRenderer>().sprite = NozzleSprites[1];
                break;
            case float i when i > 16:
                TurretNozzle.GetComponent<SpriteRenderer>().sprite = NozzleSprites[2];
                break;
        }
    }
}
