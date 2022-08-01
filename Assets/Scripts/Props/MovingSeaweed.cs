using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSeaweed : MovingObstacle
{
    [Header("Movement Track Info")]
    [SerializeField] private Vector3 startPos, endPos;
    [SerializeField] private float speed;
    
    private bool activated;

    private float trackPercent = 0;
    private float x, y;

    protected override void FixedUpdate()
    {
        if (activated)
        {
            //Go Along Track
            trackPercent += speed * Time.fixedDeltaTime;
            x = (endPos.x - startPos.x) * trackPercent + startPos.x;
            y = (endPos.y - startPos.y) * trackPercent + startPos.y;
            _rigidbody.MovePosition(new Vector2(x,y));
            
            if (trackPercent >= 1)
                activated = false;
        }
        else
        {
            //apply movement
            base.FixedUpdate();
        }
    }

    public void Activate()
    {
        trackPercent = 0;
        activated = true;
    }
}
