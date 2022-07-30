using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingProp : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        //off screen check
        
        //apply movement
        Vector2 delta = new Vector2(baseMoveSpeed * GameManager.LevelSpeed + transform.position.x, 0);
        transform.position = delta;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //When colliding with player destroy self
    }
}
