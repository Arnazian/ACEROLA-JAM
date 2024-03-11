using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Material cursorMaterial;
    [SerializeField] private float cursorFadeDuration;
    [SerializeField] private ParticleSystem burstParticles;
    [SerializeField] private ParticleSystem followParticles;
    private bool normalMouseVisible = false;

    
    void Start()
    {
 
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        if(PlayerStateController.instance != null)
        {
            PlayerStateController.instance.onShootStateEnter.AddListener(DisableFancyMouse);
            PlayerStateController.instance.onMoveStateEnter.AddListener(EnableFancyMouse);
        }
        if (PauseGame.instance != null)
        {
            PauseGame.instance.onGamePause.AddListener(SetNormalMouseVisible);
            PauseGame.instance.onGamePause.AddListener(DisableFancyMouse);
            PauseGame.instance.onGameResume.AddListener(SetNormalMouseInvisble);
            PauseGame.instance.onGameResume.AddListener(EnableFancyMouse);
        }       
    }

    
    void Update()
    {
        if (!normalMouseVisible && Cursor.visible)
            Cursor.visible = false;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = mousePos;
    }

    public void EnableFancyMouse()
    {
        cursorMaterial.DOFloat(0f, "_FadeAmount", cursorFadeDuration).SetUpdate(true);        
        var emission = followParticles.emission;
        emission.enabled = true;
        followParticles.Play();
    }
    public void DisableFancyMouse()
    {
        var emission = followParticles.emission;
        emission.enabled = false;
        // burstParticles.Play();
        cursorMaterial.DOFloat(1f, "_FadeAmount", cursorFadeDuration).SetUpdate(true);
    }

    public void SetNormalMouseVisible()
    {
        normalMouseVisible = true;
        Cursor.visible = true;
    }
    public void SetNormalMouseInvisble()
    {
        normalMouseVisible = false;
        Cursor.visible = false;
    }

}
