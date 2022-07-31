using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoostVariation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private SpriteMask _mask;

    private void LateUpdate()
    {
        _mask.sprite = _renderer.sprite;
    }
}
