using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingProp : MonoBehaviour
{
    [Range(1,10)]
    [SerializeField] private float baseMoveSpeed = 1f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //off screen check
        if(transform.position.x < -10)
            Destroy(gameObject);
        
    }

    private void FixedUpdate()
    {
        //apply movement
        _rigidbody.MovePosition(transform.position + Vector3.right*(baseMoveSpeed * GameManager.LevelSpeed * Time.fixedDeltaTime) );
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //When colliding with player destroy self
    }
}
