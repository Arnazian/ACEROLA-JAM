using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileSpeed;

    public float GetProjectileDamage() => projectileDamage;
    public float GetProjectileSpeed() => projectileSpeed;


    #region Singleton Impelementation
    public static PlayerStats instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    #endregion
}
