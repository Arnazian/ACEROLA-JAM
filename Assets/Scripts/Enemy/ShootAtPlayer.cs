using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootIntervalMin;
    [SerializeField] private float shootIntervalMax;
    private float shootIntervalCur;
    private bool canShoot = false;

    private BaseEnemyActivation baseEnemyActivation;

    private void Start()
    {
        baseEnemyActivation = GetComponent<BaseEnemyActivation>();
        baseEnemyActivation.onActivate.AddListener(ActivateShooting);
    }

    private void Update()
    {
        HandleShootCoolDown();        
    }

    void HandleShootCoolDown()
    {
        if (!canShoot)
            return;
        if (shootIntervalCur <= 0)
            DoShoot();
        else
            shootIntervalCur -= Time.deltaTime;
    }

    void DoShoot()
    {
        float newShootInterval = Random.Range(shootIntervalMin, shootIntervalMax);
        shootIntervalCur = newShootInterval;

        GameObject newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
    }
    void ActivateShooting()
    {
        canShoot = true;
    }
}
