using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AdaptiveVFX : MonoBehaviour
{
    private ColorGrading colorEffect;
    private Bloom bloom;
    private Vignette vignette;
    
    [SerializeField] private Vector2 saturationScalingPoints = new Vector2(0, 2);

    [SerializeField] private Vector2 saturationMinMax = new Vector2(-100, 0);

    [SerializeField] private Vector2 bloomIntensityScalingPoints = new Vector2(2, 4);

    [SerializeField] private Vector2 bloomIntensityMinMax = new Vector2(0, 5);

    [SerializeField] private Vector2 vignetteScalingPoints = new Vector2(0, 3);

    [SerializeField] private Vector2 vignetteIntensityMinMax = new Vector2(0.5f, 0);

    private void Awake()
    {
        var volume = GetComponent<PostProcessVolume>();
        bloom = volume.profile.GetSetting<Bloom>();
        colorEffect = volume.profile.GetSetting<ColorGrading>();
        vignette = volume.profile.GetSetting<Vignette>();
    }

    private static float GetTFromLevelSpeed(Vector2 scalePoints)
    {
        return Mathf.Clamp01((Mathf.Abs(GameManager.LevelSpeed) - scalePoints.x) / (scalePoints.y - scalePoints.x));
    }

    private void Update()
    {
        bloom.intensity.Interp(bloomIntensityMinMax.x, bloomIntensityMinMax.y, GetTFromLevelSpeed(bloomIntensityScalingPoints));
        colorEffect.saturation.Interp(saturationMinMax.x, saturationMinMax.y, GetTFromLevelSpeed(saturationScalingPoints));
        vignette.intensity.Interp(vignetteIntensityMinMax.x, vignetteIntensityMinMax.y, GetTFromLevelSpeed(vignetteScalingPoints));
    }
}
