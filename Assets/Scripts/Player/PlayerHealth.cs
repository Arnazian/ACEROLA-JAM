using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private DeathMenu deathMenu;
    [SerializeField] private GameObject healthBarObject;
    [SerializeField] private Slider[] healthBarSliders;
    [SerializeField] private float startOfGameSliderChangeDuration;
    [SerializeField] private float sliderChangeDuration;
    [SerializeField] private float maxHealth;
    private float curHealth;

    [SerializeField] private Material impactMaterial;
    [SerializeField] private float impactFadeDuration;
    private Tween impactFadeTween;
    private bool isInvulnerable = false;

    private void Start()
    {
        curHealth = maxHealth;
        impactMaterial.DOFloat(1f, "_FadeAmount", 0f);
    }

    public void DisableHealthBar()
    {
        healthBarObject.SetActive(false);
    }
    public void EnableHealthBar()
    {
        healthBarObject.SetActive(true);
        foreach (Slider s in healthBarSliders)
        {
            s.maxValue = maxHealth;
            s.value = 0;
            DOTween.To(() => s.value, x => s.value = x, curHealth, startOfGameSliderChangeDuration);
        }
    }
    public void TakeDamage(float damage)
    {
        if (isInvulnerable)
            return;
        curHealth -= damage;
        if (curHealth <= 0)
            PlayerDeath();
        UpdateHealthVisuals();
    }
    void UpdateHealthVisuals()
    {
        foreach (Slider s in healthBarSliders)
        {
            DOTween.To(() => s.value, x => s.value = x, curHealth, sliderChangeDuration);
        }

        impactFadeTween.Kill();
        impactFadeTween = impactMaterial.DOFloat(0f, "_FadeAmount", impactFadeDuration).OnComplete(() =>
            impactMaterial.DOFloat(1f, "_FadeAmount", impactFadeDuration));
    }

    public void SetIsInvulnerable(bool newValue)
    {
        isInvulnerable = newValue;
    }
    public bool GetIsInvulnerable() => isInvulnerable;
    void PlayerDeath()
    {
        SetIsInvulnerable(true);
        deathMenu.EnableDeathMenu();
        PlayerStateController.instance.StunPlayer();
    }
}
