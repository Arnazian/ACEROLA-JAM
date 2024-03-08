using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class PlayerLightController : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private Light2D flashLight;
    [SerializeField] private Light2D baseLight;

    [SerializeField] private float secondsToDimFlashLight;
    private float flashLightOriginalIntensity;
    private float baseLightOriginalIntensity;

    private void Start()
    {
        flashLightOriginalIntensity = flashLight.intensity;
        baseLightOriginalIntensity = baseLight.intensity;
    }
    public void ShootingStateLights()
    {
        DOTween.To(() => flashLight.intensity, x => flashLight.intensity = x, 0f, secondsToDimFlashLight);
    }

    public void MoveStateLights()
    {
        DOTween.To(() => flashLight.intensity, x => flashLight.intensity = x, flashLightOriginalIntensity, secondsToDimFlashLight);
    }

    public void FadeOutLights(float duration)
    {
        DOTween.To(() => baseLight.intensity, x => baseLight.intensity = x, 0f, duration);
        DOTween.To(() => flashLight.intensity, x => flashLight.intensity = x, 0f, duration);
    }

    public void FadeInBaseLight(float duration)
    {
        DOTween.To(() => baseLight.intensity, x => baseLight.intensity = x, baseLightOriginalIntensity, duration);
    }

    public void FadeInFlashLight(float duration)
    {
        DOTween.To(() => flashLight.intensity, x => flashLight.intensity = x, flashLightOriginalIntensity, duration);
    }
}
