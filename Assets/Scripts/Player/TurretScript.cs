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
    private GameObject turretInv;
    private GameObject turretStatObject;
    public Sprite[] BodySprites;
    public Sprite[] NozzleSprites;
    public AudioClip[] ShootSounds;
    SpriteRenderer bodySprite;
    SpriteRenderer nozzleSprite;
    SpriteRenderer legsSprite;

    public ParticleSystem smokeParticle;

    public ParticleSystem upgradeSparks;
    public ParticleSystem upgradeSpraypaint;
    public ParticleSystem upgradeDrillSparks;

    public GameObject shootPoint;

    ModuleHolder turretInventoryScript;
    TurretStatContoller turretStatScript;

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
    public float fireRateMult;
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
    private float baseSpreadAngle = 0;
    private float baseShotSpeed = 15.0f;
    private int basePierce = 1;
    private float baseBulletLifetime = 0.4f;
    private float baseHomingStrength = 0f;
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

        turretInv = GameObject.FindWithTag("TurretInv");
        turretStatObject = GameObject.FindWithTag("TurretStatDisplay");
        turretInventoryScript = turretInv.GetComponent<ModuleHolder>();
        turretStatScript = turretStatObject.GetComponent<TurretStatContoller>();

        shootAudioSource = GetComponent<AudioSource>();
        // temporary
        enemyManager = FindFirstObjectByType<EnemyManager>();

        // Set stats
        UpdateModules();
    }

    void Update()
    {
        //Handles aiming and shooting
        // Too lazy to program findFurthest to work with range so bullet lifetime is making a comeback
        if (enemyManager.findFurthest())
        {
            // Aims at target
            Target = enemyManager.findFurthest();
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            aimDirection = new Vector3(0, 0, angle - 90);
            transform.eulerAngles = aimDirection;

            // Shoots at target
            if (Time.time - timeLastFired >= fireDelay)
            {
                timeLastFired = Time.time;
                if (bulletCount <= 0 || damage <= 0 || bulletLifetime <= 0.1 || shotSpeed <= 0 || pierce <= 0)
                {
                    cough();
                }
                else
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
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bul = Instantiate(Bullet, shootPoint.transform.position, Quaternion.identity);
            bul.GetComponent<BulletScript>().SpawnBullet(angle + Random.Range(spreadAngle, spreadAngle * -1), shotSpeed, damage, bulletLifetime, pierce);

            if (bulletLifetime <= 0.05f)
            {
                smokeParticle.Play();
            }

            Debug.Log(isHoming);
            if (homingStrength > 0)
            {
                bul.GetComponent<HomingMovement>().enabled = true;
                bul.GetComponent<TrailRenderer>().enabled = true;
            }
        }
    }

    public void cough()
    {
        shootAudioSource.pitch = Random.Range(0.6f, 1.4f);
        shootAudioSource.PlayOneShot(ShootSounds[4]);
        ParticleSystem smoke = Instantiate(smokeParticle, shootPoint.transform.position, Quaternion.identity);
        smoke.transform.eulerAngles = new Vector3 (angle + 180, -90, 90);
    }

    public void changeCostume()
    {
        //Damage
        switch (damage)
        {
           case int i when i <= 1:
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
    }

    private void UpdateModules()
    {
        damage = baseDamage;
        fireDelay = BaseFireDelay;
        pierce = basePierce;
        shotSpeed = baseShotSpeed;
        range = baseRange;
        bulletLifetime = baseBulletLifetime;
        bulletCount = baseBulletCount;
        spreadAngle = baseSpreadAngle;
        homingStrength = baseHomingStrength;
        fireRateMult = 1;

        foreach (Module module in modules)
        {
            damage += module.damage;
            pierce += module.pierce;
            fireDelay -= module.fireDelay;
            shotSpeed += module.shotSpeed;
            range += module.range;
            // Super jank way of readding bullet lifetime
            bulletLifetime += module.range;
            bulletCount += module.bulletCount;
            spreadAngle -= module.spreadAngle;

            // Special modules
            homingStrength += module.homingStrength * 10;
        }
        //fireDelay *= (0.01f * fireRateMult);
        if (fireDelay < 0.025f)
        {
            fireDelay = 0f;
        }
        if (spreadAngle < 0f)
        {
            spreadAngle = 0f;
        }
        if (homingStrength > 0) // Starts all homing off with base 100 homing strength
            homingStrength = homingStrength + 100;

        turretStatScript.UpdateStats(damage, fireDelay, pierce, shotSpeed, bulletLifetime, bulletCount, spreadAngle);
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
