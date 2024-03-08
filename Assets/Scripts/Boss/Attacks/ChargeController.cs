using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeController : MonoBehaviour
{
    [SerializeField] private float chargeMoveDuration;
    [SerializeField] private float chargeDistance;
    [SerializeField] private float windupDuration;

    private BossMoveTowardsPlayer bossMoveTowardsPlayer;
    void Start()
    {
        BossReferences.instance.chargeVisuals.SetActive(false);
        bossMoveTowardsPlayer = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossMoveTowardsPlayer>();
    }

    public void ChargeAtPlayer()
    {
        StartCoroutine(CoroutineBossAttack());
    }
    IEnumerator CoroutineBossAttack()
    {
        Vector3 targetPosition = transform.position + (transform.up * chargeDistance);

        BossReferences.instance.chargeVisuals.SetActive(true);
        bossMoveTowardsPlayer.SetCanMove(false);
        yield return new WaitForSeconds(windupDuration);
        BossReferences.instance.chargeVisuals.SetActive(false);
        bossMoveTowardsPlayer.MoveToPosition(targetPosition, chargeMoveDuration);

        yield return new WaitForSeconds(chargeMoveDuration);
        bossMoveTowardsPlayer.SetCanMove(true);

        BossReferences.instance.comboAttackHandler.DoneWithAttack();
    }
}
