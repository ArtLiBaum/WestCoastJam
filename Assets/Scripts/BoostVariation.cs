using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoostVariation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private SpriteMask _mask;
    private Vector3 spin;
    private void Start()
    {
        spin =new Vector3(0,0,UnityEngine.Random.Range(-0.1f, 0.1f));
    }

    private void LateUpdate()
    {
        _mask.sprite = _renderer.sprite;
        transform.Rotate(spin);
    }
}
