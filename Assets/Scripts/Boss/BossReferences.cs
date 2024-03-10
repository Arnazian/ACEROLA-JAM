using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossReferences: MonoBehaviour
{
    public GameObject boss;
    public float maxHealth;
    public Transform shootPoint;
    public GameObject chargeVisuals;
    public ChargeController chargeController;
    public ComboAttackHandler comboAttackHandler;
}

