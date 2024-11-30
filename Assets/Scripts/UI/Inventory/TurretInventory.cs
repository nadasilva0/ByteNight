using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretInventory : MonoBehaviour
{
    public ModuleCard cardPrefab;

    GameObject Turret;
    TurretScript script;
    GameObject Conveyor;
    ModuleMaker inventoryScript;
    public
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
        newCard.setStatDisplay(module, 0);
        module.InTurret = true;
    }

    public void RemoveModule(Module module)
    {

    }

    public void UpdateInventory()
    {

    }
}
