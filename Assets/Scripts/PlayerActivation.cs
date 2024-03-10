using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerActivation : MonoBehaviour
{
    [SerializeField] private bool disableStartDelay = false;
    [SerializeField] private GameObject particles;
    [SerializeField] private SpriteRenderer[] playerArt;
    private PlayerLightController playerLightController;

    [Header("Wait Times")]
    [SerializeField] private float secondsBeforeActivation;
    [SerializeField] private float secondsAfterBaseLightFade;
    [SerializeField] private float secondsAfterArtFade;
    [SerializeField] private float secondsAfterFlashLightFade;
    [SerializeField] private float secondsBeforeMovePrompt;

    [Header("Fade Durations")]
    [SerializeField] private float baseLightFadeInDuration;
    [SerializeField] private float artFadeInDuration;
    [SerializeField] private float flashLightFadeInDuration;

    [SerializeField] private CursorController cursorController;
    [SerializeField] private TutorialPrompts tutorialPrompts;
    private PlayerHealth playerHeatlh;

    private void Start()
    {
        if (disableStartDelay)
        {
            cursorController.EnableFancyMouse();
            return;
        }

        cursorController.DisableFancyMouse();
        playerHeatlh = GetComponent<PlayerHealth>();
        playerHeatlh.DisableHealthBar();

        particles.SetActive(false);   
        playerLightController = GetComponent<PlayerLightController>();
        playerLightController.FadeOutLights(0f);

        foreach (SpriteRenderer sr in playerArt)
            sr.DOFade(0f, 0f);

        StartCoroutine(CoroutineActivatePlayer());
    }
    IEnumerator CoroutineActivatePlayer()
    {
        yield return new WaitForEndOfFrame();
        PlayerStateController.instance.StunPlayer();
        CameraShake.instance.SetCameraTarget(transform);
        yield return new WaitForSeconds(secondsBeforeActivation);

        playerLightController.FadeInBaseLight(baseLightFadeInDuration);
        yield return new WaitForSeconds(secondsAfterBaseLightFade);

        foreach (SpriteRenderer sr in playerArt)
            sr.DOFade(1f, artFadeInDuration);
        AudioManager.instance.PlayMusic();

        yield return new WaitForSeconds(secondsAfterArtFade);
        playerHeatlh.EnableHealthBar();
        playerLightController.FadeInFlashLight(flashLightFadeInDuration);
        yield return new WaitForSeconds(secondsAfterFlashLightFade);

        cursorController.EnableFancyMouse();
        particles.SetActive(true);
   
        PlayerStateController.instance.UnStunPlayer();
        CameraShake.instance.ResetCameraTarget();

        yield return new WaitForSeconds(secondsBeforeMovePrompt);
        tutorialPrompts.OpenMovePormpt();
    }
}