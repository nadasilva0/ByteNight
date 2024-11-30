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

    }
    public int generateDamage(int quality, int isDebuff)
    {
        int newStat = Random.Range(0, quality + 1) + 1;
        if (isDebuff == 1)
        {
            newStat = newStat * -1;
        }
        return newStat;
    }

    public float generateFireDelay(int quality, int isDebuff)
    {
        // THIS IS FOR FLAT FIRE DELAY CHANGES, NOT THE PERCENTAGES
        float newStat = Random.Range(0.05f, 0.10f);
        newStat = Mathf.Round(newStat * 100.0f) * 0.01f;
        if (isDebuff == 1)
        {
            newStat = newStat * -1;
        }
        Debug.Log(newStat);
        return newStat;
    }

    public float generateFireRate(int quality, int isDebuff)
    {
        // THIS IS FOR PERCENTAGE CHANGES
        float newStat = Random.Range(5f, 10f);
        newStat = Mathf.Round(newStat * 100.0f) * 0.01f;
        if (isDebuff == 1)
        {
            newStat = newStat * -1;
        }
        Debug.Log(newStat);
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

    // The way the system works rn allows setupStats to call the same stat twice, replacing the previous value. Maybe keep this but also maybe don't because it'd mess up quality calculations.
    [ContextMenu("Create Random Stat Module")]
    public void CreateStatModule(int quality)
    {
        ModuleCard newCard = Instantiate(cardPrefab, transform);
        Module newModule = new Module();
        newModule.InTurret = false;

        // Calculates number of stats, uses Quality stat to raise the odds of getting two stats on one module
        int numberOfStats = 1;
        int numberOdds = 1 / Random.Range(1, (10 - quality));
        if (numberOdds == 1)
            numberOfStats = 2;

        // Add a random number of stats to the module
        for (int i = 0; i < numberOfStats; i++)
        {
            // Adds one of the stats
            int choice = Random.Range(0, 6);
            switch (choice)
            {
                case 0:
                    //Damage
                    newModule.damage = generateDamage(quality, 0);
                    break;
                case 1:
                    //Fire Rate
                    newModule.fireDelay = generateFireDelay(quality, 0);
                    break;
                case 2:
                    //Pierce
                    newModule.pierce = generateStatTest(quality, 0);
                    break;
                case 3:
                    //Shot Speed
                    newModule.shotSpeed = generateStatTest(quality, 0);
                    break;
                case 4:
                    //Range
                    newModule.range = generateStatTest(quality, 0);
                    break;
                case 5:
                    //Bullet Count
                    newModule.bulletCount = generateStatTest(quality, 0);
                    break;
                case 6:
                    //Accuracy
                    newModule.spreadAngle = generateStatTest(quality, 0);
                    break;
            }
        }
        newCard.setStatDisplay(newModule, 0);
        audioSource.PlayOneShot(moduleCreateSound);
    }

    public void CreateModuleCard(Module module)
    {
        ModuleCard newCard = Instantiate(cardPrefab, transform);
        newCard.setStatDisplay(module, 0);
        module.InTurret = false;
    }

}
