using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingObstacle : MovingProp
{
    [SerializeField] private float penalty;
    [SerializeField] private float screenShakeDur = 0.2f;
    [SerializeField] private float screenShakeIntensity = 0.2f;
    [SerializeField] private AudioClip hitSound;

    private void Start()
    {
        hitSound = Resources.Load("SFX/hitnoise") as AudioClip;
    }

    protected override void OnHit()
    {
        ++GameManager.TotalHits;
        GameManager.PlayHitSound(hitSound);
        GameManager.LevelSpeed += penalty;
        CameraController.Main.Shake(screenShakeIntensity, screenShakeDur);
        Destroy(gameObject);
    }
}
