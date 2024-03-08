using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private GameObject residueObject;
    private bool disabled = false;
    private GameObject player;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        DoLaunch();
    }

    void DoLaunch()
    {
        FacePlayer();
        rb.velocity = transform.up * speed;
    }
    void FacePlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        transform.rotation = rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (disabled)
            return;
        if (collision.CompareTag("Player"))
            HitPlayer(collision.GetComponent<PlayerHealth>());        
        else if (collision.CompareTag("Object"))        
            HitObject();        
    }

    void HitPlayer(PlayerHealth playerHealth)
    {
        playerHealth.TakeDamage(damage);
        Instantiate(residueObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void HitObject()
    {
        Instantiate(residueObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
