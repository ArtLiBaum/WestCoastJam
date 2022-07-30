using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PropGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> propSets;
    private float _globalTime;
    private float _spawnTime;
    private void Awake()
    {
       _spawnTime = _globalTime = 0;
    }

    private void Update()
    {
        
        //Spawn a random prop prefab ever 3 seconds
        if (Math.Abs(_globalTime - _spawnTime) < 0.1f)
        {
            Instantiate(propSets[UnityEngine.Random.Range(0, propSets.Count)], transform);
            _spawnTime = _globalTime + 3;
        }
        
        _globalTime += Time.deltaTime;

    }
    
    
}
