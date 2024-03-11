using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
public class EnemyHealth : MonoBehaviour
{
    public UnityEvent onEnemyDeath;
    [SerializeField] private float maxHealth;
    private float curHealth;
    [SerializeField] private float deathShakeForce;
    [SerializeField] private GameObject deathParticles;

    private BaseEnemyActivation baseEnemyActivation;
    private bool healthActive = false;



    private void Start()
    {
        baseEnemyActivation = GetComponent<BaseEnemyActivation>();
        //baseEnemyActivation.onActivate.AddListener(ActivateHealth);
        ActivateHealth();
        curHealth = maxHealth;
    }

    private void ActivateHealth()
    {
        healthActive = true;
    }
    public void TakeDamage(float damage)
    {
        if (!healthActive)
            return;
       
        Debug.Log("Did take damage");
        curHealth -= damage;
        if (curHealth <= 0)
            EnemyDeath();
    }

    public void EnemyDeath()
    {
        onEnemyDeath.Invoke();
        AudioManager.instance.PlayEnemyDeath();
        CameraShake.instance.EnemyDeathShake(deathShakeForce);
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        // death visuals
    }
}
