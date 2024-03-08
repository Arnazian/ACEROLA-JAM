using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyTouchPlayer : MonoBehaviour
{
    [SerializeField] private bool selfDestructOnTouch = true;
    [SerializeField] private float damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DamagePlayer(collision.GetComponent <PlayerHealth>());
        }
    }

    void DamagePlayer(PlayerHealth playerHealth)
    {
        playerHealth.TakeDamage(damage);

        if (selfDestructOnTouch)
            SelfDestruct(); 
    }

    void SelfDestruct()
    {
        GetComponent<EnemyHealth>().EnemyDeath();
    }
}
