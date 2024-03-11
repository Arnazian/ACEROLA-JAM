using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using Unity.VisualScripting;

public class LightTriggerController : MonoBehaviour
{
    [SerializeField] private float lightFadeDuration;
    [SerializeField] private Light2D[] lightsToControl;
    List<float> lightIntensities = new List<float>();

    List<Tween> lightTweens = new List<Tween>();

    void Start()
    {
        foreach (var light in lightsToControl)
        {
            lightIntensities.Add(light.intensity);
            light.intensity = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        KillAllLightTweens();            
        for (int i = 0; i < lightsToControl.Length; i++)
        {
            int index = i;
            lightTweens.Add(DOTween.To(() => lightsToControl[index].intensity, x => lightsToControl[index].intensity = x, lightIntensities[index], lightFadeDuration));
            Debug.Log("In For loop");
        }                   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        KillAllLightTweens();
        for (int i = 0; i < lightsToControl.Length; i++)
        {
            int index = i;
            lightTweens.Add(DOTween.To(() => lightsToControl[index].intensity, x => lightsToControl[index].intensity = x, 0f, lightFadeDuration));
        }
    }

    private void KillAllLightTweens()
    {
        foreach (Tween t in lightTweens)
            t.Kill();
        lightTweens.Clear();    
    }
}
