using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float accelerationAmt = 5.0f;

    [SerializeField] private float maxSpeed = 10.0f;

    //% of speed lost when touching screen edges (must be <1 and >= 0)
    [SerializeField] private float bounceSpeedLoss = 0.3f;
    
    private float curSpeed = 0.0f;

    private Rigidbody2D rb;

    [SerializeField] private Animator anim;
    
    private static readonly int PlyVertVelocity = Animator.StringToHash("plyVertVelocity");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        //Don't update if game is stopped
        if (!GameManager.isPlaying) return;
        
        if (Input.GetKey(KeyCode.W))
        {
            curSpeed += accelerationAmt * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            curSpeed -= accelerationAmt * Time.deltaTime;
        }

        curSpeed = Mathf.Clamp(curSpeed, -maxSpeed, maxSpeed);
        
        anim.SetFloat(PlyVertVelocity, curSpeed);
    }

    private void FixedUpdate()
    {
        //Don't update if game is stopped
        if (!GameManager.isPlaying) return;
        
        var newPos = transform.position + Vector3.up * curSpeed * Time.fixedDeltaTime;

        var maxHeight = CameraController.Main.Cam.ScreenToWorldPoint(Vector3.up * Screen.height).y;
        var minHeight = CameraController.Main.Cam.ScreenToWorldPoint(Vector3.zero).y;
        
        if (newPos.y > maxHeight)
        {
            newPos.y = maxHeight;
            curSpeed *= bounceSpeedLoss - 1;

        }
        else if (newPos.y < minHeight)
        {
            newPos.y = minHeight;
            curSpeed *= bounceSpeedLoss - 1;
        }

        rb.MovePosition(newPos);
    }
}
