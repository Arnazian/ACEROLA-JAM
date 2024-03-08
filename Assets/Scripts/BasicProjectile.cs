using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float projectileSpeed;
    private float projectileDamage;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private GameObject[] artToDisable;

    [Header("Destroying Projectile After Time")]
    [SerializeField] private bool destroyAfterTime = false;
    [SerializeField] private float destroyAfterSeconds;

    private string enemyTag = "Enemy";
    private string objectTag = "Object";
    private bool disabled = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (destroyAfterTime) { StartCoroutine(DestroyObjectAfterSeconds(destroyAfterSeconds)); }
        rb.velocity = transform.up * projectileSpeed;

        projectileSpeed = PlayerStats.instance.GetProjectileSpeed();
        projectileDamage = PlayerStats.instance.GetProjectileDamage();
    }

    void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        if (disabled)
            return;
        //transform.position += transform.up * Time.deltaTime * projectileSpeed;
        // transform.Translate(transform.up * Time.deltaTime * projectileSpeed, Space.World);
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (disabled)
            return;

        if (collision.CompareTag(enemyTag))
            HitEnemy(collision.GetComponent<EnemyHealth>());

        else if (collision.CompareTag("Boss"))
            HitBoss(collision.GetComponent<BossHealth>());

        else if (collision.CompareTag(objectTag))
            HitObject();
    }

    void HitBoss(BossHealth bHealth)
    {
        DisableSelf();
        bHealth.TakeDamage(projectileDamage);
        Instantiate(deathParticles, transform.position, transform.rotation);
    }
    void HitEnemy(EnemyHealth eHealth)
    {
        DisableSelf();
        eHealth.TakeDamage(projectileDamage);
        Instantiate(deathParticles, transform.position, transform.rotation);
    }

    void HitObject()
    {
        DisableSelf();
        Instantiate(deathParticles, transform.position, transform.rotation);
    }

    private void DisableSelf()
    {      
        disabled = true;
        rb.velocity = Vector3.zero;
        foreach (GameObject go in artToDisable)
            go.SetActive(false);    
    }

    IEnumerator DestroyObjectAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
