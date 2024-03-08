using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image flashFill;
    [SerializeField] private Image healthFill;
    private float maxHealth;
    private float curHealth;
    private float curHealthBarValue;

    [Header("Changing Values")]
    [Range(0.1f, 1f)]
    [SerializeField] private float durationToChangeOneHealthPercent = 0.2f;
    [Range(0.1f, 1f)]
    [SerializeField] private float durationToChangeOneFlashPercent = 0.4f;
    [Range(0.1f, 1f)]
    [SerializeField] private float flashDelay = 0.3f;

    private float curFlashDuration;

    bool flashTimer = false;
    /// <summary>
    /// change fill.amount to calculating amount
    /// </summary>
    
    void Update()
    {
        FlashTimer();
    }

    public void FlashTimer()
    {
        if(flashTimer == false) { return; }

        curFlashDuration -= Time.deltaTime;

        if (curFlashDuration <= 0) 
        {
            flashTimer = false;
            float duration = (flashFill.fillAmount - GetCurHealthPercent()) * durationToChangeOneFlashPercent;
            flashFill.DOFillAmount(GetCurHealthPercent(), duration);
        }        
    }

    public void SetHealth(float amount)
    {
        curHealth = amount;
        float healthChangeDuration = (healthFill.fillAmount - GetCurHealthPercent()) * durationToChangeOneHealthPercent;
        healthFill.DOFillAmount(GetCurHealthPercent(), healthChangeDuration);
        curFlashDuration = flashDelay;
        flashTimer = true;
    }
    public void DecreaseHealth(float amount)
    {
        curHealth -= amount;
        float healthChangeDuration = (healthFill.fillAmount - GetCurHealthPercent()) * durationToChangeOneHealthPercent;
        healthFill.DOFillAmount(GetCurHealthPercent(), healthChangeDuration);
        
        curFlashDuration = flashDelay;
        flashTimer = true;        
    }

    public void IncreaseHealth(float amount)
    {
        curHealth += amount;
        float healthChangeDuration = (healthFill.fillAmount - GetCurHealthPercent()) * durationToChangeOneHealthPercent;
        healthFill.DOFillAmount(GetCurHealthPercent(), healthChangeDuration);


        curFlashDuration = flashDelay;
        flashTimer = true;
    }

    float GetCurHealthPercent() => curHealth / maxHealth;
    float GetFlashPercent() => curHealth / maxHealth;

    public void TemporaryDamage(float amount)
    {
        DecreaseHealth(amount);
    }

    public void InitializeHealth(float amount)
    {
        maxHealth = amount;
        curHealth = maxHealth;
        flashFill.fillAmount = 1;
        healthFill.fillAmount = 1;
        SetHealth(curHealth);
    }
}
