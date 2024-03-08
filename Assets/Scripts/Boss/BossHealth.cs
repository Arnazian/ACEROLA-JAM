using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float curHealth;
    [SerializeField] private float deathShakeForce;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private Slider bossHealthBar;
    [SerializeField] private float sliderChangeDuration;
    [SerializeField] private Transform hpBarSliderTransform;
    [SerializeField] private GameObject hpBarHitParticles;
    [SerializeField] private float healthBarSetupDuration;
    private bool healthBarIsSetup = false;

    private void Start()
    {
        curHealth = maxHealth;
        bossHealthBar.maxValue = maxHealth;
        bossHealthBar.value = 0f;
    }

    public void TakeDamage(float damage)
    {
        if (!healthBarIsSetup)
            return;

        curHealth -= damage;
        UpdateHealthBar();
        if (curHealth <= 0)
            BossDeath();       
    }

    public void BossDeath()
    {
        CameraShake.instance.BossDeathShake(deathShakeForce);
        // Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        // death visuals
    }

    void UpdateHealthBar()
    {  
        DOTween.To(() => bossHealthBar.value, x => bossHealthBar.value = x, curHealth, sliderChangeDuration);
        Instantiate(hpBarHitParticles, hpBarSliderTransform.position, Quaternion.identity);
        // visuals
    }

    public void SetupHealthbar()
    {
        DOTween.To(() => bossHealthBar.value, x => bossHealthBar.value = x, curHealth, healthBarSetupDuration).OnComplete(() =>
            healthBarIsSetup = true );
    }
}
