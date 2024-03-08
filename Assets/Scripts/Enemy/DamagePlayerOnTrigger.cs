using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerOnTrigger : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float hitPlayerInterval;
    private float hitPlayerTimer;
    private bool hasHitPlayer = false;

    private void Update()
    {
        HandleIntervalTimer();
    }

    void HandleIntervalTimer()
    {
        if (!hasHitPlayer)
            return;
        if (hitPlayerTimer <= 0)
            hasHitPlayer = false;
        else 
            hitPlayerTimer -= Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasHitPlayer)
        {
            DamagePlayer(collision.GetComponent<PlayerHealth>());
        }
    }

    void DamagePlayer(PlayerHealth playerHealth)
    {
        hasHitPlayer = true;
        hitPlayerTimer = hitPlayerInterval;
        playerHealth.TakeDamage(damage);
    }
}
