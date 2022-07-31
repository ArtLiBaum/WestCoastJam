using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PropGenerator : MonoBehaviour
{
    [SerializeField] private List<PropSet> easySets,medSets,hardSets;
    private Dictionary<string, GameObject> _allSets;
    private float _globalTime;
    private float _spawnTime;

    [SerializeField] private SpawnState _currentState;
    public SpawnState CurrentState => _currentState;
    
    private RandomSetManager _easyRandomSetManager, _medRandomSetManager, _hardRandomSetManager;
    private RandomSetManager _currentSetManager;
    private PropSet _currentSet;

    private bool _isRandom = true;
    
    
    
    private void Start()
    {
        _easyRandomSetManager = gameObject.AddComponent<RandomSetManager>();
        _medRandomSetManager = gameObject.AddComponent<RandomSetManager>();
        _hardRandomSetManager = gameObject.AddComponent<RandomSetManager>();

        foreach (var set in easySets)
        {
            _easyRandomSetManager.AddEvent(set);
        }
        foreach (var set in medSets)
        {
            _medRandomSetManager.AddEvent(set);
        }
        foreach (var set in hardSets)
        {
            _hardRandomSetManager.AddEvent(set);
        }
        
        //TESTING
        SetState(SpawnState.RandomEasy);
    }

    public enum SpawnState
    {
        Scripted,
        RandomEasy,
        RandomMed,
        RandomHard,
    }

    private void Awake()
    {
        _spawnTime = _globalTime = 0;
    }

    private void Update()
    {
        //dont update if game is stopped
        if (!GameManager.isPlaying) return;
        
        _globalTime += GameManager.LevelTime;


        if (_isRandom)
            SpawnRandom();
    }

    public void SetState(SpawnState state)
    {
        _currentState = state;
        switch (_currentState)
        {
            case SpawnState.Scripted:
                _isRandom = false;
                break;
            case SpawnState.RandomEasy:
                _isRandom = true;
                _currentSetManager = _easyRandomSetManager;
                break;
            case SpawnState.RandomMed:
                _isRandom = true;
                _currentSetManager = _medRandomSetManager;
                break;
            case SpawnState.RandomHard:
                _isRandom = true;
                _currentSetManager = _hardRandomSetManager;
                break;
            default:
                break;
        }
    }

   private void SpawnRandom()
   {
       //Spawn a random prop prefab 
       if (!(Math.Abs(_globalTime - _spawnTime) < 0.1f)) return;
       
       _currentSet = _currentSetManager.SelectSet();
        Instantiate(_currentSet.Prefab, transform);
        _spawnTime = _globalTime + _currentSet.SpawnDelay ;
   }
    
}