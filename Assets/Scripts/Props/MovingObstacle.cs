using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MovingProp
{
    [SerializeField] private float penalty;
    
    protected override void OnHit()
    {
        GameManager.LevelSpeed += penalty;
        Destroy(gameObject);
    }
}
