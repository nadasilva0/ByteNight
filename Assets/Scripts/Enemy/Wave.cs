using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Waves", order = 1)]
public class Wave : ScriptableObject
{
    [field: SerializeField]
    public GameObject[] EnemiesInWave { get; private set; }
    [field: SerializeField]
    public float[] TimeBeforeWaveStart { get; private set; }

    [field: SerializeField]
    public float[] NumberToSpawn { get; private set; }

    [field: SerializeField]
    public float[] TimeBetweenSpawns { get; private set; }

    [field: SerializeField]
    public bool isRandomWave { get; private set; }
}
