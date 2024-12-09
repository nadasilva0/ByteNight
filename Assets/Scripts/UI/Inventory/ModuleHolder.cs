using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class ModuleHolder : MonoBehaviour
{
    public ModuleCard cardPrefab;

    GameObject Turret;
    TurretScript script;
    GameObject Conveyor;
    ModuleMaker inventoryScript;
    
    // Start is called before the first frame update
    void Start()
    {
        Turret = GameObject.FindWithTag("Turret");
        script = Turret.GetComponent<TurretScript>();
        Conveyor = GameObject.FindWithTag("Conveyor");
        inventoryScript = Conveyor.GetComponent<ModuleMaker>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddModule(Module module)
    {
        ModuleCard newCard = Instantiate(cardPrefab, transform);
        newCard.setStatDisplay(module);
        module.InTurret = true;
    }

    public void RemoveModule(Module module)
    {

    }

    public void UpdateInventory()
    {

    }
}
