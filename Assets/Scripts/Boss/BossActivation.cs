using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class BossActivation : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float distanceToActivate;
    private float distanceToPlayer;
    private bool isActive = false;


    [SerializeField] private float secondsAfterCameraOnSelf;
    [SerializeField] private float secondsToFadeLight;
    [SerializeField] private float secondsToResetCamera;
    [SerializeField] private Light2D selfLight;
    [SerializeField] private float lightIntensity;


    [SerializeField] private GameObject artToDisable;
    [SerializeField] private GameObject disappearEffect;
    [SerializeField] private Transform bossGrowTransform;
    [SerializeField] private Vector3 bossGrowSize;
    [SerializeField] private float bossShrinkDuration;
    private Vector3 bossOriginalSize;

    [SerializeField] private GameObject healthBar;

    private BossComboController bossComboController;
    private BossMoveTowardsPlayer bossMoveTowardsPlayer;
    private MinionSpawner minionSpawner;


    void Start()
    {
        bossOriginalSize = bossGrowTransform.localScale;
        healthBar.SetActive(false);
        minionSpawner = GetComponent<MinionSpawner>();
        bossMoveTowardsPlayer = GetComponent<BossMoveTowardsPlayer>();  
        bossComboController = GetComponent<BossComboController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        selfLight.intensity = 0;
    }

    
    void Update()
    {
        CheckDistanceToPlayer();
    }

    
    void CheckDistanceToPlayer()
    {
        if (isActive)
            return;


        /*
        distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceToPlayer < distanceToActivate)
            StartCoroutine(CoroutineActivateBoss());
        */
        
    }

    public void FirstEncounter()
    {
        StartCoroutine(CoroutineEncounter());
    }

    public void SecondEncounter()
    {
        StartCoroutine(CoroutineEncounter());
    }

    public void ThirdEncounter()
    {

    }
    IEnumerator CoroutineEncounter()
    {
        PlayerStateController.instance.StunPlayer();
        AudioManager.instance.PlayBossScream();
        CameraShake.instance.SetCameraTarget(transform);
        yield return new WaitForSeconds(secondsAfterCameraOnSelf);

        bossGrowTransform.DOScale(bossGrowSize, secondsToFadeLight);
        DOTween.To(() => selfLight.intensity, x => selfLight.intensity = x, lightIntensity, secondsToFadeLight);
        yield return new WaitForSeconds(secondsToFadeLight);

        Vector3 tinyScale = new Vector3(0f, 0f, 1f);
        bossGrowTransform.DOScale(tinyScale, bossShrinkDuration);
        Instantiate(disappearEffect, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(secondsToResetCamera);

        CameraShake.instance.ResetCameraTarget();
        PlayerStateController.instance.UnStunPlayer();

        // spawn minions
        // enable stage 2
        // destroy self
        Destroy(gameObject);
    }

    void ActivateAttackMode()
    {
        bossComboController.StartNewCombo();
        bossMoveTowardsPlayer.SetCanMove(true);
        minionSpawner.SetCanSpawn(true);
        healthBar.SetActive(true);
        GetComponent<BossHealth>().SetupHealthbar();
    }
}
