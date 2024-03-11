using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseEnemyTouchPlayer : MonoBehaviour
{

    [SerializeField] private bool selfDestructOnTouch = true;
    [SerializeField] private float damage;

    private BaseEnemyActivation baseEnemyActivation;
    private bool canDamage = false;

    private void Start()
    {
        baseEnemyActivation = GetComponent<BaseEnemyActivation>();
        baseEnemyActivation.onActivate.AddListener(ActivateDamage);
    }

    void ActivateDamage()
    {
        canDamage = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canDamage)
            return;

        if (collision.CompareTag("Player"))
        {
            DamagePlayer(collision.GetComponent <PlayerHealth>());
        }
    }

    void DamagePlayer(PlayerHealth playerHealth)
    {
        if (playerHealth.GetIsInvulnerable())
            return;
        playerHealth.TakeDamage(damage);

        if (selfDestructOnTouch)
            SelfDestruct(); 
    }

    void SelfDestruct()
    {
        GetComponent<EnemyHealth>().EnemyDeath();
    }
}
