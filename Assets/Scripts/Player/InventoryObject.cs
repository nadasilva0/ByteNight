using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<ModuleObject> Container = new List<ModuleObject>();

    public void AddModule(ModuleObject module)
    {

    }
}
