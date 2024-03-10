using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private bool isBoss = false;
    [SerializeField] private float maxHealth;
    private float curHealth;
    [SerializeField] private float deathShakeForce;
    [SerializeField] private GameObject deathParticles;



    private void Start()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Did take damage");
        curHealth -= damage;
        if (curHealth <= 0)
            EnemyDeath();
    }

    public void EnemyDeath()
    {
        AudioManager.instance.PlayEnemyDeath();
        CameraShake.instance.EnemyDeathShake(deathShakeForce);
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        // death visuals
    }
}
