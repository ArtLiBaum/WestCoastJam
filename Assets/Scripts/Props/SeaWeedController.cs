using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaWeedController : MonoBehaviour
{
    [SerializeField] private List<MovingSeaweed> seaWeedPatterns;
    [Range(-8,10)]
    [SerializeField] private float xTrigger;

    private bool triggered = false;
    
    private void Update()
    {
        //Don't fire more than once
        if (triggered) return;
        
        //Check to see if in position
        if (transform.position.x <= xTrigger)
        {
            triggered = true;
            foreach (var weed in seaWeedPatterns)
            {
                weed.Activate();
            }
        }
    }
}
