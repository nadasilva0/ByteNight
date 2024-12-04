using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Module
{
    public bool InTurret = false;

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
    public float explosionSize;
    public bool isHoming;
    public float homingStrength;
    public bool givesBurn;
    public float burnTime;
}
