using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MovingProp
{
    [SerializeField] private float penalty;
    [SerializeField] private float screenShakeDur = 0.2f;
    [SerializeField] private float screenShakeIntensity = 0.2f;
    
    protected override void OnHit()
    {
        GameManager.LevelSpeed += penalty;
        CameraController.Main.Shake(screenShakeIntensity, screenShakeDur);
        Destroy(gameObject);
    }
}
