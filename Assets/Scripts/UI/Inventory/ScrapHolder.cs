using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;
using System;
using Random = UnityEngine.Random;

public class ScrapHolder : MonoBehaviour
{
    public ModuleCard cardPrefab;

    public List<Module> modules = new List<Module>();
    public List<GameObject> moduleCards = new List<GameObject>();

    public AudioSource scrapSound;
    public AudioSource errorSound;

    private GameObject moduleCard;

    GameObject Conveyor;
    ModuleMaker moduleMaker;

    // Start is called before the first frame update
    void Start()
    {
        Conveyor = GameObject.FindWithTag("Conveyor");
        moduleMaker = Conveyor.GetComponent<ModuleMaker>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddModule(Module module)
    {
        modules.Add(module);
        ModuleCard newCard = Instantiate(cardPrefab, transform);
        newCard.setStatDisplay(module);
        newCard.inScrapMenu = true;
        newCard.tag = "ScrappingCard";
        module.InTurret = true;
    }

    public void RemoveModule(Module module)
    {
        modules.Remove(module);
    }

    public void scrapModules()
    {
        if (modules.Count == 2)
        {
            int qualityResult = 0;
            int qualityChange = Random.Range(-1, 2);
            Debug.Log(qualityChange);
            List<int> allowedStats = new List<int> { 0, 1, 1, 2, 2, 3, 3, 4, 4, 5};
            foreach (Module module in modules)
            {
                qualityResult += module.quality;
            }

            qualityResult = (qualityResult / 2) + qualityChange;
            Debug.Log(qualityResult);

            if (qualityResult < 0)
            {
                qualityResult = 1;
            }

            if (qualityResult >= 2)
            {
                allowedStats.Add(0);
                allowedStats.Add(5);
                allowedStats.Remove(4);
            }
            if (qualityResult >= 3)
            {
                allowedStats.Add(7);
            }
            if (qualityResult >= 4)
            {
                allowedStats.Add(7);
                allowedStats.Add(0);
                allowedStats.Add(5);
                allowedStats.Remove(4);
                allowedStats.Remove(6);
            }
            moduleMaker.CreateStatModule(qualityResult, allowedStats);
            scrapSound.Play();
            
            foreach (var scrappedCards in GameObject.FindGameObjectsWithTag("ScrappingCard"))
            {
                Destroy(scrappedCards);
            }
            modules.Clear();

        }
        else
        {
            Debug.Log("Needs another module!");
            errorSound.Play();
        }
    }
}
