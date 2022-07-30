using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prop Set", menuName = "ScriptableObject/PropSet")]
public class PropSet : ScriptableObject
{
    public GameObject Prefab;
    public float SpawnDelay = 3f;
    [Range(0,100)]
    public int Weight;
}
