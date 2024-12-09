using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class ScrapHolder : MonoBehaviour
{
    public ModuleCard cardPrefab;

    public List<Module> modules = new List<Module>();

    GameObject Conveyor;
    ModuleMaker inventoryScript;

    // Start is called before the first frame update
    void Start()
    {
        Conveyor = GameObject.FindWithTag("Conveyor");
        inventoryScript = Conveyor.GetComponent<ModuleMaker>();
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
        module.InTurret = true;
    }

    public void RemoveModule(Module module)
    {
        modules.Remove(module);
    }
}
