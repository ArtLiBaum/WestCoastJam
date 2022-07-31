using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpeedBoost : MovingProp
{
    [SerializeField] private float boostStrength;
    [SerializeField] private GameObject burstEffect;
    protected override void OnHit()
    {
        Instantiate(burstEffect,transform.position,Quaternion.identity);
        ++GameManager.TotalPoints;
        GameManager.LevelSpeed -= boostStrength;
        Destroy(gameObject);
    }
}
