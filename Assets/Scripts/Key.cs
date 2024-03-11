using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using System;

public class Key : MonoBehaviour
{
    [SerializeField] private Light2D keyLight;
    [SerializeField] private float lightFadeInDuration;
    [SerializeField] private float lightFadeOutDuration;
    [SerializeField] private GameObject objectToDisable; 
    private float lightIntensity;
    private Tween lightTween;


    private void OnEnable()
    {
        lightIntensity = keyLight.intensity;
        keyLight.intensity = 0;
        lightTween = DOTween.To(() => keyLight.intensity, x => keyLight.intensity = x, lightIntensity, lightFadeInDuration);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PickUpKey();
    }

    void PickUpKey()
    {
        // add key to player stats
        // update UI
        // play key sound

        objectToDisable.SetActive(false);
        lightTween.Kill();
        lightTween = DOTween.To(() => keyLight.intensity, x => keyLight.intensity = x, 0f, lightFadeOutDuration).OnComplete(() =>
            Destroy(gameObject));
    }
}
