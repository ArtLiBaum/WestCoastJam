using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerLevelSpeed : MonoBehaviour
{
    private void HandleCollision(GameObject other)
    {
        if (Multitag.CompareTag(other, "SpeedBoost"))
        {
            GameManager.LevelSpeed += 1.0f;
        }
        else if (Multitag.CompareTag(other, "Obstacle"))
        {
            GameManager.LevelSpeed -= 1.0f;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        HandleCollision(other.gameObject);
    }
}
