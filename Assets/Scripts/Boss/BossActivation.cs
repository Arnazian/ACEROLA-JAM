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
    [SerializeField] private Light2D selfLight;
    [SerializeField] private float lightIntensity;

    [SerializeField] private GameObject healthBar;

    private BossComboController bossComboController;
    private BossMoveTowardsPlayer bossMoveTowardsPlayer;
    private MinionSpawner minionSpawner;


    void Start()
    {
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

        distanceToPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceToPlayer < distanceToActivate)
            StartCoroutine(CoroutineActivateBoss());
        
    }
    IEnumerator CoroutineActivateBoss()
    {
        isActive = true;

        PlayerStateController.instance.StunPlayer();
        CameraShake.instance.SetCameraTarget(transform);
        yield return new WaitForSeconds(secondsAfterCameraOnSelf);
        // SCREAM
        DOTween.To(() => selfLight.intensity, x => selfLight.intensity = x, lightIntensity, secondsToFadeLight).OnComplete(() =>
        {
            CameraShake.instance.ResetCameraTarget();
            PlayerStateController.instance.UnStunPlayer();

            bossComboController.StartNewCombo();
            bossMoveTowardsPlayer.SetCanMove(true);
            minionSpawner.SetCanSpawn(true);
            healthBar.SetActive(true);
            GetComponent<BossHealth>().SetupHealthbar();
        });

    }
}
