using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float accelerationAmt = 5.0f;

    [SerializeField] private float maxSpeed = 10.0f;

    private float curSpeed = 0.0f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            curSpeed += accelerationAmt * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            curSpeed -= accelerationAmt * Time.deltaTime;
        }

        curSpeed = Mathf.Clamp(curSpeed, -maxSpeed, maxSpeed);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + Vector3.up * curSpeed * Time.fixedDeltaTime);
    }
}
