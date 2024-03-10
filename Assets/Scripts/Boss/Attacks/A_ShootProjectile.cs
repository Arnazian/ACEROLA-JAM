using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_ShootProjectile : BaseBossAttack, IBossAttack
{
    
    [SerializeField] private GameObject projectileToSpawn;
    private Transform shootPoint;
    public void DoBossAttack()
    {
        shootPoint = FindObjectOfType<BossReferences>().shootPoint;

        Instantiate(projectileToSpawn, shootPoint.position, Quaternion.identity);
        comboAttackHandler.DoneWithAttack();
    }
}
