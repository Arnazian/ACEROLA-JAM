using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Charge : BaseBossAttack, IBossAttack
{
    public void DoBossAttack()
    {
        BossReferences.instance.chargeController.ChargeAtPlayer();
    }
}
