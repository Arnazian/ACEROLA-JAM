using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private GameObject hitObjectResidue;
    [SerializeField] private GameObject hitPlayerResidue;
    [SerializeField] private GameObject[] artToDisable;

    [Header("Audio Variables")]
    [SerializeField] private Vector2 spellPitch;
    [SerializeField] private Vector2 strongPitch;
    [SerializeField] private AudioSource spellSound;
    [SerializeField] private AudioSource strongSound;

    private bool disabled = false;
    private Rigidbody2D rb;

    void Start()
    {
        /*
        spellSound.pitch = Random.Range(spellPitch.x, spellPitch.y);
        strongSound.pitch = Random.Range(strongPitch.x, strongPitch.y);
        spellSound.Play();
        strongSound.Play();
        */
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * projectileSpeed;
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

    void HitPlayer(PlayerHealth heatlh)
    {
        AudioManager.instance.PlayHitEnemy();
        DisableSelf();
        heatlh.TakeDamage(projectileDamage);
        if (hitPlayerResidue != null)
            Instantiate(hitPlayerResidue, transform.position, transform.rotation);
    }


    void HitObject()
    {
        AudioManager.instance.PlayHitObject();
        DisableSelf();
        if (hitObjectResidue != null)
            Instantiate(hitObjectResidue, transform.position, transform.rotation);
    }

    private void DisableSelf()
    {
        disabled = true;
        rb.velocity = Vector3.zero;
        foreach (GameObject go in artToDisable)
            go.SetActive(false);
    }
}
