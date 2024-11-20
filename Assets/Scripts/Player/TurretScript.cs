using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class TurretScript : MonoBehaviour
{
    // Handles modules
    public List<Module> modules = new List<Module>();

    // References to parts of the Turret sprite
    public GameObject TurretBody;
    public GameObject TurretLegs;
    public GameObject TurretNozzle;
    public GameObject TurretExhaust;
    public Sprite[] BodySprites;
    public Sprite[] NozzleSprites;
    public AudioClip[] ShootSounds;
    SpriteRenderer bodySprite;
    SpriteRenderer nozzleSprite;
    SpriteRenderer legsSprite;

    // Variables used by turret for calculations and other
    public GameObject Bullet;
    public GameObject Target;
    public Vector3 aimDirection;
    float timeLastFired;
    float angle;
    private EnemyManager enemyManager;

    [Header("Turret Stats")]
    public float range;
    public float fireDelay;
    public int bulletCount;
    public float spreadAngle;

    [Header("Bullet Stats")]
    public float shotSpeed;
    public int damage;
    public int pierce;
    public float bulletLifetime;
    // Special, module-exclusive stats (Still passed to bullet)
    public bool isExplosive;
    public bool isHoming;
    public float homingStrength;
    public bool givesBurn;
    public float burnTime;

    // Base stats //
    private int baseDamage = 1;
    private float baseRange = 6.0f;
    private float BaseFireDelay = 1f;
    private int baseBulletCount = 1;
    private float baseSpreadAngle = 3;
    private float baseShotSpeed = 20.0f;
    private int basePierce = 1;
    private float baseBulletLifetime = 1f;
    private float baseHomingStrength = 1f;
    private float baseBurnTime = 2.0f;

    // Handles Audio
    public AudioSource shootAudioSource;
    public AudioSource upgradeAudioSource;
    public AudioClip shootSound;

    public void Init(EnemyManager _enemyManager)
    {
        enemyManager = _enemyManager;
    }

    // Start is called before the first frame update
    void Start()
    {
        bodySprite = TurretBody.GetComponent<SpriteRenderer>();
        nozzleSprite = TurretNozzle.GetComponent<SpriteRenderer>();
        legsSprite = TurretLegs.GetComponent<SpriteRenderer>();

        shootAudioSource = GetComponent<AudioSource>();
        // temporary
        enemyManager = FindFirstObjectByType<EnemyManager>();

        // Set stats
        damage = baseDamage;
        range = baseRange;
        fireDelay = BaseFireDelay;
        bulletCount = baseBulletCount;
        spreadAngle = baseSpreadAngle;
        shotSpeed = baseShotSpeed;
        pierce = basePierce;

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
        shootAudioSource.pitch = Random.Range(0.8f, 1.2f);
        shootAudioSource.PlayOneShot(shootSound);
        GameObject bul = Instantiate(Bullet, transform.position, Quaternion.identity);
        bul.GetComponent<BulletScript>().SpawnBullet(angle + Random.Range(spreadAngle, spreadAngle * -1), shotSpeed, damage, bulletLifetime, pierce);
        
        // Homing
        bul.GetComponent<HomingMovement>().enabled = isHoming;
    }

    public void changeCostume()
    {
        //Damage
        switch (damage)
        {
           case int i when i < 1:
                bodySprite.sprite = BodySprites[0];
                shootSound = ShootSounds[0];
                break;
           case int i when i > 1 && i <= 2:
                bodySprite.sprite = BodySprites[1];
                shootSound = ShootSounds[1];
                break;
           case int i when i > 3 && i <= 6:
                bodySprite.sprite = BodySprites[2];
                shootSound = ShootSounds[2];
                break;
           case int i when i > 6:
                bodySprite.sprite = BodySprites[3];
                shootSound = ShootSounds[3];
                break;
        }

        //Accuracy
        switch (spreadAngle)
        {
            case float i when i <= 8:
                nozzleSprite.sprite = NozzleSprites[0];
                break;
            case float i when i > 9 && i <= 16:
                nozzleSprite.sprite = NozzleSprites[1];
                break;
            case float i when i > 16:
                nozzleSprite.sprite = NozzleSprites[2];
                break;
        }

        //Play upgrade sound
        upgradeAudioSource.Play();
    }

    private void UpdateModules()
    {
        damage = baseDamage;
        fireDelay = BaseFireDelay;
        pierce = basePierce;
        shotSpeed = baseShotSpeed;
        range = baseRange;
        bulletCount = baseBulletCount;
        spreadAngle = baseSpreadAngle;

        foreach (Module module in modules)
        {
            damage += module.damage;
            fireDelay -= module.fireDelay;
            pierce += module.pierce;
            shotSpeed += module.shotSpeed;
            range += module.range;
            bulletCount += module.bulletCount;
            spreadAngle -= module.spreadAngle;
        }
        Debug.Log(fireDelay);
        if (fireDelay <= 0.025f)
        {
            fireDelay = 0.025f;
        }
        if (spreadAngle < 0f)
        {
            fireDelay = 0f;
        }
        bulletLifetime = range * 1.5f;
        changeCostume();
    }

    public void AddModule(Module newModule)
    {
        modules.Add(newModule);
        UpdateModules();
    }

    public void RemoveModule(Module newModule)
    {
        modules.Remove(newModule);
        UpdateModules();
    }
}
