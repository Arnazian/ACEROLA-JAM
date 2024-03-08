using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttackHandler : MonoBehaviour
{
    private BossCombo currentCombo;
    private BaseBossAttack currentAttack;

    private int currentAttackCounter = 0;
    private BossComboController comboController;

    private void Awake()
    {
        comboController = GetComponent<BossComboController>();
    }

    public void StartCombo(BossCombo comboToDo)
    {
        currentCombo = comboToDo;
        DoCurrentAttack();
    }

    public void StartNextAttackInCombo()
    {
        currentAttackCounter++;
        if (currentAttackCounter >= currentCombo.attacksInCombo.Length)
        {
            EndCombo();
            return;
        }
        DoCurrentAttack();
    }

    void DoCurrentAttack()
    {
        currentAttack = currentCombo.attacksInCombo[currentAttackCounter];
        currentAttack.comboAttackHandler = this;
        currentAttack.GetComponent<IBossAttack>().DoBossAttack();
    }

    public void DoneWithAttack()
    {
        StartCoroutine(CoroutineDoneWithAttack());
    }
    public IEnumerator CoroutineDoneWithAttack()
    {
        float timeToWait = currentCombo.delayAfterAttack[currentAttackCounter];
        yield return new WaitForSeconds(timeToWait);
        StartNextAttackInCombo();
    }

    void EndCombo()
    {
        currentAttackCounter = 0;
        comboController.FinishedWithCombo();
    }
}
