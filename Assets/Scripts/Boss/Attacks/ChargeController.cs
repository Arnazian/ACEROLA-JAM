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
        FindObjectOfType<BossReferences>().chargeVisuals.SetActive(false);
        bossMoveTowardsPlayer = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossMoveTowardsPlayer>();
    }

    public void ChargeAtPlayer()
    {
        StartCoroutine(CoroutineBossAttack());
    }
    IEnumerator CoroutineBossAttack()
    {
        Vector3 targetPosition = transform.position + (transform.up * chargeDistance);

        FindObjectOfType<BossReferences>().chargeVisuals.SetActive(true);
        bossMoveTowardsPlayer.SetCanMove(false);
        yield return new WaitForSeconds(windupDuration);
        FindObjectOfType<BossReferences>().chargeVisuals.SetActive(false);
        bossMoveTowardsPlayer.MoveToPosition(targetPosition, chargeMoveDuration);

        yield return new WaitForSeconds(chargeMoveDuration);
        bossMoveTowardsPlayer.SetCanMove(true);

        FindObjectOfType<BossReferences>().comboAttackHandler.DoneWithAttack();
    }
}
