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


    #region Singleton Implementation
    public static BossReferences instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    #endregion

}

