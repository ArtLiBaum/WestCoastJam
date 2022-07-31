using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaWeedController : MonoBehaviour
{
    [SerializeField] private List<Animator> seaWeedAnimators;
    [Range(-8,10)]
    [SerializeField] private float xTrigger;

    [SerializeField] private AnimationClip seaweedPath;

    private void Update()
    {
        //Check to see if in postision
        if (transform.position.x < xTrigger)
        {
            foreach (var anim in seaWeedAnimators)
            {
                anim.Play(seaweedPath.name);
            }
        }
    }
}
