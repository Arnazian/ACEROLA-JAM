using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStateController : MonoBehaviour
{
    public UnityEvent onMoveStateEnter;
    public UnityEvent onShootStateEnter;
    private PlayerLightController playerLightController;
    private CocosTopDownController cocosTopDownController;
    private BasicShooting basicShooting;
    private Dash dash;

    private void Start()
    { 
        dash = GetComponent<Dash>();
        playerLightController = GetComponent<PlayerLightController>();
        cocosTopDownController = GetComponent<CocosTopDownController>();
        basicShooting = GetComponent<BasicShooting>();
    }
    public void EnableShootState()
    {
        onShootStateEnter.Invoke();
        playerLightController.ShootingStateLights();
        cocosTopDownController.SetSpeedToShootState();
    }

    public void EnableMoveState()
    {
        onMoveStateEnter.Invoke();
        playerLightController.MoveStateLights();
        cocosTopDownController.SetSpeedToMoveState();
    }

    public void SetDashState(bool newState)
    {
        if (newState)
        {
            cocosTopDownController.DisableMovement();
            basicShooting.DisableShooting();
        }
        else
        {
            cocosTopDownController.EnableMovement();
            basicShooting.EnableShooting();
        }
    }
    public void StunPlayer()
    {
        dash.SetStunned(true);
        cocosTopDownController.DisableMovement();
        basicShooting.DisableShooting();
    }

    public void UnStunPlayer()
    {
        dash.SetStunned(false);
        cocosTopDownController.EnableMovement();
        basicShooting.EnableShooting();
    }

    #region  Singleton implementation
    public static PlayerStateController instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    #endregion
}
