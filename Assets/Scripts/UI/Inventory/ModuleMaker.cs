using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ModuleMaker : MonoBehaviour
{
    public ModuleCard cardPrefab;

    public AudioSource audioSource;
    public AudioClip moduleCreateSound;

    private void Start()
    {
        //Creates 2 stat modules to start
        CreatePositiveStatModule(1, new List<int> {1, 2, 3});
        CreatePositiveStatModule(1, new List<int> { 1, 2, 3});
    }

    public Module setupStats(Module newModule, int quality, int isDebuff, List<int> allowedStats)
    {
        // Changes the options in the list if the module is being run through this code a second time depending on what stats the module already has
        // May add multiple copies of stuff to increase the weight (ie homing has a higher chance of being coupled with a negative shot speed or fire rate)
        // We're yandeving this shit idc anymore
        if (isDebuff == 1)
        {
            allowedStats.Add(6);
            allowedStats.Add(6);
            allowedStats.Add(6);
            allowedStats.Add(6);
            if (quality <= 2)
            {
                allowedStats.Add(6);
                allowedStats.Add(6);
            }
        }
        if (newModule.damage != 0)
        {
            allowedStats.Remove(0);
            if (isDebuff == 1)
            {
                allowedStats.Add(1);
                allowedStats.Add(1);
                allowedStats.Add(4);
                allowedStats.Add(4);
            }
        }
        if (newModule.fireDelay != 0)
        {
            allowedStats.Remove(1);
            if (isDebuff == 1)
            {
                allowedStats.Add(6);
                allowedStats.Add(6);
            }
        }
        if (newModule.pierce != 0)
        {
            allowedStats.Remove(2);
        }
        if (newModule.shotSpeed != 0)
        {
            allowedStats.Remove(3);
            if (isDebuff == 1)
            {
                allowedStats.Add(4);
                allowedStats.Add(4);
                allowedStats.Add(4);
                allowedStats.Add(4);
                allowedStats.Add(4);
            }
        }
        if (newModule.range != 0)
        {
            allowedStats.Remove(4);
            if (isDebuff == 1)
            {
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
            }
        }
        if (newModule.bulletCount != 0)
        {
            allowedStats.Remove(5);
            if (isDebuff == 1)
            {
                allowedStats.Add(0);
                allowedStats.Add(0);
                allowedStats.Add(0);
                allowedStats.Add(1);
                allowedStats.Add(1);
                allowedStats.Add(1);
                allowedStats.Add(1);
                allowedStats.Add(1);
                allowedStats.Add(1);
                allowedStats.Add(6);
                allowedStats.Add(6);
                allowedStats.Add(6);
                allowedStats.Add(6);
                allowedStats.Add(6);
                allowedStats.Add(6);
                allowedStats.Add(6);
                allowedStats.Add(6);
                allowedStats.Add(6);
                allowedStats.Add(6);
            }
        }
        if (newModule.spreadAngle != 0)
        {
            allowedStats.Remove(6);
        }
        if (newModule.homingStrength != 0)
        {
            allowedStats.Remove(7);
            if (isDebuff == 1)
            {
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
                allowedStats.Add(3);
            }
            else
            {
                allowedStats.Add(4);
                allowedStats.Add(4);
                allowedStats.Add(4);
                allowedStats.Add(4);
                allowedStats.Add(4);
                allowedStats.Add(4);
                allowedStats.Add(4);
                allowedStats.Add(4);
                allowedStats.Add(4);
                allowedStats.Add(4);
                allowedStats.Add(2);
                allowedStats.Add(2);
                allowedStats.Add(2);
                allowedStats.Add(2);
            }
        }
        //Debug.Log(allowedStats);
        // Adds one of the stats
        int choice = allowedStats[Random.Range(0, allowedStats.Count)];
        //Debug.Log(choice);
        switch (choice)
        {
            case 0:
                //Damage
                newModule.damage = generateDamage(quality, isDebuff);
                break;
            case 1:
                //Fire Rate
                newModule.fireDelay = generateFireDelay(quality, isDebuff);
                break;
            case 2:
                //Pierce
                if (newModule.homingStrength > 0) // Generates higher quality pierce if the module also adds homing
                    quality += 2;
                newModule.pierce = generatePierce(quality, isDebuff);
                break;
            case 3:
                //Shot Speed
                newModule.shotSpeed = generateShotSpeed(quality, isDebuff);
                break;
            case 4:
                //Range
                if (newModule.homingStrength > 0) // Generates higher quality pierce if the module also adds homing
                    quality += 9;
                newModule.range = generateRange(quality, isDebuff);
                break;
            case 5:
                //Bullet Count
                newModule.bulletCount = generateBulletCount(quality, isDebuff);
                break;
            case 6:
                //Accuracy
                newModule.spreadAngle = generateAccuracy(quality, isDebuff);
                break;
            case 7:
                //Homing Strength
                newModule.homingStrength = generateHoming(quality, 0); //Can never decrease homing strength
                break;
        }
        return newModule;
    }
    public int generateDamage(int quality, int isDebuff)
    {
        int newStat = Random.Range(1 + (quality / 3), (quality + 1) / 2);
        if (isDebuff == 1)
        {
            newStat = newStat * -1;
        }
        return newStat;
    }
    public int generatePierce(int quality, int isDebuff)
    {
        int newStat = Random.Range(1 + (quality / 2), quality + 2);
        if (isDebuff == 1)
        {
            newStat = newStat * -1;
        }
        return newStat;
    }

    public int generateBulletCount(int quality, int isDebuff)
    {
        int newStat = Random.Range(1 + (quality / 3), (quality + 1) / 2);
        if (isDebuff == 1)
        {
            newStat = newStat * -1;
        }
        return newStat;
    }
    public float generateRange(int quality, int isDebuff)
    {
        //This technically generates bullet lifetime because of how I reprogrammed range
        float newStat = Random.Range(0.1f + (0.1f * (quality + 1)), 0.2f + (0.3f * (quality + 1)));
        if (isDebuff == 1)
        {
            newStat = newStat * -1.5f;
        }
        newStat = Mathf.Round(newStat * 10f) / 10f;
        return newStat;
    }
    public float generateShotSpeed(int quality, int isDebuff)
    {
        float newStat = Random.Range(0.5f + (quality / 2), 1.1f + (quality));
        if (isDebuff == 1)
        {
            newStat = newStat * -1.5f;
        }
        newStat = Mathf.Round(newStat * 10f) / 10f;
        return newStat;
    }
    public float generateAccuracy(int quality, int isDebuff)
    {
        int newStat = Random.Range(2, 6 - (2 * (quality + 1)));
        if (isDebuff == 1)
        {
            newStat = newStat * -2;
        }
        return newStat;
    }

    public float generateFireDelay(int quality, int isDebuff)
    {
        float newStat = Random.Range(0.05f + (0.01f * (quality + 1)), 0.05f + (0.05f * (quality + 1)));
        newStat = Mathf.Round(newStat * 100f) / 100f;
        
        if (isDebuff == 1)
        {
            newStat = newStat * -1;
        }
        return newStat;
    }

    public float generateHoming(int quality, int isDebuff)
    {
        int newStat = Random.Range(10 + quality, 5 + (5 * (quality + 1)));

        if (isDebuff == 1)
        {
            newStat = newStat * -1;
        }
        return newStat;
    }

    public int generateStatTest(int quality, int isDebuff)
    {
        int newStat = Random.Range(0, quality + 1) + 1;
        if (isDebuff == 1)
        {
            newStat = newStat * -1;
        }
        return newStat;
    }

    // This code is a disaster, find a better way to do all this in v2
    [ContextMenu("Create Random Stat Module")]
    public void CreateStatModule(int quality, List<int> allowedStats)
    {
        ModuleCard newCard = Instantiate(cardPrefab, transform);
        Module newModule = new Module();
        newModule.InTurret = false;

        int positiveOdds = 1 / Random.Range(1, 3); ; // If this is 1, the second stat will be positive, otherwise it is negative. Higher quality = higher chance to be 1

        // Chooses how many stats to generate
        int numberOfStats = 1;
        int numberOdds = 1;
        if (positiveOdds == 1) // If the second stat would be positive, lower chance to generate the second stat.
        {
            numberOdds = 1 / Random.Range(1, (5 / ((quality / 2) + 1))); // Chance to generate a positive second stat is affected by quality
        }
        else // If the second stat would be negative, higher chance to generate the second stat.
        {
            numberOdds = 1;
        }

        if (numberOdds == 1)
            numberOfStats = 2;

        //Debug.Log(numberOdds);
        int baseNumberOfStats = numberOfStats;
        // Generates the stats
        for (int i = 0; i < numberOfStats; i++)
        {
            // Decides whether or not the stat should be positive or negative, chance of negative stat decreases as quality increases. First stat can never be negative.
            // There's almost definitely a better way to do this but time crunch moment
            if (positiveOdds == 1) //Both stats that would be generated are positive
            {
                if (i == 0) // If this is the first stat 
                {
                    newModule = setupStats(newModule, quality, 0, allowedStats);
                }
                else // If this is the second stat
                {
                    newModule = setupStats(newModule, quality, 0, allowedStats);
                }
            }
            else //The second stat will be negative
            {
                if (i == 0)
                {
                    newModule = setupStats(newModule, quality, 0, allowedStats);
                }
                else
                {
                    newModule = setupStats(newModule, quality, 1, allowedStats);
                }
            }
        }



        if (newModule.homingStrength > 0) // Guarantees that homing modules have at least 1 extra positive stat that is either a pierce or range buff
        {
            newModule = setupStats(newModule, quality, 0, new List<int> {2, 4, 4, 4});
        }

        newModule.quality = quality;
        // Sets display
        if (positiveOdds == 1)
            newModule.quality += 1;
        newCard.setStatDisplay(newModule);

        audioSource.PlayOneShot(moduleCreateSound);
    }

    // Yandev moment
    public void CreatePositiveStatModule(int quality, List<int> allowedStats)
    {
        ModuleCard newCard = Instantiate(cardPrefab, transform);
        Module newModule = new Module();
        newModule.InTurret = false;

        int positiveOdds = 1; // If this is 1, the second stat will be positive, otherwise it is negative. Higher quality = higher chance to be 1

        // Chooses how many stats to generate
        int numberOfStats = 1;
        int numberOdds = 1;
        if (positiveOdds == 1) // If the second stat would be positive, lower chance to generate the second stat.
        {
            numberOdds = 1 / Random.Range(1, (10 - quality)); // Chance to generate a positive second stat is affected by quality
        }
        else // If the second stat would be negative, higher chance to generate the second stat.
        {
            numberOdds = 1 / Random.Range(1, 4);
        }

        if (numberOdds == 1)
            numberOfStats = 2;

        //Debug.Log(numberOdds);
        int baseNumberOfStats = numberOfStats;
        // Generates the stats
        for (int i = 0; i < numberOfStats; i++)
        {
            // Decides whether or not the stat should be positive or negative, chance of negative stat decreases as quality increases. First stat can never be negative.
            // There's almost definitely a better way to do this but time crunch moment
            if (positiveOdds == 1) //Both stats that would be generated are positive
            {
                if (i == 0) // If this is the first stat 
                {
                    newModule = setupStats(newModule, quality, 0, allowedStats);
                }
                else // If this is the second stat
                {
                    allowedStats.Add(6); // Adds Accuracy to the pool of shit that can be changed
                    newModule = setupStats(newModule, quality, 0, allowedStats);
                }
            }
            else //The second stat will be negative
            {
                if (i == 0)
                {
                    newModule = setupStats(newModule, quality, 0, allowedStats);
                }
                else
                {
                    allowedStats.Add(6); // Adds Accuracy to the pool of shit that can be changed
                    newModule = setupStats(newModule, quality, 1, allowedStats);
                }
            }
        }



        if (newModule.homingStrength > 0) // Guarantees that homing modules have at least 1 extra positive stat that is either a pierce or range buff
        {
            newModule = setupStats(newModule, quality, 0, new List<int> { 2, 4, 4, 4 });
        }

        // Sets display
        if (positiveOdds == 1)
            newModule.quality += 1;
        newCard.setStatDisplay(newModule);
    }

    public void CreateModuleCard(Module module)
    {
        ModuleCard newCard = Instantiate(cardPrefab, transform);
        newCard.setStatDisplay(module);
        module.InTurret = false;
    }

}
