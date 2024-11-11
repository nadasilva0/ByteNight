using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Module", menuName = "Items/Module")]
public class ModuleObject : ItemObject
{
    public int damage;
    public void Awake()
    {
        type = ItemType.Module;
    }
}
