using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpaceJunkVariation : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Vector3 spin;
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = FindObjectOfType<SpaceJunkRandomizer>().RandomJunkSprite();
        if (UnityEngine.Random.Range(0, 4) >= 2)
            _renderer.flipX = !_renderer.flipX;
        spin =new Vector3(0,0,Random.Range(-0.1f, 0.1f));
    }


    private void Update()
    {
        //Randomly rotate sprite
        transform.Rotate(spin);
    }
}
