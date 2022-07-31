using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public static CameraController Main { get; private set; }

    public Camera Cam { get; private set; }

    private void Awake()
    {
        Main = this;
        Cam = GetComponent<Camera>();
    }

    #region Camera shake

    [SerializeField] private float shakeIntensity = 1.0f;
    [SerializeField] private float shakeDuration = 1.0f;
    
    private float curShakeDuration = 0.0f;
    private float curShakeIntensity = 0.0f;

    private Vector3 curShakeOffset = Vector3.zero;
    
    public void Shake(float intensity, float duration)
    {
        if (intensity >= curShakeIntensity || curShakeDuration <= 0)
        {
            curShakeDuration = duration * shakeDuration;
            curShakeIntensity = intensity;
        }
    }

    private Vector3 GenerateShake()
    {
        var randVector = Random.insideUnitCircle;
        return randVector / randVector.magnitude * shakeIntensity * curShakeIntensity;
    }

    private void UpdateShake()
    {
        curShakeDuration -= Time.deltaTime;

        transform.position -= curShakeOffset;
        curShakeOffset = Vector3.zero;

        if (curShakeDuration > 0)
        {
            curShakeOffset = GenerateShake();
            transform.position += curShakeOffset;
        }
    }

    #endregion


    // Update is called once per frame
    private void Update()
    {
        UpdateShake();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Shake(0.4f, 0.2f);
        }
    }
}
