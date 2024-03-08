using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FadeLightOverTime : MonoBehaviour
{
    [SerializeField] private float secondsToFade;
    private Light2D lightToFade;

    void Start()
    {
        lightToFade = GetComponent<Light2D>();
        DoFadeLight();
    }
    
    void DoFadeLight()
    {
        DOTween.To(() => lightToFade.intensity, x => lightToFade.intensity = x, 0f, secondsToFade);
    }
}
