using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCoolDownMax;
    private float dashCoolDown;
    private bool canDash = true;
    private bool isDashing = false;
    private CocosTopDownController cocosTopDownController;
    private bool isStunned = false;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();    
        cocosTopDownController = GetComponent<CocosTopDownController>();    
    }
    private void Update()
    {
        HandleDashCoolDown();
    }

    void HandleDashCoolDown()
    {
        if (isDashing) 
            return;
        if (dashCoolDown <= 0)
        {
            canDash = true;
        }
        else
            dashCoolDown -= Time.deltaTime;
            
        
    }
    public void HandleDashInput(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(CoroutineDoDash());
        }    
    }

    IEnumerator CoroutineDoDash()
    {      

        dashCoolDown = dashCoolDownMax;
        isDashing = true;
        canDash = false;
        PlayerStateController.instance.SetDashState(true);
        playerHealth.SetIsInvulnerable(true);
        cocosTopDownController.ForceMovePlayer(dashDistance, dashDuration);
        yield return new WaitForSeconds(dashDuration);

        playerHealth.SetIsInvulnerable(false);
        if (!isStunned)
        {
            PlayerStateController.instance.SetDashState(false);
            canDash = false;
            isDashing = false;
        }
     
    }
    public void SetStunned(bool value)
    {
        isStunned = value;  
        canDash = !value;
    }
}
