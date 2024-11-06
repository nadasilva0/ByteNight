using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class ModuleObject : ScriptableObject
{
    public GameObject prefab;
    [TextArea(15,20)]
    public string description;
}
