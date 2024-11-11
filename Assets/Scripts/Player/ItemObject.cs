using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum ItemType
{
    Turret,
    Module
}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items")]
public class ItemObject : ScriptableObject
{
    public ItemType type;
}
