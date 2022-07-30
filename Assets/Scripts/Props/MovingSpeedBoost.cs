using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpeedBoost : MovingProp
{
    [SerializeField] private float boostStrength;
    protected override void OnHit()
    {
        GameManager.LevelSpeed -= boostStrength;
        Destroy(gameObject);
    }
}
