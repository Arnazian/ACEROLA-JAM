using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BaseEnemyLightController : MonoBehaviour
{
    [SerializeField] private Light2D myLight;
    [SerializeField] private float intensity;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float distanceToActivate;
    private bool lightActive = false;
    private Transform player;

    void Start()
    {
        intensity = myLight.intensity;
        myLight.intensity = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        HandleDistanceToPlayer();
    }

    void HandleDistanceToPlayer()
    {
        if (player == null)
            return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= distanceToActivate)
            EnableLight();
        else if (lightActive)
            DisableLight();
    }

    void EnableLight()
    {
        lightActive = true;
        DOTween.To(() => myLight.intensity, x => myLight.intensity = x, intensity, fadeDuration);
    }

    private void DisableLight()
    {
        lightActive = false;
        DOTween.To(() => myLight.intensity, x => myLight.intensity = x, 0, fadeDuration);
    }
}
