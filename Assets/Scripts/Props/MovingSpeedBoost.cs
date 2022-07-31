using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpeedBoost : MovingProp
{
    [SerializeField] private float boostStrength;
    [SerializeField] private GameObject burstEffect;
    
    [SerializeField] private float speedRampTime = 1.0f;

    protected override void OnHit()
    {
        Instantiate(burstEffect,transform.position,Quaternion.identity);
        ++GameManager.TotalPoints;
        GameManager.AdjustSpeed(boostStrength, speedRampTime);
        Destroy(gameObject);
    }
}
