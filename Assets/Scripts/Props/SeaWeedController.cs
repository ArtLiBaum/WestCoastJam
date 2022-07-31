using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaWeedController : MonoBehaviour
{
    [SerializeField] private List<SeaweedPattern> seaWeedPatterns;
    [Range(-8,10)]
    [SerializeField] private float xTrigger;


    private void Update()
    {
        //Check to see if in postision
        if (transform.position.x < xTrigger)
        {
            foreach (var weed in seaWeedPatterns)
            {
                weed.Play();
            }
        }
    }
}
